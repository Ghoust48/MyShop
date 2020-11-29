using System.ComponentModel.DataAnnotations;

namespace MyShop.Models
{
    public class Screen
    {
        public int Id { get; set; }
        
        /// <summary>
        /// Диагональ экрана
        /// </summary>
        [Display(Name = "Диагональ экрана")]
        public string Diagonal { get; set; }

        /// <summary>
        /// Разрешение экрана
        /// </summary>
        [Display(Name = "Разрешение экрана")]
        public string Resolution { get; set; }

        /// <summary>
        /// Тип матрицы
        /// </summary>
        [Display(Name = "Тип матрицы")]
        public string MatrixType { get; set; }

        /// <summary>
        /// Поверхность экрана
        /// </summary>
        [Display(Name = "Поверхность экрана")]
        public string Surface { get; set; }

        /// <summary>
        /// Мультитач
        /// </summary>
        [Display(Name = "Мультитач")]
        public bool Multitouch { get; set; }
    }
}