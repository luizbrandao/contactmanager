using System.Net.Http;
using System.Web.Http;
using ContactManager.Services;
using ContactManager.Models;

namespace ContactManager.Controllers
{
    public class AlbumController : ApiController
    {
        // Instacia de AlbumRepository
        // onde está o array com os albuns
        private AlbumRepository repo;

        public AlbumController()
        {
            // Inicializo uma instancia de AlbumRepository
            this.repo = new AlbumRepository();
        }

        // GET api/album
		[AcceptVerbs("GET")]
        public Album[] Get()
        {
            // Recupero todos os albuns no array
            return repo.GetAllAlbuns();
        }

        // GET api/album/5
        public Album[] Get(int id)
        {
            // Recupero um album em especifico, apartir de um id
            return repo.getAlbum(id.ToString());
        }

        // POST api/album
		public HttpResponseMessage Post(Album album)
        {
            //Adicino um novo album ao array
            this.repo.SaveAlbum(album);

            // Recupero a resposta obtida
            var response = Request.CreateResponse<Album>(System.Net.HttpStatusCode.Created, album);

            // retorno a resposta
            return response;
        }

        // PUT api/album/5
        public HttpResponseMessage Put(int id, Album album)
        {
            // Atualizo as informacoes do album
            this.repo.SaveAlbum(album);

            // Recupero a resposta
            var response = Request.CreateResponse<Album>(System.Net.HttpStatusCode.Created, album);

            // Retorno a Resposta
            return response;
        }

        // DELETE api/album/5
        public Album[] Delete(int id)
        {
            // Apago um determinado album do array, apartir do id
            return repo.deleteAlbum(id.ToString());
        }
    }
}
