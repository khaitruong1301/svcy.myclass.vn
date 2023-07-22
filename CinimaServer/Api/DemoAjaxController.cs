using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using CinimaServer.Models;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

//using Microsoft.AspNet.WebApi.Cors;
namespace CinimaServer.Api
{
    
    [RoutePrefix("api/ajax")]
    public class DemoAjaxController : ApiControllerBase
    {
        // GET: api/DemoAjax
        [Route("getmethod")]
        public IEnumerable<Models.ViewModel.MovieViewModel> Get()
        {
            return null;
        }

        // GET: api/DemoAjax/5
        public string Get(int id)
        {
            return "[ajax_data"+id+"]";
        }

        // POST: api/DemoAjax

        [HttpPost]
       // [AllowCrossSiteJson]
        //[EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "X-My-Header")]
        [Route("postmethod")]
        //public abc Post(abc param)
        //{
        //    //var context = HttpContext.Current;
        //    //string callback = context.Request.Params["callback"];
        //    //string json = "{c: " + param + "}";//Newtonsoft.Json.JsonConvert.SerializeObject(...);
        //    //string response = string.IsNullOrEmpty(callback) ? json : string.Format("{0}({1});", callback, json);

        //    //// Response
        //    //context.Response.ContentType = "application/json";
        //    //context.Response.Write(response);
        //    return param;
        //}

        // PUT: api/DemoAjax/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/DemoAjax/5
        public void Delete(int id)
        {
        }
    }
}
