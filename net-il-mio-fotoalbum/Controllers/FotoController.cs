using Microsoft.AspNetCore.Mvc;
using net_il_mio_fotoalbum.Database;
using net_il_mio_fotoalbum.Models;

namespace net_il_mio_fotoalbum.Controllers
{
    public class FotoController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View("index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Foto foto)
        { 
            if(ModelState.IsValid)
            {
                return View("Create", foto);
            }

            using (FotoContext  db = new FotoContext())
            {
                Foto fotoToCreate = new Foto();
                fotoToCreate.Title = foto.Title;
                fotoToCreate.Description = foto.Description;
                fotoToCreate.Visibility = foto.Visibility;
                fotoToCreate.Categories= foto.Categories;

                db.Fotos.Add(fotoToCreate);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View("Create");
        }
    }
}
