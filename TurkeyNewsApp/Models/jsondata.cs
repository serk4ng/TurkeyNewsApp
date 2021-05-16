using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TurkeyNewsApp.Models
{
    public class jsondata
    {
        public string status { get; set; }
        public string totalResults { get; set; }
        public object articles { get; set; }
        
    }
}