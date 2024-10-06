using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace WebApplicationOrder.Models.DBEntities
{
    public class OrderDetail
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int OrderedDetailsId { get; set; }

        [DisplayName("Master Order ID")]
        public int OrderId { get; set; } // foriegn key for ordermaster

        [ForeignKey("OrderId")]
        public virtual OrderMaster ?OrderMaster { get; set; }

     
        public int ItemId { get; set; } // Foreign key for Item


        [ForeignKey("ItemId")]
        public virtual Item ?Item { get; set; } 

        public int Quantity { get; set; }
        public decimal Cost { get; set; }
    }
}
