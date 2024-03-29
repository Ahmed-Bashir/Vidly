﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Movie
    {
        public byte Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }


        [Required]
        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }

        public DateTime DateAdded { get; set; }


        [Display(Name = "Number in Stock")]
        [Range(1, 20, ErrorMessage = "Number of stock can only be between 1-20")]
        public byte Stock { get; set; }


        [Required(ErrorMessage = "Please select a genre")]
        [Display(Name = "Genre")]
        public byte GenreId { get; set; }


        public Genre Genre { get; set; }

   




    }
}