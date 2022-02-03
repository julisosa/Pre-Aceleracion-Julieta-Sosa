using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Pre_Aceleracion_Julieta_Sosa.Models
{
    public class Genre
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "A name is required")]
        [StringLength(20, ErrorMessage = "Name cannot be longer than 20 characters", MinimumLength = 2)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "An image is required")]
        [Display(Name = "Image")]
        public string Image { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }
    }
}
