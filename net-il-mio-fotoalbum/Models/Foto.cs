using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace net_il_mio_fotoalbum.Models
{

    public class Foto
    {
        [Key]
        public int FotoId { get; set; }

        [Required(ErrorMessage ="il campo è obbligatorio")]
        [StringLength(50, ErrorMessage ="Il titolo non può essere più lungo di 50 caratteri")]
        public string Title { get; set; }

        [StringLength(200, ErrorMessage = "La descrizione non può essere più lungo di 200 caratteri")]
        public string Description { get; set; }

        [MaxLength(500, ErrorMessage = "La lunghezza del link non deve superare i 500 caratteri")]
        public string? ImageUrl { get; set; }


        public byte[]? ImageFile { get; set; }

        public string ImageSrc =>
            ImageFile is null ? (ImageUrl is null ? "" : ImageUrl) : $"data:image/png;base64,{Convert.ToBase64String(ImageFile)}";


 

        public bool Visibility { get; set; }

  
        public List<Category>? Categories { get; set; }

        public Foto() { }

        public Foto(string title, string image, bool visibility)
        {
            this.Title = title;
            this.ImageUrl = image;
            this.Visibility = true;
        }

    }
}
