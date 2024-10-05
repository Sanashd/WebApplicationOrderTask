using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationOrder.Models.DBEntities
{
    public class OrderMaster
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int OrderId { get; set; }
        public int CustomerID { get; set; }
        public DateTime OrderedDate { get; set; } = DateTime.Now;   
        public decimal OrderedAmount { get; set; }

        // This collection should not be marked as required, allowing an OrderMaster to be created without details
      //  public  ICollection<OrderDetail> OrderDetails { get; set; } 



    }
}
