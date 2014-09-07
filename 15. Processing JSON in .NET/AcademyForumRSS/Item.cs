using System;
using Newtonsoft.Json;

public class Item
{
    public string Title { get; set; }

    public string Link { get; set; }

    public string Description { get; set; }

    public string Category { get; set; }

    public DateTime PubDate { get; set; }

    public override string ToString()
    {
        return string.Format("{0}, posted on {1}", this.Title, this.PubDate.ToShortDateString());
    }
}
