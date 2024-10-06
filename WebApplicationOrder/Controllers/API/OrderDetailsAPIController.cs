using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationOrder.DAL;
using WebApplicationOrder.Models.DBEntities;

namespace WebApplicationOrder.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsAPIController : ControllerBase
    {
        private readonly ItemDbContext _context;

        public OrderDetailsAPIController(ItemDbContext context)
        {
            _context = context;
        }

        // GET: api/orderDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDetail>>> GetOrderDetails()
        {
           
            var orderDetails = await _context.OrderDetails
                                             .Include(od => od.OrderMaster)
                                             .Include(od => od.Item)       
                                             .ToListAsync();

            return orderDetails;
        }

        // GET: api/orderDetails/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDetail>> GetOrderDetail(int id)
        {
            var orderDetail = await _context.OrderDetails
                .Include(od => od.OrderMaster) 
                                    .Include(od => od.Item)        
                                    .FirstOrDefaultAsync(od => od.OrderedDetailsId == id); 
            if (orderDetail == null)
            {
                return NotFound();
            }
            return orderDetail;
        }


        // POST: api/orderDetails
        [HttpPost]
        public async Task<ActionResult<OrderDetail>> PostOrderDetail(OrderDetail orderDetail)
        {
            _context.OrderDetails.Add(orderDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOrderDetail), new { id = orderDetail.OrderedDetailsId }, orderDetail);
        }

        // PUT: api/orderDetails/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderDetail(int id, OrderDetail orderDetail)
        {
            if (id != orderDetail.OrderedDetailsId)
            {
                return BadRequest();
            }

            _context.Entry(orderDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderDetailsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/orderDetails/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderDetail(int id)
        {
            var orderDetail = await _context.OrderDetails.FindAsync(id);
            if (orderDetail == null)
            {
                return NotFound();
            }

            _context.OrderDetails.Remove(orderDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderDetailsExists(int id)
        {
            return _context.OrderDetails.Any(e => e.OrderedDetailsId == id);
        }
    }
}
