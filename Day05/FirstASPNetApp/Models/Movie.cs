using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FirstASPNetApp.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [StringLength(60, MinimumLength = 3)]
        public string Title { get; set; }
        [DisplayName("Release Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ReleaseDate { get; set; }
        [RegularExpression(@"^[A-Z]+[a-zA-Z]*$")]
        [Required]
        [StringLength(30)]
        public string Genre { get; set; }
        [Range(1,200)]
        public decimal Price { get; set; }
        [RegularExpression(@"^[A-Z]+[a-zA-Z]*$")]
        [StringLength(5)]
        public string Rating { get; set; }
    }
}