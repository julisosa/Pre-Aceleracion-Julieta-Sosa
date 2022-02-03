using System;

namespace Pre_Aceleracion_Julieta_Sosa.DTOs
{
    public class MovieDto
    {
        public int GenreId { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public DateTime CreationDate { get; set; }
        public int Qualification { get; set; }
    }
}
