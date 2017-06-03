using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebPageService
{
    public class Response
    {
        public int _id { get; set; }
        public string _name { get; set; }
        public string _description { get; set; }
        public string _videoUrl { get; set; }
        public List<string> _imageUrls { get; set; }
    }
}
