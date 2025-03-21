namespace AIJobCoaches.Domain.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class QuizResponse
    {
        [Key]
        public int QuizResponseID { get; set; }

        public int QuizResultID { get; set; }
        public virtual QuizResult QuizResult { get; set; }

        public string Question { get; set; } // Texto de la pregunta

        public string SelectedOption { get; set; } // Opción seleccionada por el usuario

        public string CorrectOption { get; set; } // Respuesta correcta

        public bool IsCorrect { get; set; } // Indica si la respuesta fue correcta
    }
}
