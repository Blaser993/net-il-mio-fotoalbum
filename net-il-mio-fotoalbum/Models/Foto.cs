﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace net_il_mio_fotoalbum.Models
{

    public class Foto
    {
        [Key]
        public int FotoId { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public bool Visibility { get; set; }

    }
}
