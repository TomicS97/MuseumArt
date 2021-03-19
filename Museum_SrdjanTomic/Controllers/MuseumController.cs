using Museum_SrdjanTomic.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Museum_SrdjanTomic.Controllers
{
    public class MuseumController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetTree()               //Get value from Tree file.
        {
            var path = Server.MapPath(@"~/JSON/tree.json");
            string jsonFile;

            using (var reader = new StreamReader(path))
            {
                jsonFile = reader.ReadToEnd();
            }
            var result = JsonConvert.DeserializeObject<Tree>(jsonFile);

            return View(result);
        }

        [HttpGet]
        public ActionResult GetCollection(int id)       //Get value from collection file.
        {
            var path = Server.MapPath(@"~/JSON/collection.json");
            string jsonFile;
            using (var reader = new StreamReader(path))
            {
                jsonFile = reader.ReadToEnd();
            }
            var json = JsonConvert.DeserializeObject<Collection>(jsonFile);

            var result = json.Collections.FirstOrDefault(x => x.Id == id);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditArt(int id)
        {
            var path = Server.MapPath(@"~/JSON/collection.json");
            string jsonFile;
            using (var reader = new StreamReader(path))
            {
                jsonFile = reader.ReadToEnd();
            }
            var json = JsonConvert.DeserializeObject<Collection>(jsonFile);

            var result = json.Collections.FirstOrDefault(x => x.Id == id);

            return View("Edit", result);
        }

        [HttpPost]
        public ActionResult SaveEditArt(string description, string url, int id, string name)    //Save edit collection
        {
            var path = Server.MapPath(@"~/JSON/collection.json");
            string jsonFile;
            using (var reader = new StreamReader(path))
            {
                jsonFile = reader.ReadToEnd();
            }
            var json = JsonConvert.DeserializeObject<Collection>(jsonFile);
            foreach (var item in json.Collections)
            {
                if (item.Id == id)
                {
                    item.Description = description;
                    item.Url = url;
                    item.Name = name;
                }
            }
            var result = JsonConvert.SerializeObject(json);
            using (var writer = new StreamWriter(path))
            {
                writer.Write(result);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Filter(string name)    
        {
            var path = Server.MapPath(@"~/JSON/tree.json");
            string jsonFile;
            using (var reader = new StreamReader(path))
            {
                jsonFile = reader.ReadToEnd();
            }
            var json = JsonConvert.DeserializeObject<Tree>(jsonFile);


            var result = new List<Tree>();

            foreach (var item in json.Collection)
            {
                //var deb = item.Collection.FirstOrDefault(x => x.Name.)
                result.AddRange(item.Collection.Where(x => x.Name.ToUpper().Contains(name.ToUpper())));
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}