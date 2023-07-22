using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using CinimaServer.Models;
using CinimaServer.Models.ViewModel;
using AutoMapper;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using System.Text;

namespace CinimaServer.Api
{
    public class MovieController : ApiControllerBase
    {

        // GET: api/movie/getmovie
        HomeCinemaEntities db = new HomeCinemaEntities();
        //[EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
        public HttpResponseMessage GetMovie()
        {
            var lst = db.Movies;
            List<MovieViewModel> lstModel = new List<MovieViewModel>();
            MovieViewModel movie;
            foreach (var item in lst)
            {
                movie = new MovieViewModel();
                movie.ID = item.ID;
                movie.Title = item.Title;
                movie.Description = item.Description;
                movie.Image = item.Image;
                movie.GenreName = item.Genre.Name;
                movie.Director = item.Director;
                movie.Writer = item.Writer;
                movie.Producer = item.Producer;
                movie.ReleaseDate = item.ReleaseDate.Value;
                movie.Rating = item.Rating.Value;
                movie.TrailerURI = item.TrailerURI;

                movie.Genre = item.Genre.Name;
                lstModel.Add(movie);
            }
            var respone = Request.CreateResponse(HttpStatusCode.OK,lstModel);
         
            return respone;
        }
        
        //[EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
        public HttpResponseMessage GetMoviePage(int page=1, int pageSize=10 )
        {
            var lst = db.Movies;
            List<MovieViewModel> lstModel = new List<MovieViewModel>();
            MovieViewModel movie;
            foreach (var item in lst)
            {
                movie = new MovieViewModel();
                movie.ID = item.ID;
                movie.Title = item.Title;
                movie.Description = item.Description;
                movie.Image = item.Image;
                movie.GenreName = item.Genre.Name;
                movie.Director = item.Director;
                movie.Writer = item.Writer;
                movie.Producer = item.Producer;
                movie.ReleaseDate = item.ReleaseDate.Value;
                movie.Rating = item.Rating.Value;
                movie.TrailerURI = item.TrailerURI;
                movie.Genre = item.Genre.Name;
                lstModel.Add(movie);
            }
            int totalRow = lstModel.Count;
            var model = lstModel.OrderBy(x => x.Title).Skip(page * pageSize).Take(pageSize);
            PaginationSet<MovieViewModel> pagedSet = new PaginationSet<MovieViewModel>()
            {
                Page = page,
                TotalCount = totalRow,
                TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize),
                Items = model
            };
            var respone = Request.CreateResponse(HttpStatusCode.OK, pagedSet);
         
            return respone;
        }
        
    // GET: api/Movie/5
        public HttpResponseMessage GetMovieDetailByGroup(int id,string groupID)
        {
            var item = db.Movies.SingleOrDefault(n => n.ID == id);
            MovieDetails movie = new MovieDetails();
            if (item != null)
            {
                movie.ID = item.ID;
                movie.Title = item.Title;
                movie.Description = item.Description;
                movie.Image = item.Image;
                movie.GenreName = item.Genre.Name;
                movie.Director = item.Director;
                movie.Writer = item.Writer;
                movie.Producer = item.Producer;
                movie.ReleaseDate = item.ReleaseDate.Value;
                movie.Rating = item.Rating.Value;
                movie.TrailerURI = @"https://www.youtube.com/embed/" + item.TrailerURI.Substring(32);
                movie.Genre = item.Genre.Name;
                var showTimes = db.CinimaRoom_Movie.Where(n => n.MovieID == item.ID && n.GroupID == groupID);
                if (showTimes.Count() > 0)
                {
                    List<ShowTimes> ShowTimes = new List<ShowTimes>();
                    ShowTimes st;
                    foreach (var it in showTimes)
                    {
                        st = new ShowTimes();
                        st.ShowTimeID = it.CinimaRoomMovieID;
                        st.CinimaRoomID = it.CinimaRoomID;
                        st.MovieID = it.MovieID;
                        st.StartDate = it.StartDate;
                        st.EndDate = it.EndDate;
                        st.Price = it.Price;
                        st.DateReady = it.DateReady;
                        st.GroupID = it.GroupID;
                        CinimaRooms room = new CinimaRooms();
                        CinimaRoom r = db.CinimaRooms.SingleOrDefault(n => n.CinimaRoomID == it.CinimaRoomID && n.GroupID == groupID);
                        if (r != null)
                        {
                            room.CinimaRoomID = r.CinimaRoomID;
                            room.CinimaRoomName = r.CinimaRoomName;
                            room.GroupID = r.GroupID;
                            room.NumberOfSeats = r.NumberOfSeats;
                        }
                        st.CinimaRoom = room;
                        ShowTimes.Add(st);
                    }
                    movie.ShowTimes = ShowTimes;
                }

            }
            var respone = Request.CreateResponse(HttpStatusCode.OK, movie);
            return respone;
        }

        public HttpResponseMessage GetMovieDetail(int id)
        {
            var item = db.Movies.SingleOrDefault(n => n.ID == id);
            MovieDetails movie = new MovieDetails();
            if (item != null)
            {
                movie.ID = item.ID;
                movie.Title = item.Title;
                movie.Description = item.Description;
                movie.Image = item.Image;
                movie.GenreName = item.Genre.Name;
                movie.Director = item.Director;
                movie.Writer = item.Writer;
                movie.Producer = item.Producer;
                movie.ReleaseDate = item.ReleaseDate.Value;
                movie.Rating = item.Rating.Value;
                movie.TrailerURI = item.TrailerURI;
                movie.Genre = item.Genre.Name;


                var showTimes = db.CinimaRoom_Movie.Where(n => n.MovieID == item.ID );
                if (showTimes.Count() > 0)
                {
                    List<ShowTimes> ShowTimes = new List<ShowTimes>();
                    ShowTimes st;
                    foreach (var it in showTimes)
                    {
                        st = new ShowTimes();
                        st.ShowTimeID = it.CinimaRoomMovieID;
                        st.CinimaRoomID = it.CinimaRoomID;
                        st.MovieID = it.MovieID;
                        st.StartDate = it.StartDate;
                        st.EndDate = it.EndDate;
                        st.Price = it.Price;
                        st.DateReady = it.DateReady;
                        st.GroupID = it.GroupID;
                        CinimaRooms room = new CinimaRooms();
                        CinimaRoom r = db.CinimaRooms.SingleOrDefault(n => n.CinimaRoomID == it.CinimaRoomID );
                        if (r != null)
                        {
                            room.CinimaRoomID = r.CinimaRoomID;
                            room.CinimaRoomName = r.CinimaRoomName;
                            room.GroupID = r.GroupID;
                            room.NumberOfSeats = r.NumberOfSeats;
                        }
                        st.CinimaRoom = room;
                        ShowTimes.Add(st);
                    }
                    movie.ShowTimes = ShowTimes;
                }

            }
            var respone = Request.CreateResponse(HttpStatusCode.OK, movie);
            return respone;
        }

        public IHttpActionResult GetCinimaRoomDetail(int ShowTimeID)
        {
            var LichChieu = db.CinimaRoom_Movie.SingleOrDefault(n => n.CinimaRoomMovieID == ShowTimeID);
            var lstDanhSachGheDuocDat = db.CinimaBookings.Where(n => n.ShowTimeID == ShowTimeID);
            if (LichChieu!=null)
            {
                bool dem = false;
                CinimaRoomDetail cinimaRoomDetail = new CinimaRoomDetail();
                List<Seats> lstSeat = new List<Seats>();
                foreach (var ghe in LichChieu.CinimaRoom.Seats) //Lịch chiếu sẽ ra được rạp //Lấy mã rạp load ghế của rạp đó
                {
                    
                    dem = false;
                    //Tất cả ghế trong rạp
                    Seats seat = new Seats();
                    seat.SeatID = ghe.SeatID;
                    seat.CinimaRoomID = ghe.CinimaRoomID;
                    seat.GroupID = ghe.GroupID;
                    seat.SeatName = ghe.SeatName;
                    seat.SeatType = ghe.SeatType;
                    seat.Status = false;
                    seat.NumOrder = ghe.NumOrder;
                    seat.UserName = "no people booked";
                    seat.Price = int.Parse(LichChieu.Price);
                    foreach (var gheDaDat in lstDanhSachGheDuocDat)
                    {
                        if (ghe.SeatID == gheDaDat.SeatID)
                        {
                            seat.UserName = db.Customers.SingleOrDefault(n=>n.ID == gheDaDat.UserID).UserName;
                           
                            dem=true;
                        }
                    }
                    if (dem == true)
                    {
                        seat.Status = true;
                    }
                    lstSeat.Add(seat);
                }
                cinimaRoomDetail.Seats = lstSeat;
                cinimaRoomDetail.ShowTimeID = ShowTimeID;
                return Ok(cinimaRoomDetail);
            }
            return Ok("reference does not exist");
        }
        [HttpPost]
        public IHttpActionResult BookingTickets(JsonData data)
        {
           // ListBookingTickets lstDanhSachVeDat = new ListBookingTickets();
            JavaScriptSerializer parser = new JavaScriptSerializer();
            var lstDanhSachVeDat = parser.Deserialize<ListBookingTickets>(data.data);
            CinimaBooking booking;
            try
            {
                foreach (var item in lstDanhSachVeDat.Tickets)
                {
                    booking = new CinimaBooking();
                    booking.SeatID = item.SeatID;
                    booking.DateView = DateTime.Now;
                    booking.Price = item.Price;
                    booking.UserID = db.Customers.SingleOrDefault(n => n.UserName == lstDanhSachVeDat.UserID).ID;
                    booking.GroupID = lstDanhSachVeDat.GroupID;
                    booking.ShowTimeID = int.Parse(lstDanhSachVeDat.ShowTimeID);
                    db.CinimaBookings.Add(booking);
                }
                db.SaveChanges();
                return Ok("successfully");
            }
            catch (Exception){
                return Ok("failure");
            }
            


        }
    }
}
