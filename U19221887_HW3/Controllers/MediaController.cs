using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using U19221887_HW3.Models;

namespace U19221887_HW3.Controllers
{
    public class MediaController : Controller
    {
        // GET: Media
        //Create different view for navbar

        [HttpGet] // method to get
        public ActionResult Home()
        {
            return View();
        }

        [HttpPost] // method to pass
        public ActionResult Home(HttpPostedFileBase ReceiveFile, FormCollection collection)
        {

            //Receive from radio button from form collection
            string value = Convert.ToString(collection["optradio"]);

            //see if the checked options aligned
            if (value == "Document")

            {
                //fetching from file path
                ReceiveFile.SaveAs(Path.Combine(HttpContext.Server.MapPath("~/Content/MediaFolder/Documents"), ReceiveFile.FileName));
            }
            else if (value == "Image")
            {
                ReceiveFile.SaveAs(Path.Combine(HttpContext.Server.MapPath("~/Content/MediaFolder/Images"), ReceiveFile.FileName));
            }
            else
            {
                ReceiveFile.SaveAs(Path.Combine(HttpContext.Server.MapPath("~/Content/MediaFolder/Videos"), ReceiveFile.FileName));
            }
            return RedirectToAction("Home");
        }



        public ActionResult Files()
        {
            List<FileModel> files = new List<FileModel>();

            string[] Documents = Directory.GetFiles(Server.MapPath("~/Content/MediaFolder/Documents"));
            string[] Images = Directory.GetFiles(Server.MapPath("~/Content/MediaFolder/Images"));
            string[] Videos = Directory.GetFiles(Server.MapPath("~/Content/MediaFolder/Videos"));



            foreach (var ReceiveFile in Documents)
            {
                FileModel locatedFile = new FileModel();
                locatedFile.FileName = Path.GetFileName(ReceiveFile);
                files.Add(locatedFile);
            }
            foreach (var ReceiveFile in Images)
            {
                FileModel locatedFile = new FileModel();
                locatedFile.FileName = Path.GetFileName(ReceiveFile);
                files.Add(locatedFile);
            }
            foreach (var ReceiveFile in Videos)
            {
                FileModel locatedFile = new FileModel();
                locatedFile.FileName = Path.GetFileName(ReceiveFile);
                files.Add(locatedFile);
            }

            return View(files);
        }

        public FileResult DownloadFile(string fileName)
        {
            // to check if file exists 
            byte[] bytes = null;
            string document = Server.MapPath("~/Content/MediaFolder/Documents") + fileName;
            string images = Server.MapPath("~/Content/MediaFolder/Images") + fileName;
            string videos = Server.MapPath("~/Content/MediaFolder/Videos") + fileName;

            // to read the statements 
            if (fileName == "document")
            {
                bytes = System.IO.File.ReadAllBytes(Server.MapPath("~/Content/MediaFolder/Documents/"));
            }
            else if (fileName == "images")
            {
                bytes = System.IO.File.ReadAllBytes(Server.MapPath("~/Content/MediaFolder/Images/"));
            }
            else
            {
                bytes = System.IO.File.ReadAllBytes(Server.MapPath("~/Content/MediaFolder/Videos/"));
            }

            return File(bytes, fileName);
        }

        public ActionResult DeleteFile(string fileName)
        {
            //action is to delete
            string location = null;

            if (fileName == "document")
            {
                location = Server.MapPath("~/Content/Media/Documents/");
            }
            else if (fileName == "images")
            {
                location = Server.MapPath("~/Content/Media/Images/");
            }
            else
            {
                location = Server.MapPath("~/Content/Media/Video/");
            }

            System.IO.File.Delete(location);
            return RedirectToAction("Home");
        }


        public ActionResult Images()
        {
            //delare list
            List<FileModel> Images = new List<FileModel>();
            //read from images
            string[] ReadImage = Directory.GetFiles(Server.MapPath("~/Content/MediaFolder/Images"));
            foreach (var Receivefile in ReadImage)
            {
                FileModel locatedFile = new FileModel();
                locatedFile.FileName = Path.GetFileName(Receivefile);
                Images.Add(locatedFile);
            }
            return View(Images);
        }

        public ActionResult Videos()
        {
            List<FileModel> Videos = new List<FileModel>();
            string[] ReadVideo = Directory.GetFiles(Server.MapPath("~/Content/MediaFolder/Videos"));
            foreach (var Receivefile in ReadVideo)
            {
                FileModel locatedFile = new FileModel();
                locatedFile.FileName = Path.GetFileName(Receivefile);
                Videos.Add(locatedFile);
            }
            return View(Videos);
        }


        public ActionResult AboutMe()
        {
            return View();
        }

    }
}




    