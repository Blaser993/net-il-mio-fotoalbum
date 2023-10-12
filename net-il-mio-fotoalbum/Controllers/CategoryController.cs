using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using net_il_mio_fotoalbum.Database;
using net_il_mio_fotoalbum.Models;


namespace net_il_mio_fotoalbum.Controllers
{

    [Authorize(Roles = "ADMIN")]
    public class CategoryController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            using (FotoContext db = new FotoContext())
            {
                List<Category> categories = db.Categories.ToList<Category>();

                return View("index", categories);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", category);
            }

            using (FotoContext db = new FotoContext())
            {
                Category categoryToCreate = new Category();
                categoryToCreate.Name = category.Name;


                db.Categories.Add(categoryToCreate);
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
                Category categoryToEdit = db.Categories.Where(category => category.CategoryId == id).FirstOrDefault();

                if (categoryToEdit == null)
                {
                    return NotFound();
                }
                else
                {
                    return View(categoryToEdit);
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, Category category)
        {
            if (!ModelState.IsValid)
            {
                return View("Update", category);
            }

            using (FotoContext db = new FotoContext())
            {
                Category categoryToUpdate = db.Categories.Where(category => category.CategoryId == id).FirstOrDefault();

                if (categoryToUpdate != null)
                {
                    categoryToUpdate.Name = category.Name;

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
                Category categoryToDelete = db.Categories.Where(category => category.CategoryId == id).FirstOrDefault();

                if (categoryToDelete != null)
                {
                    db.Categories.Remove(categoryToDelete);

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
    


