using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TurkeyNewsApp.Extensions
{
    internal static class HttpExtensions
    {
        internal static Uri AddQuery(this Uri url, string paramName, string paramValue)
        {
            UriBuilder uriBuilder = new UriBuilder(url);
            System.Collections.Specialized.NameValueCollection query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query[paramName] = paramValue;
            uriBuilder.Query = query.ToString();

            return uriBuilder.Uri;
        }
    }
}