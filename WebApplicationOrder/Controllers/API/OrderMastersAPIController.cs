using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationOrder.DAL;
using WebApplicationOrder.Models.DBEntities;

namespace WebApplicationOrder.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderMastersAPIController : ControllerBase
    {
        private readonly ItemDbContext _context;

        public OrderMastersAPIController(ItemDbContext context)
        {
            _context = context;
        }

        // GET: api/orderMasters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderMaster>>> GetOrderMastes()
        {
            return await _context.OrderMasters.ToListAsync();
        }

        // GET: api/orderMaster/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderMaster>> GetOrderMaster(int id)
        {
            var orderMaster = await _context.OrderMasters.FindAsync(id);
            if (orderMaster == null)
            {
                return NotFound();
            }
            return orderMaster;
        }

        // POST: api/orderMaster
        [HttpPost]
        public async Task<ActionResult<OrderMaster>> PostOrderMaster(OrderMaster orderMaster)
        {
            _context.OrderMasters.Add(orderMaster);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOrderMaster), new { id = orderMaster.OrderId }, orderMaster);
        }

        // PUT: api/orderMaster/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderMaster(int id, OrderMaster orderMaster)
        {
            if (id != orderMaster.OrderId)
            {
                return BadRequest();
            }

            _context.Entry(orderMaster).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderMasterExists(id))
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

        // DELETE: api/orderMaster/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderMaster(int id)
        {
            var orderMaster = await _context.OrderMasters.FindAsync(id);
            if (orderMaster == null)
            {
                return NotFound();
            }

            _context.OrderMasters.Remove(orderMaster);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderMasterExists(int id)
        {
            return _context.OrderMasters.Any(e => e.OrderId == id);
        }
    }
}
