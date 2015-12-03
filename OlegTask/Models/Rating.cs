using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OlegTask.Models
{
    /// <summary>
    /// Оценка
    /// </summary>
    [DisplayName("Оценки")]
    public class Rating
    {
        /// <summary>
        /// Идентификационный номер
        /// </summary>
        [Display(Name = "№")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Значение
        /// </summary>
        [Display(Name = "Значение")]
        [Range(0, 10)]
        public float Value { get; set; }

        /// <summary>
        /// Идентификационный номер водителя
        /// </summary>
        [Display(Name = "Идентификационный номер водителя")]
        public int DriverId { get; set; }

        /// <summary>
        /// Водитель
        /// </summary>
        public virtual Driver Driver { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        [Display(Name = "Дата создания"), Column(TypeName = "smalldatetime")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime Created { get; set; }
    }
}