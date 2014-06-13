using LocalizedModelProviderLab.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LocalizedModelProviderLab.Models
{
    public class ProductModel
    {
        public int ProductId { get; set; }

        [StringLength(3)]
        [Required]
        public string ProductName { get; set; }

        public DateTime CreateDate { get; set; }

        [Range(100D, 300.55D)]
        public decimal Price { get; set; }

        public int Qty { get; set; }

        [Compare("Qty")]
        public int Qty_Old { get; set; }
    }
}