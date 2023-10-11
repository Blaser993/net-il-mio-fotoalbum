using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace net_il_mio_fotoalbum.Models
{

    public class Foto
    {
        [Key]
        public int FotoId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public bool Visibility { get; set; }


        public List<Category>? Categories { get; set; }

        public Foto() { }

        public Foto(string title, string image, bool visibility)
        {
            this.Title = title;
            this.Image = image;
            this.Visibility = true;
        }

    }
}
