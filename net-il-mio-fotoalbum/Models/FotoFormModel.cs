using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace net_il_mio_fotoalbum.Models
{
    public class FotoFormModel
    {
        public IFormFile? ImageFormFile { get; set; }



        public Foto? Foto { get; set; }

        //relazioni
   
        public List<SelectListItem>?  Categories { get; set; }

        [Required(ErrorMessage = "devi selezionare almeno una categoria")]
        public List<string>? SelectedCategoriesId { get; set; }



    }
}
