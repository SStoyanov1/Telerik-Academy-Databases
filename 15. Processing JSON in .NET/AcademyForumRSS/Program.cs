using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class Program
{
    private const string RssFeedUrl = @"http://forums.academy.telerik.com/feed/qa.rss";
    private const string RssFilePath = @"../../rss.xml";
    private const string JsonFilePath = @"../../rss.json";
    private const string HtmlFilePath = @"../../index.html";

    public static void Main()
    {
        // 1. Download RSS feed and save to file        
        Console.WriteLine("Downloading RSS...");
        using (var webClient = new WebClient())
            webClient.DownloadFile(RssFeedUrl, RssFilePath);

        // 2. Parse the XML from the feed to JSON
        var rssXML = XElement.Load(RssFilePath);
        string json = JsonConvert.SerializeXNode(rssXML, Newtonsoft.Json.Formatting.Indented);
        FileUtils.CreateFile(JsonFilePath, json);

        // 3. Print all question titles to the console
        var jsonObj = JObject.Parse(json);
        var titles = jsonObj["rss"]["channel"]["item"].Select(i => i["title"]);
        Console.WriteLine(string.Join(Environment.NewLine, titles));

        // 4. Parse the JSON string to POCO
        var itemsJson = jsonObj["rss"]["channel"]["item"].ToString();
        var items = JsonConvert.DeserializeObject<Item[]>(itemsJson);

        // 5. Create a HTML page that lists all questions from the RSS
        CreateHtmlPage(items);
    }

    private static void CreateHtmlPage(IEnumerable<Item> items)
    {
        var htmlGenerator = new HtmlGenerator();
        string html = htmlGenerator.GenerateHtml(items);
        FileUtils.CreateFile(HtmlFilePath, html);

        Console.WriteLine("Html page saved to <{0}>", HtmlFilePath);
    }
}
