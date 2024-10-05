using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplicationOrder.DAL;
using WebApplicationOrder.Models.DBEntities;

namespace WebApplicationOrder.Controllers
{
    public class OrderMastersController : Controller
    {
        private readonly ItemDbContext _context;

        public OrderMastersController(ItemDbContext context)
        {
            _context = context;
        }

        // GET: OrderMasters
        public async Task<IActionResult> Index()
        {
            return View(await _context.OrderMasters.ToListAsync());
        }

        // GET: OrderMasters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderMaster = await _context.OrderMasters
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (orderMaster == null)
            {
                return NotFound();
            }

            return View(orderMaster);
        }

        // GET: OrderMasters/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OrderMasters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerID,OrderedDate,OrderedAmount")] OrderMaster orderMaster)
        {
            if (ModelState.IsValid)
            {

                _context.Add(orderMaster);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(orderMaster);
        }

        public IActionResult CreateOrderDetails(int orderId)
        {
            // Pass the OrderId to the View for OrderDetails creation
            ViewBag.OrderId = orderId;
            return View();
        }


        public async Task<IActionResult> UpdateOrderAmount(int orderId)
        {
            // Get the OrderMaster
            var orderMaster = await _context.OrderMasters.FindAsync(orderId);

            if (orderMaster != null)
            {
                // Sum the Cost of all OrderDetails for this Order
                var orderDetails = await _context.OrderDetails
                                                 .Where(od => od.OrderId == orderId)
                                                 .ToListAsync();

                // Calculate the OrderedAmount
                orderMaster.OrderedAmount = orderDetails.Sum(od => od.Cost);

                // Update the OrderMaster with the calculated OrderedAmount
                _context.Update(orderMaster);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return NotFound();
        }


        // GET: OrderMasters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderMaster = await _context.OrderMasters.FindAsync(id);
            if (orderMaster == null)
            {
                return NotFound();
            }
            return View(orderMaster);
        }

        // POST: OrderMasters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,CustomerID,OrderedDate,OrderedAmount")] OrderMaster orderMaster)
        {
            if (id != orderMaster.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderMaster);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderMasterExists(orderMaster.OrderId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(orderMaster);
        }

     

        // GET: OrderMasters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderMaster = await _context.OrderMasters
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (orderMaster == null)
            {
                return NotFound();
            }

            return View(orderMaster);
        }

        // POST: OrderMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orderMaster = await _context.OrderMasters.FindAsync(id);
            if (orderMaster != null)
            {
                _context.OrderMasters.Remove(orderMaster);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderMasterExists(int id)
        {
            return _context.OrderMasters.Any(e => e.OrderId == id);
        }
    }
}
