using System.ComponentModel.DataAnnotations;

namespace MyShop.Models
{
    /// <summary>
    /// Корпус
    /// </summary>
    [Display(Name = "Корпус")]
    public class Housing
    {
        public int Id { get; set; }
        
        /// <summary>
        /// Конструкция
        /// </summary>
        [Display(Name = "Конструкция")]
        public string Design { get; set; }

        /// <summary>
        /// Материал корпуса
        /// </summary>
        [Display(Name = "Материал корпуса")]
        public string BodyMaterial { get; set; }

        /// <summary>
        /// Конструкция стекла
        /// </summary>
        [Display(Name = "Конструкция стекла")]
        public string GlassConstruction { get; set; }

        /// <summary>
        /// Цвет
        /// </summary>
        [Display(Name = "Цвет")]
        public string Colour { get; set; }
    }
}