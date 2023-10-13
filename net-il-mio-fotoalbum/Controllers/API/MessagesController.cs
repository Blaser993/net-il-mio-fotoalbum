using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using net_il_mio_fotoalbum.Database;
using net_il_mio_fotoalbum.Models;

namespace net_il_mio_fotoalbum.Controllers.API
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MessagesController : Controller
    {
        private FotoContext _myDb;

        public MessagesController(FotoContext myDb)
        {
            _myDb = myDb;
        }


        [HttpPost]
        public IActionResult SendData([FromBody] Message data)
        {
            try
            {
                _myDb.Messages.Add(data);

                 _myDb.SaveChanges();

                return Ok("Messaggio salvato con successo nel database");
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return StatusCode(500, "Errore durante il salvataggio del messaggio nel database");
            }
        }

    }
}
