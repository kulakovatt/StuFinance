namespace StuFinance
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Incom")]
    public partial class Incom
    {
        [Key]
        [Column(Order = 0)]
        public int num_record { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        [Key]
        [Column(Order = 2, TypeName = "date")]
        public DateTime data_record { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(10)]
        public string type_incom { get; set; }

        [Key]
        [Column(Order = 4, TypeName = "numeric")]
        public decimal sum_incom { get; set; }

        public virtual Family Family { get; set; }
    }
}
