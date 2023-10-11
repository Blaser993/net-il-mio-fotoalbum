using System.ComponentModel.DataAnnotations;

namespace net_il_mio_fotoalbum.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public string Name { get; set; }

        public List<Foto>? Fotos { get; set; }


        public Category() { }

        public Category(string name)
        {
            this.Name = name;
        }
    }
}
