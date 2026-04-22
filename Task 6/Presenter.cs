using System.Net;
using HtmlAgilityPack;

namespace Internship.Task6;

public static class Presenter
{
    private const int MaxLinks = 50;
    private const int MaxXPathResults = 50;

    public static void PrintHeader(string title)
    {
        var bar = new string('=', Math.Min(title.Length + 4, 80));
        Console.WriteLine();
        Console.WriteLine(bar);
        Console.WriteLine($"  {title}");
        Console.WriteLine(bar);
    }

    public static void PrintTitleAndHeadings(HtmlDocument doc)
    {
        var title = doc.DocumentNode.SelectSingleNode("//title")?.InnerText?.Trim() ?? "(no title)";
        PrintHeader($"Title: {Clean(title)}");

        string[] headingTags = { "h1", "h2", "h3" };
        bool anyFound = false;
        foreach (var tag in headingTags)
        {
            var nodes = doc.DocumentNode.SelectNodes($"//{tag}");
            if (nodes == null) continue;
            Console.WriteLine();
            Console.WriteLine($"--- {tag.ToUpper()} ({nodes.Count}) ---");
            int i = 1;
            foreach (var n in nodes)
            {
                var text = Clean(n.InnerText);
                if (string.IsNullOrWhiteSpace(text)) continue;
                Console.WriteLine($"  {i++}. {Truncate(text, 120)}");
                anyFound = true;
            }
        }
        if (!anyFound)
        {
            Console.WriteLine("(no headings found)");
        }
    }

    public static void PrintLinks(HtmlDocument doc, string baseUrl)
    {
        var anchors = doc.DocumentNode.SelectNodes("//a[@href]");
        if (anchors == null || anchors.Count == 0)
        {
            Console.WriteLine("No links found.");
            return;
        }
        PrintHeader($"Links ({anchors.Count})");

        int shown = 0;
        foreach (var a in anchors)
        {
            var href = a.GetAttributeValue("href", "").Trim();
            if (string.IsNullOrEmpty(href) || href.StartsWith("#") || href.StartsWith("javascript:"))
                continue;
            var text = Clean(a.InnerText);
            if (string.IsNullOrWhiteSpace(text)) text = "(no text)";
            var absolute = TryResolve(baseUrl, href);
            Console.WriteLine($"{shown + 1,3}. {Truncate(text, 50),-50} -> {Truncate(absolute, 80)}");
            shown++;
            if (shown >= MaxLinks)
            {
                Console.WriteLine($"    ... (showing first {MaxLinks} of {anchors.Count})");
                break;
            }
        }
    }

    public static void PrintMeta(HtmlDocument doc)
    {
        PrintHeader("Meta");

        var title = doc.DocumentNode.SelectSingleNode("//title")?.InnerText?.Trim();
        Console.WriteLine($"Title:       {Clean(title) ?? "(none)"}");

        var descNode = doc.DocumentNode.SelectSingleNode("//meta[@name='description']");
        var desc = descNode?.GetAttributeValue("content", null);
        Console.WriteLine($"Description: {Clean(desc) ?? "(none)"}");

        var kwNode = doc.DocumentNode.SelectSingleNode("//meta[@name='keywords']");
        var kw = kwNode?.GetAttributeValue("content", null);
        if (!string.IsNullOrEmpty(kw))
        {
            Console.WriteLine($"Keywords:    {Clean(kw)}");
        }

        var ogNodes = doc.DocumentNode.SelectNodes("//meta[starts-with(@property,'og:')]");
        if (ogNodes != null && ogNodes.Count > 0)
        {
            Console.WriteLine();
            Console.WriteLine("Open Graph:");
            foreach (var n in ogNodes)
            {
                var prop = n.GetAttributeValue("property", "");
                var content = n.GetAttributeValue("content", "");
                Console.WriteLine($"  {prop,-18} = {Truncate(Clean(content) ?? "", 80)}");
            }
        }
    }

    public static void PrintXPath(HtmlDocument doc, string xpath)
    {
        HtmlNodeCollection? nodes;
        try
        {
            nodes = doc.DocumentNode.SelectNodes(xpath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"XPath error: {ex.Message}");
            return;
        }
        if (nodes == null || nodes.Count == 0)
        {
            Console.WriteLine("No matches.");
            return;
        }
        PrintHeader($"XPath matches ({nodes.Count})");
        int i = 1;
        foreach (var n in nodes)
        {
            var text = Clean(n.InnerText);
            if (string.IsNullOrWhiteSpace(text)) continue;
            Console.WriteLine($"  {i}. {Truncate(text, 120)}");
            i++;
            if (i > MaxXPathResults)
            {
                Console.WriteLine($"    ... (showing first {MaxXPathResults} of {nodes.Count})");
                break;
            }
        }
    }

    private static string? Clean(string? s)
    {
        if (s == null) return null;
        return WebUtility.HtmlDecode(s).Trim()
            .Replace("\r", "")
            .Replace("\n", " ")
            .Replace("\t", " ");
    }

    private static string Truncate(string s, int max)
    {
        return s.Length <= max ? s : s.Substring(0, max - 1) + "…";
    }

    private static string TryResolve(string baseUrl, string href)
    {
        try
        {
            if (Uri.TryCreate(new Uri(baseUrl), href, out var resolved))
                return resolved.ToString();
        }
        catch { }
        return href;
    }
}
