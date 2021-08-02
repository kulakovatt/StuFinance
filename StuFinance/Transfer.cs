namespace StuFinance
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Transfer")]
    public partial class Transfer
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(20)]
        public string category_from { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string category_before { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        [Key]
        [Column(Order = 3, TypeName = "numeric")]
        public decimal sum_transfer { get; set; }

        public virtual Family Family { get; set; }
    }
}
