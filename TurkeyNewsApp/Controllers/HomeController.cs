using NewsAPI;
using NewsAPI.Constants;
using NewsAPI.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using TurkeyNewsApp.Extensions;
using TurkeyNewsApp.Models;

namespace TurkeyNewsApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index2()
        {
            string json;

            using (var client = new WebClient())
            {
                 json = client.DownloadString("https://newsapi.org/v2/top-headlines?country=tr&apiKey=acffc7a435f24838a883e692c0fa44dc");
            }
            
            //var root = JsonConvert.DeserializeObject(json);
 


                return View();
        }

  


        [HttpGet]
        public async Task<ActionResult> Index()
        {
            string url = "https://newsapi.org/v2/top-headlines?country=tr&category=technology&sortBy=publishedAt&apiKey=acffc7a435f24838a883e692c0fa44dc";

            List<News> lst = new List<News>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    
                   
                    var appjsondata = await response.Content.ReadAsStringAsync();
                    JavaScriptSerializer js = new JavaScriptSerializer();
  
                    jsondata myconvertedjsondata = JsonConvert.DeserializeObject<jsondata>(appjsondata);

                    var Data = JsonConvert.DeserializeObject<List<News>>((myconvertedjsondata.articles).ToString());

                    Data = Data;
                    //    var convertednews = JsonConvert.DeserializeObject<News>((myconvertedjsondata.articles).ToString());

                    lst = Data;
                    return View(Data);
                }
                return View(lst);
            }
        }


        public async Task<ActionResult> Index4()
        {
            var a  = await GetEverythingAsync("tr");
            return View(a);
        }
        public async Task<ActionResult> Index3()
        {
          
            // init with your API keyGetEverythingAsync
            var newsApiClient = new NewsApiClient("acffc7a435f24838a883e692c0fa44dc");
 

            var articlesResponse = newsApiClient.GetEverythingAsync(new EverythingRequest
            {
                // Q = "Apple", 

                SortBy = SortBys.PublishedAt,
                Language = Languages.TR,
                From = new DateTime(2021, 4, 19)


            }).GetAwaiter().GetResult();

            List<News> mynews = new List<News>();
            foreach (var article in articlesResponse.Articles)
                {
                    News news = new News();
                    news.Author = article.Author;
                    news.Content = article.Content;
                    news.Title = article.Title;
                    news.Url = article.Url;
                    news.UrlToImage = article.UrlToImage;
                    news.PublishedAt = article.PublishedAt;
                    mynews.Add(news);
                }

            return await Task.Run(() => View(mynews));

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public void TurkeyNewsConn()
        {
            // init with your API key
            var newsApiClient = new NewsApiClient("acffc7a435f24838a883e692c0fa44dc");
            var articlesResponse = newsApiClient.GetEverything(new EverythingRequest
            {
                // Q = "Apple", 
                SortBy = SortBys.PublishedAt,
                Language = Languages.TR,
                //From = new DateTime(2018, 1, 25)
            });

            if (articlesResponse.Status == Statuses.Ok)
            {
             var result = articlesResponse.Articles;
            }
          
        }

        // TESTTTTT- - - - - - - -- -  - - -- - - - -   -- -  - 

        private readonly string _key;
        private string _baseUrl = "https://newsapi.org/v2/";

        public string BaseUrl
        {
            get => _baseUrl;
            set
            {
                if (!Uri.IsWellFormedUriString(value, UriKind.Absolute))
                {
                    throw new UriFormatException($"Invalid base URI: {value}.");
                }

                _baseUrl = value;
            }
        }

        private string _userAgent { get; set; }

        public string UserAgent
        {
            get => _userAgent;
            set => _userAgent = value;
        }

        private HttpClient _client = new HttpClient();
        private bool disposed = false;

        /// <summary>
        ///  API Client.
        /// </summary>
        /// <param name="apiKey"> Your API key needed to access the endpoints.</param>
        public HomeController()
        {
            _key = "acffc7a435f24838a883e692c0fa44dc";
        }

        /// <summary>
        /// Returns a <see cref="NewsSourcesModel"/> of sources available.
        /// </summary>
        /// <returns><see cref="Task{TResult}"/></returns>
 

        /// <summary>
        /// Returns a <see cref="NewsModel"/> containing a list of articles relating to <paramref name="query"/>.
        /// </summary>
        /// <param name="query">Keywords or phrases to search for.</param>
        /// <param name="sources">A comma-separated string of identifiers (maximum 20) for the news sources or blogs you want headlines from.</param>
        /// <param name="lang">The language you want to get headlines for. Default being all languages returned.</param>
        /// <param name="domains"> An array of domains (eg bbc.co.uk, techcrunch.com, engadget.com) to restrict the search to. </param>
        /// <returns>Task{TResult}</returns>
        public async Task<News> GetEverythingAsync( string lang = null)
        {

            /*
                        Uri uri = new Uri(_baseUrl + "everything")
                            .AddQuery("apiKey", _key)


                            .AddQuery("language", lang.ToString()); */

            Uri uri = new Uri("https://newsapi.org/v2/everything?q=bitcoin&apiKey=acffc7a435f24838a883e692c0fa44dc");
    
            HttpResponseMessage response = await _client.GetAsync(uri);
            response.EnsureSuccessStatusCode();

            // var result = JsonConvert.DeserializeObject<News>(await response.Content.ReadAsStringAsync());
            News nre = new News();
            var result =nre;
            return result;
        }

        /// <summary>
        /// Returns a <see cref="NewsModel"/> containing a list of top articles relating to <paramref name="query"/>.
        /// </summary>
        /// <param name="query">Keywords or phrases to search for.</param>
        /// <param name="sources">A comma-separated string of identifiers (maximum 20) for the news sources or blogs you want headlines from.</param>
        /// <param name="lang">The language you want to return top headlines for. Default being all languages returned.</param> 
        /// <param name="domains"> An array of domains (eg bbc.co.uk, techcrunch.com, engadget.com) to restrict the search to. </param>
        /// <returns>Task{TResult}</returns>
 
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }

            if (disposing)
            {
                _client.Dispose();
            }

            disposed = true;
        }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            Dispose(true);
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }


 


    }
}