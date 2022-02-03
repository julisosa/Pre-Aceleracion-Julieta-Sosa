using System.ComponentModel.DataAnnotations;

namespace Pre_Aceleracion_Julieta_Sosa.Models
{
    public class Character
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "An image is required")]
        public string Image { get; set; }

        [Required(ErrorMessage = "A name is required")]
        [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters", MinimumLength = 2)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Age is required")]
        public int Age { get; set; }

        public int Weight { get; set; }

        [Required(ErrorMessage = "A story is required")]
        [StringLength(50, ErrorMessage = "Story cannot be longer than 100 characters", MinimumLength = 10)]
        public string Story { get; set; }

        public Movie Movie { get; set; }
    }
}
