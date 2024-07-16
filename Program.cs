using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


//Console.WriteLine("Hello, World!");

var httpClient = new HttpClient();

var baseUrl = "https://www.googleapis.com/youtube/v3/search";

Console.WriteLine("Enter your search query: ");
var query = Console.ReadLine();

var part = "snippet";
var type = "video";
var apiKey = "AIzaSyC_KSD7rol4YtJhuSFRVBQ1UmeWQ6ZIHks";
var url = $"{baseUrl}?part={part}&q={query}&type={type}&key={apiKey}";

HttpResponseMessage response = await httpClient.GetAsync(url);
string responseBody = await response.Content.ReadAsStringAsync();
var responseJson = JObject.Parse(responseBody);

//var items = (JArray)responseJson["items"];
//foreach (JObject item in items)
//{
//    string title = (string)item["snippet"]["title"];
//    string videoId = (string)item["id"]["videoId"];
//    string videoUrl = $"https://www.youtube.com/watch?v={videoId}";
//    Console.WriteLine($"{title}: {videoUrl}");
//}

var searchResponse =
JsonConvert.DeserializeObject<SearchResponse>(responseBody);
foreach (var result in searchResponse.Items)
{
    string title = result.Snippet.Title;
    string videoId = result.Id.VideoId;
    string videoUrl = $"https://www.youtube.com/watch?v={videoId}";
    Console.WriteLine($"{title}: {videoUrl}");
}


Console.WriteLine("Press any key to exit...");
Console.ReadKey();


public partial class SearchResponse
{
    [JsonProperty("kind")]
    public string Kind { get; set; } = string.Empty!;

    [JsonProperty("etag")]
    public string Etag { get; set; } = string.Empty!;

    [JsonProperty("nextPageToken")]
    public string NextPageToken { get; set; } = string.Empty!;

    [JsonProperty("regionCode")]
    public string RegionCode { get; set; } = string.Empty!;

    [JsonProperty("pageInfo")]
    public PageInfo PageInfo { get; set; } = default!;
    [JsonProperty("items")]
    public List<Item>? Items { get; set; }
}
public partial class Item
{
    [JsonProperty("kind")]
    public string Kind { get; set; } = string.Empty!;

    [JsonProperty("etag")]
    public string Etag { get; set; } = string.Empty!;

    [JsonProperty("id")]
    public Id Id { get; set; }

    [JsonProperty("snippet")]
    public Snippet Snippet { get; set; } = default!;
}

public partial class Id
{
    [JsonProperty("kind")]
    public string Kind { get; set; } = string.Empty!;

    [JsonProperty("videoId")]
    public string VideoId { get; set; } = string.Empty!;
}

public partial class Snippet
{
    [JsonProperty("publishedAt")]
    public DateTimeOffset PublishedAt { get; set; }

    [JsonProperty("channelId")]
    public string ChannelId { get; set; } = string.Empty!;

    [JsonProperty("title")]
    public string Title { get; set; } = string.Empty!;

    [JsonProperty("description")]
    public string Description { get; set; } = string.Empty!;

    [JsonProperty("thumbnails")]
    public Thumbnails Thumbnails { get; set; }

    [JsonProperty("channelTitle")]
    public string ChannelTitle { get; set; } = string.Empty!;

    [JsonProperty("liveBroadcastContent")]
    public string LiveBroadcastContent { get; set; } = string.Empty!;

    [JsonProperty("publishTime")]
    public DateTimeOffset PublishTime { get; set; }
}


public partial class Thumbnails
{
    [JsonProperty("default")]
    public Default Default { get; set; }

    [JsonProperty("medium")]
    public Default Medium { get; set; }

    [JsonProperty("high")]
    public Default High { get; set; }
}

public partial class Default
{
    [JsonProperty("url")]
    public Uri Url { get; set; }

    [JsonProperty("width")]
    public long Width { get; set; }

    [JsonProperty("height")]
    public long Height { get; set; }
}

public partial class PageInfo
{
    [JsonProperty("totalResults")]
    public long TotalResults { get; set; }

    [JsonProperty("resultsPerPage")]
    public long ResultsPerPage { get; set; }
}



