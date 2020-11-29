using System.ComponentModel.DataAnnotations;

namespace MyShop.Models
{
    public class Memory
    {
        public int Id { get; set; }
        
        /// <summary>
        /// Оперативная память
        /// </summary>
        [Display(Name = "Оперативная память")]
        public string RAM { get; set; }

        /// <summary>
        /// Постоянная память
        /// </summary>
        [Display(Name = "Постоянная память")]
        public string PersistentMemory { get; set; }

        /// <summary>
        /// Поддержка карт памяти
        /// </summary>
        [Display(Name = "Поддержка карт памяти")]
        public bool MemoryCardSupport { get; set; }
    }
}