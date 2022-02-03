using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Pre_Aceleracion_Julieta_Sosa.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "An image is required")]
        public string Image { get; set; }

        [Required(ErrorMessage = "The title is required")]
        [StringLength(50, ErrorMessage = "Title cannot be longer than 50 characters", MinimumLength = 2)]
        public string Title { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreationDate { get; set; }

        [Required(ErrorMessage = "A qualification is required")]
        public int Qualification { get; set; }

        public Genre Genre { get; set; }

        public virtual ICollection<Character> Characters { get; set; }
    }
}
