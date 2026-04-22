using HtmlAgilityPack;

namespace Internship.Task6;

public class Scraper : IDisposable
{
    private readonly HttpClient client;

    public Scraper()
    {
        client = new HttpClient();
        client.DefaultRequestHeaders.UserAgent.ParseAdd(
            "Mozilla/5.0 (compatible; CognifyzScraper/1.0; +https://cognifyz.com)");
        client.DefaultRequestHeaders.Accept.ParseAdd("text/html,application/xhtml+xml");
        client.Timeout = TimeSpan.FromSeconds(15);
    }

    public async Task<HtmlDocument> FetchAsync(string url)
    {
        using var response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();
        var html = await response.Content.ReadAsStringAsync();
        var doc = new HtmlDocument();
        doc.LoadHtml(html);
        return doc;
    }

    public void Dispose()
    {
        client.Dispose();
        GC.SuppressFinalize(this);
    }
}
