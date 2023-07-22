using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

using CinimaServer.Models;
using CinimaServer.Models.HomeCinimaVM;

namespace CinimaServer.Api
{
    public class MoviesController : ApiController
    {
        HomeCinemaEntities db = new HomeCinemaEntities();
        // GET: Movies
        [HttpPost]
        public MovieVM CreateMovie(MovieVM movieVM)
        {
            Movie mv = db.Movies.SingleOrDefault(n => n.ID == movieVM.ID);
            if(mv!=null)
            {
                return null;
            }
            Movie movie = new Movie();
            movie.ID = movieVM.ID;
            movie.Title = movieVM.Title;
            movie.Description = movieVM.Description;
            if (db.Genres.Any(n => n.ID == movieVM.GenreId))
            {
                movie.GenreId = movieVM.GenreId;
            }
            else
            {
                movie.GenreId = "1";
            }
            movie.Image = movieVM.ID + "." + movieVM.Image.Split('.')[movieVM.Image.Split('.').Length-1];
            movie.Director = movieVM.Director;
            movie.Writer = movieVM.Writer;
            movie.Producer = movieVM.Producer;
            movie.ReleaseDate = movieVM.ReleaseDate;
            movie.Rating = movieVM.Rating;
            movie.TrailerURI = movieVM.TrailerURI;
            movie.GroupID = movieVM.GroupID;
            db.Movies.Add(movie);
            db.SaveChanges();
            return movieVM;
        }
        [HttpPost]
        public Boolean UploadFile()
        {

            HttpResponseMessage response = new HttpResponseMessage();
            //Kiểm tra xem có file cũ không để xóa
            var httpRequest = HttpContext.Current.Request;
            var file = httpRequest.Files["Files"];
            var movieID = httpRequest.Form["ID"];
            //xóa file cũ nếu có
            string mapPath = HttpContext.Current.Server.MapPath("/images/movies/");
            string delFile = mapPath + movieID;
            bool flag = DeleteFileLocalServer(delFile);
            //Lấy type của file
            string type = file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
            //Đổi tên file và save file
            string fileName = movieID + "." + type;
            UploadFileLocalServer(file, mapPath, mapPath, fileName);
            return true;
        }
        public bool UploadFileLocalServer(HttpPostedFile Files,string mapPath, string path, string fileName)
        {

            // Get the complete folder path and store the file inside it.
            try
            {
                path = Path.Combine(mapPath, fileName);
                Files.SaveAs(path);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool DeleteFileLocalServer(string path)
        {
            string fullPath = path;
            if (File.Exists(fullPath + ".png"))
            {
                string fullPath1 = fullPath+".png";
                File.Delete(fullPath1);
            }
            if (File.Exists(fullPath + ".jpg"))
            {
               string fullPath2 = fullPath + ".jpg";
                File.Delete(fullPath2);
            }
            if (File.Exists(fullPath + ".jpeg"))
            {
                string fullPath3 = fullPath + ".jpeg";
                File.Delete(fullPath3);
            }

            return true;
        }
        [HttpPut]
        public MovieVM EditMovie(MovieVM movieVM)
        {
            Movie movie = db.Movies.SingleOrDefault(n => n.ID == movieVM.ID);
            if (movie == null)
            {
                return null;
            }      
            movie.Title = movieVM.Title;
            movie.Description = movieVM.Description;
            if (db.Genres.Any(n => n.ID == movieVM.GenreId))
            {
                movie.GenreId = movieVM.GenreId;
            }
            else
            {
                movie.GenreId = "1";
            }
            movie.Image = movieVM.ID + "." + movieVM.Image.Split('.')[movieVM.Image.Split('.').Length - 1];
            movie.Director = movieVM.Director;
            movie.Writer = movieVM.Writer;
            movie.Producer = movieVM.Producer;
            movie.ReleaseDate = movieVM.ReleaseDate;
            movie.Rating = movieVM.Rating;
            movie.TrailerURI = movieVM.TrailerURI;
            movie.GroupID = movieVM.GroupID;
            db.SaveChanges();
            return movieVM;
        }
        [HttpDelete]
        public bool DeleteMovie(int id)
        {
            Movie movie = db.Movies.SingleOrDefault(n => n.ID == id);
            if (movie != null)
            {
                db.Movies.Remove(movie);
                db.SaveChanges();
                return true;
            }
            return false;
        }


    }
}