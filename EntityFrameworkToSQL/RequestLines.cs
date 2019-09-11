using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameworkToSQL
{
    public partial class RequestLines
    {
        public int Id { get; set; }
        public int RequestId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        [ForeignKey("ProductId")]
        [InverseProperty("RequestLines")]
        public virtual Products Product { get; set; }
        [ForeignKey("RequestId")]
        [InverseProperty("RequestLines")]
        public virtual Requests Request { get; set; }
    }
}
