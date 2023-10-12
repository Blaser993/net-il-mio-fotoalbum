using System.ComponentModel.DataAnnotations;

namespace net_il_mio_fotoalbum.Models
{
    public class Message
    {
        [Key]
        [Required]
        [EmailAddress]
        public string Mail { get; set; }

        [Required]
        [StringLength(1000, ErrorMessage = "Non puoi scrivere un messaggio più lungo di mille caratteri")]
        public string Text { get; set; }


        public Message(string Mail, string Text)
        {
            this.Mail = Mail;
            this.Text = Text;
        }

        public Message() { }    
     
    }
}
