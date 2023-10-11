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
            using(FotoContext db = new FotoContext())
            {

                List<Foto> fotos = db.Fotos.ToList<Foto>();


                return View("index", fotos);

            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Foto foto)
        { 
            if(!ModelState.IsValid)
            {
                return View("Create", foto);
            }

            using (FotoContext  db = new FotoContext())
            {
                Foto fotoToCreate = new Foto();
                fotoToCreate.Title = foto.Title;
                fotoToCreate.Image = foto.Image;
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

        [HttpGet]
        public IActionResult Update(int id)
        {
            using (FotoContext db = new FotoContext())
            {
                Foto fotoToEdit = db.Fotos.Where(foto => foto.FotoId == id).FirstOrDefault();

                if (fotoToEdit == null)
                {
                    return NotFound();
                }
                else
                {
                    return View(fotoToEdit);
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, Foto foto)
        {
            if (!ModelState.IsValid)
            {
                return View("Update", foto);
            }

            using (FotoContext db = new FotoContext())
            {
                Foto fotoToUpdate = db.Fotos.Where(foto => foto.FotoId == id).FirstOrDefault();

                if(fotoToUpdate != null)
                {
                    fotoToUpdate.Title = foto.Title;
                    fotoToUpdate.Image = foto.Image;
                    fotoToUpdate.Description = foto.Description;
                    fotoToUpdate.Visibility = foto.Visibility;
                    fotoToUpdate.Categories = foto.Categories;

                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {
                    return NotFound();
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            using (FotoContext db = new FotoContext())
            {
                Foto fotoToDelete = db.Fotos.Where(foto => foto.FotoId == id).FirstOrDefault();

                if(fotoToDelete != null)
                {
                    db.Fotos.Remove(fotoToDelete);

                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {
                    return NotFound();
                }
            }
        }

    }
}
