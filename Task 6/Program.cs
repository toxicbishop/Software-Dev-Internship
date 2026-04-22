using HtmlAgilityPack;
using Internship.Task6;

using var scraper = new Scraper();

Console.WriteLine("=== Interactive Web Scraper ===");
Console.WriteLine("Try: news.ycombinator.com, en.wikipedia.org/wiki/Web_scraping, example.com");

while (true)
{
    Console.WriteLine();
    Console.WriteLine("Menu:");
    Console.WriteLine("  [1] Title + headings (h1/h2/h3)");
    Console.WriteLine("  [2] All links");
    Console.WriteLine("  [3] Meta info (title, description, Open Graph)");
    Console.WriteLine("  [4] Custom XPath query");
    Console.WriteLine("  [q] Quit");
    Console.Write("Pick > ");
    var choice = (Console.ReadLine() ?? "").Trim();

    if (choice is "q" or "Q")
    {
        Console.WriteLine("Bye.");
        break;
    }

    if (choice is not ("1" or "2" or "3" or "4"))
    {
        Console.WriteLine("Invalid choice.");
        continue;
    }

    Console.Write("URL > ");
    var url = (Console.ReadLine() ?? "").Trim();
    if (string.IsNullOrEmpty(url))
    {
        Console.WriteLine("URL required.");
        continue;
    }
    if (!url.StartsWith("http://") && !url.StartsWith("https://"))
    {
        url = "https://" + url;
    }

    HtmlDocument doc;
    try
    {
        Console.WriteLine($"Fetching {url} ...");
        doc = await scraper.FetchAsync(url);
    }
    catch (HttpRequestException ex)
    {
        Console.Error.WriteLine($"HTTP error: {ex.Message}");
        continue;
    }
    catch (TaskCanceledException)
    {
        Console.Error.WriteLine("Request timed out.");
        continue;
    }
    catch (UriFormatException)
    {
        Console.Error.WriteLine("Invalid URL.");
        continue;
    }
    catch (Exception ex)
    {
        Console.Error.WriteLine($"Error: {ex.Message}");
        continue;
    }

    switch (choice)
    {
        case "1":
            Presenter.PrintTitleAndHeadings(doc);
            break;
        case "2":
            Presenter.PrintLinks(doc, url);
            break;
        case "3":
            Presenter.PrintMeta(doc);
            break;
        case "4":
            Console.WriteLine("Examples: //h1   //article//p   //meta[@property='og:title']");
            Console.Write("XPath > ");
            var xpath = (Console.ReadLine() ?? "").Trim();
            if (string.IsNullOrEmpty(xpath))
            {
                Console.WriteLine("XPath required.");
                break;
            }
            Presenter.PrintXPath(doc, xpath);
            break;
    }
}
