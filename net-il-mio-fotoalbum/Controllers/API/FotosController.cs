using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using net_il_mio_fotoalbum.Database;
using net_il_mio_fotoalbum.Models;

namespace net_il_mio_fotoalbum.Controllers.API
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FotosController : ControllerBase
    {

        private FotoContext _myDb;

        public FotosController(FotoContext myDb)
        {
            _myDb = myDb;
        }

        [HttpGet]
        public IActionResult GetFotos()
        {

            List<Foto> fotos = _myDb.Fotos.Include(foto => foto.Categories).ToList();
            return Ok(fotos.ToList());

        }


        [HttpGet]
        public IActionResult SearchFotos(string? search)
        {
            if (search == null)
            {
                return BadRequest(new { Message = "Non hai inserito nesssuna stringa di ricerca" });
            }


            List<Foto> foundFotos = _myDb.Fotos.Where(foto => foto.Title.ToLower().Contains(search.ToLower())).ToList();
            return Ok(foundFotos);

        }


    }
}
