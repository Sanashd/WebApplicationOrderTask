using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationOrder.Models.DBEntities
{
    public class Item
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
       
        public int ItemId { get; set; }
        [DisplayName("Item Description")]
        public string ItemDesc { get; set; }
        [DisplayName("Item Cost")]
        public decimal ItemCost { get; set; }

    }
}
