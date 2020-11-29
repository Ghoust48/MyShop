using System.ComponentModel.DataAnnotations;

namespace MyShop.Models
{
    public class Battery
    {
        public int Id { get; set; }
        
        /// <summary>
        /// Тип аккумулятора
        /// </summary>
        [Display(Name = "Тип аккумулятора")]
        public string Type { get; set; }

        /// <summary>
        /// Емкость аккумулятора
        /// </summary>
        [Display(Name = "Емкость аккумулятора")]
        public string Capacity { get; set; }

        /// <summary>
        /// Макс. время работы
        /// </summary>
        [Display(Name = "Макс. время работы")]
        public float MaxWorkingHours { get; set; }
    }
}