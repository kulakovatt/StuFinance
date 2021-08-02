namespace StuFinance
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Cost")]
    public partial class Cost
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
        public DateTime data_cost { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(50)]
        public string type_cost { get; set; }

        [Key]
        [Column(Order = 4, TypeName = "numeric")]
        public decimal sum_cost { get; set; }

        public virtual Family Family { get; set; }
    }
}
