using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace WebPageService
{
    /// <summary>
    /// Summary description for Service
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Service : System.Web.Services.WebService
    {
        //ka, en, ru

        [WebMethod]
        public List<Response> GetItems(string lang)
        {
            DataContextDataContext db = new DataContextDataContext();
            List<Response> res = new List<Response>();
            List<Item> pInfoList = db.Items.ToList();

            foreach (var item in pInfoList) res.Add(PageInfoToResponse(lang, item, true));

            return res;
        }

        [WebMethod]
        public List<Response> GetItemById(string lang, int id)
        {
            DataContextDataContext db = new DataContextDataContext();
            List<Response> res = new List<Response>();
            Item _pageInfo = db.Items.FirstOrDefault(w => w.Id == id);
            if (_pageInfo == null) throw new Exception("ინფორმაცია ვერ მოიძებნა");


            res.Add(PageInfoToResponse(lang, _pageInfo, false));

            return res;
        }


        private Response PageInfoToResponse(string lang, Item item, bool oneImage)
        {
            if (item == null) throw new Exception();
            if (lang != "ka" & lang != "en" & lang != "ru") lang = "ka";

            Response response = new Response();
            response._id = item.Id;
            response._name = lang == "ka" ? item.Name_Geo : lang == "en" ? item.Name_Eng : lang == "ru" ? item.Name_Rus : "";
            if (string.IsNullOrEmpty(response._name)) response._name = item.Name_Geo.Or(item.Name_Eng).Or(item.Name_Rus);


            response._description = lang == "ka" ? item.Description_Geo : lang == "en" ? item.Description_Eng : lang == "ru" ? item.Description_Rus : ""; ;
            if (string.IsNullOrEmpty(response._name)) response._name = item.Description_Geo.Or(item.Description_Eng).Or(item.Description_Rus);

            response._videoUrl = lang == "ka" ? item.VideoLink_Geo : lang == "en" ? item.VideoLink_Eng : lang == "ru" ? item.VideoLink_Rus : "";
            if (string.IsNullOrEmpty(response._name)) response._name = item.VideoLink_Geo.Or(item.VideoLink_Eng).Or(item.VideoLink_Rus);

            if (oneImage)
            {
                List<string> _images = new List<string>();
                _images.Add(item.ItemImages.Select(s => s.Url).FirstOrDefault());

                response._imageUrls = _images;
            }
            else
            {
                response._imageUrls = item.ItemImages.Select(s => s.Url).ToList();
            }



            return response;
        }


    }
}
