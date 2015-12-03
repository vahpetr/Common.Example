using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OlegTask.Models
{
    /// <summary>
    /// Автомобиль
    /// </summary>
    [DisplayName("Автомобили")]
    public class Car
    {
        public Car()
        {
            Drivers = new HashSet<Driver>();
        }

        /// <summary>
        /// Идентификационный номер
        /// </summary>
        [Display(Name = "№")]
        public int Id { get; set; }

        /// <summary>
        /// Номер
        /// </summary>
        [Display(Name = "Номер")]
        [Required, MaxLength(32)]
        public string Number { get; set; }

        /// <summary>
        /// Марка
        /// </summary>
        [Display(Name = "Марка")]
        [Required, MaxLength(64)]
        public string Brand { get; set; }

        /// <summary>
        /// Модель
        /// </summary>
        [Display(Name = "Модель")]
        [Required, MaxLength(64)]
        public string Model { get; set; }

        /// <summary>
        /// Цвет
        /// </summary>
        [Display(Name = "Цвет")]
        [Required, MaxLength(64)]
        public string Color { get; set; }

        /// <summary>
        /// Водители
        /// </summary>
        [Display(Name = "Водители")]
        public virtual ICollection<Driver> Drivers { get; set; }
    }
}