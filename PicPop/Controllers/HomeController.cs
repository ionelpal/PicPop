using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Web;
using System.Web.Mvc;
using PicPop.BusinessLogic;
using PicPop.Models;

namespace PicPop.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(int? categoryId)
        {
            if (categoryId.HasValue)
                return RedirectToAction("Index", "Image", new {id = categoryId});

            var images = new PicPopImagesHelper();
            var lstImagesCarrousel = images.GetUrlImages(numImages: 5);
            var lastImagesInserted = images.LastImages(10);

            var model = new HomeViewModel()
            {
                Categories = PicPop.Helper.CategoriesHelper.GetCategoriesSelectedItems(),
                ListImages = lstImagesCarrousel.Select(CloudStorageManagerHelper.GetUrlInfo).ToList(),
                LastImagesInserted = lastImagesInserted.Select(CloudStorageManagerHelper.GetUrlInfo).ToList(),
                NumImages = lstImagesCarrousel.Count
            };

            return View(model);
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

        public ContentResult GetWeather()
        {
            var weather = new WeatherService.GlobalWeatherSoapClient("GlobalWeatherSoap");
            var temp = weather.GetWeather("Dublin", "Ireland");
            XmlDocument xd = new XmlDocument();
            xd.LoadXml(temp);

            XmlNodeList xnList = xd.SelectNodes("/CurrentWeather/Temperature");
            string x = "";
            foreach (XmlNode xn in xnList)
            {
                x = xn.InnerText;
            }
           // x = x.Substring(7, 1);

            return Content(x);
        }

    }
}