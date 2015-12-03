using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OlegTask.Models
{
    /// <summary>
    /// Водитель
    /// </summary>
    [DisplayName("Водители")]
    public class Driver
    {
        public Driver()
        {
            Cars = new HashSet<Car>();
        }

        /// <summary>
        /// Идентификационный номер
        /// </summary>
        [Display(Name = "№")]
        public int Id { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        [Display(Name = "Фамилия"), DataType(DataType.Text)]
        [Required, MaxLength(128)]
        public string Surname { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        [Display(Name = "Имя"), DataType(DataType.Text)]
        [Required, MaxLength(128)]
        public string Name { get; set; }

        //wiki
        //Серия паспорта(4 символа, цифры, пробел)
        /// <summary>
        /// Номер паспорта(6 символов, цифры)
        /// </summary>
        [Display(Name = "Номер паспорта")]
        public int PassportNumber { get; set; }

        /// <summary>
        /// День рождения
        /// </summary>
        [Display(Name = "День рождения"), Column(TypeName = "smalldatetime")]
        [Range(typeof (DateTime), "1/1/1900", "6/6/2079")]
        public DateTime BirthDay { get; set; }

        /// <summary>
        /// Рейтинг
        /// </summary>
        [Display(Name = "Рейтинг")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public double Rating { get; set; }

        /// <summary>
        /// Автомобили
        /// </summary>
        public virtual ICollection<Car> Cars { get; set; }
    }
}