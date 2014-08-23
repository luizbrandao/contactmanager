using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ContactManager.Services;
using ContactManager.Models;

namespace ContactManager.Controllers
{
    public class AlbumController : ApiController
    {
        private AlbumRepository repo;

        public AlbumController()
        {
            this.repo = new AlbumRepository();
        }

        // GET api/album
        public Album[] Get()
        {
            return repo.GetAllContacts();
        }

        // GET api/album/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/album
        public HttpResponseMessage Post(Album album)
        {
            this.repo.SaveAlbum(album);

            var response = Request.CreateResponse<Album>(System.Net.HttpStatusCode.Created, album);

            return response;
        }

        // PUT api/album/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/album/5
        public void Delete(int id)
        {
        }
    }
}
