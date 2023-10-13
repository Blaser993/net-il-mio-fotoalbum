using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using net_il_mio_fotoalbum.Database;
using net_il_mio_fotoalbum.Models;

namespace net_il_mio_fotoalbum.Controllers
{
    [Authorize(Roles = "ADMIN,USER")]
    public class FotoController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            using(FotoContext db = new FotoContext())
            {

                List<Foto> fotos = db.Fotos.Include(foto => foto.Categories).ToList<Foto>();


                return View("Index", fotos);

            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(FotoFormModel data)
        { 
            if(!ModelState.IsValid)
            {
                using (FotoContext db = new FotoContext())
                {

                List<SelectListItem> allCategorySelectList = new List<SelectListItem>();
                List<Category> databaseCategories = db.Categories.ToList();


                    foreach(Category category in databaseCategories)
                    {

                        allCategorySelectList.Add(new SelectListItem()
                        {                          
                            Text = category.Name,
                            Value = category.CategoryId.ToString(),
                        });

                    }
                    data.Categories = allCategorySelectList;

                    return View("Create", data);

                }
            }

            if (data.Foto == null)
            {
                data.Foto = new Foto(); // inizializza l'oggetto Foto se è null
            }

            if (data.Foto.Categories == null)
            {
                data.Foto.Categories = new List<Category>(); // inizializzare la lista di ingredienti se è null
            }


            if(data.SelectedCategoriesId != null)
            {

                using (FotoContext db = new FotoContext())
                {
                    
                    foreach (string categorySelectedId in data.SelectedCategoriesId)
                    {
                       
                        int intCategoriesSelectId = int.Parse(categorySelectedId);

                        Category? categoryInDb = db.Categories.Where(category => category.CategoryId == intCategoriesSelectId).FirstOrDefault();

                        if (categoryInDb != null)
                        {
                            data.Foto.Categories.Add(categoryInDb);
                        }
                       
                         
                    }

                    this.SetImageFileFromFormFile(data);

                    db.Fotos.Add(data.Foto);
                    db.SaveChanges();                  
                }                
            }
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        public IActionResult Create()
        {
            using (FotoContext db = new FotoContext())
            {

                List<Foto> fotos = db.Fotos.ToList<Foto>();


                List<SelectListItem> allCategorySelectList = new List<SelectListItem>();
                List<Category> databaseCategories = db.Categories.ToList();

                foreach (Category category in databaseCategories)
                {
                    allCategorySelectList.Add(
                        new SelectListItem
                        {
                            Text = category.Name,
                            Value = category.CategoryId.ToString()
                        });
                }

                FotoFormModel model = new FotoFormModel
                {
                    Foto = new Foto(),
                    Categories = allCategorySelectList
                };

                return View("Create", model);

            }
        }

        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        public IActionResult Update(int id)
        {
            using (FotoContext db = new FotoContext())
            {
                Foto? fotoToEdit = db.Fotos.Where(foto => foto.FotoId == id).Include(foto => foto.Categories).FirstOrDefault();

                if (fotoToEdit == null)
                {
                    return NotFound();
                }
                else
                {
                    List<Category> dbCategoryList = db.Categories.ToList();
                    List<SelectListItem> selectListItem = new List<SelectListItem>(); 

                    foreach (Category category in dbCategoryList)
                    {
                        selectListItem.Add(new SelectListItem
                        { 
                        Value = category.CategoryId.ToString(), 
                        Text = category.Name,
                        Selected = fotoToEdit.Categories.Any(tagAssociated => tagAssociated.CategoryId == category.CategoryId)
                        });
                    }

                    FotoFormModel model =
                        new FotoFormModel { Foto = fotoToEdit, Categories = selectListItem };
                    return View("Update",model);
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, FotoFormModel data)
        {
            if (!ModelState.IsValid)
            {
                using (FotoContext db = new FotoContext())
                {
                    List<Category> categories = db.Categories.ToList();
                    List<SelectListItem> selectListItem = new List<SelectListItem>();

                    foreach (Category category in categories)
                    {
                        selectListItem.Add(new SelectListItem
                        {
                            Value =category.CategoryId.ToString(),
                            Text = category.Name,
                        });
                    }

                    data.Categories = selectListItem;

                    return View("Update", data);
                }

                
            }

            using (FotoContext db = new FotoContext())
            {
                Foto? fotoToUpdate = db.Fotos
                    .Where(foto => foto.FotoId == id)
                    .Include(foto => foto.Categories)
                    .FirstOrDefault();

                if (fotoToUpdate != null)
                {
                    fotoToUpdate.Categories.Clear();

                    fotoToUpdate.Title = data.Foto.Title;
                    fotoToUpdate.ImageUrl = data.Foto.ImageUrl;
                    fotoToUpdate.Description = data.Foto.Description;
                    fotoToUpdate.Visibility = data.Foto.Visibility;

                    // Verifica se data.Foto.Categories è null e, se non lo è, assegnalo a fotoToUpdate.Categories
                    if (data.Foto.Categories != null)
                    {
                        fotoToUpdate.Categories = data.Foto.Categories;
                    }

                    if (data.SelectedCategoriesId != null)
                    {
                        foreach (string categorySelectedId in data.SelectedCategoriesId)
                        {
                            int intCategorySelectedId = int.Parse(categorySelectedId);

                            Category? categoryInDb = db.Categories
                                .Where(category => category.CategoryId == intCategorySelectedId)
                                .FirstOrDefault();

                            if (categoryInDb != null)
                            {
                                fotoToUpdate.Categories.Add(categoryInDb);
                            }
                        }
                    }

                    if (data.ImageFormFile != null)
                    {
                        MemoryStream stram = new MemoryStream();
                        data.ImageFormFile.CopyTo(stram);
                        fotoToUpdate.ImageFile = stram.ToArray();
                    }

                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {
                    return NotFound();
                }
            }
        }


        [Authorize(Roles = "ADMIN")]
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



        private void SetImageFileFromFormFile(FotoFormModel formdata)
        {
            if(formdata.ImageFormFile == null)
            {
                return;
            }

            MemoryStream stream = new MemoryStream();

            formdata.ImageFormFile.CopyTo(stream);
            formdata.Foto.ImageFile = stream.ToArray();
        }

    }
}
