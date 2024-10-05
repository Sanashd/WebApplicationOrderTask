﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplicationOrder.Models.DBEntities
{
    public class OrderDetail
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int OrderedDetailsId { get; set; }
        public int OrderId { get; set; } // foriegn key for ordermaster

        [ForeignKey("OrderId")]
        public virtual OrderMaster OrderMaster { get; set; } 


        public int ItemId { get; set; } // Foreign key for Item

        [ForeignKey("ItemId")]
        public virtual Item Item { get; set; } // Reference to Item

        public int Quantity { get; set; }
        public decimal Cost { get; set; }
    }
}