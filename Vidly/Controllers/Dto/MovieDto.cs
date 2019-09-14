using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Controllers.Dto
{
    public class MovieDto
    {
        public byte Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }


        [Required]
        public DateTime ReleaseDate { get; set; }

        public DateTime DateAdded { get; set; }
        public byte Stock { get; set; }

        public byte GenreId { get; set; }

    }
}