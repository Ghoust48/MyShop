using System.ComponentModel.DataAnnotations;

namespace MyShop.Models
{
    public class Processor
    {
        public int Id { get; set; }
        
        /// <summary>
        /// Платформа
        /// </summary>
        [Display(Name = "Платформа")]
        public string Platform { get; set; }

        /// <summary>
        /// Тип процессора
        /// </summary>
        [Display(Name = "Тип процессора")]
        public string ProcessorType { get; set; }

        /// <summary>
        /// Тактовая частота процессора
        /// </summary>
        [Display(Name = "Тактовая частота процессора")]
        public string ClockSpeed { get; set; }

        /// <summary>
        /// Количество ядер
        /// </summary>
        [Display(Name = "Количество ядер")]
        public int Cores { get; set; }

        /// <summary>
        /// Микроархитектура ЦПУ
        /// </summary>
        [Display(Name = "Микроархитектура ЦПУ")]
        public string Microarchitecture { get; set; }

        /// <summary>
        /// Техпроцесс
        /// </summary>
        [Display(Name = "Техпроцесс")]
        public string TechnicalProcess { get; set; }

        /// <summary>
        /// Графический ускоритель
        /// </summary>
        [Display(Name = "Графический ускоритель")]
        public string GraphicsAccelerator { get; set; }

        /// <summary>
        /// Частота графического процессора
        /// </summary>
        [Display(Name = "Частота графического процессора")]
        public string GPUFrequency { get; set; }
    }
}