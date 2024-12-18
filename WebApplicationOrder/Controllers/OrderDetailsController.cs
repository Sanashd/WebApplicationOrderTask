﻿using System;
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
    public class OrderDetailsController : Controller
    {
        private readonly ItemDbContext _context;

        public OrderDetailsController(ItemDbContext context)
        {
            _context = context;
        }

        // GET: OrderDetails
      
        public async Task<IActionResult> Index(string searchCustomerinOrder)
        {

            var orderDetails = _context.OrderDetails
                .Include(o => o.Item)
                .Include(o => o.OrderMaster).AsQueryable();


            if (!String.IsNullOrEmpty(searchCustomerinOrder))
            {

                if (int.TryParse(searchCustomerinOrder, out int customerId))
                {
                    orderDetails = orderDetails.Where(o=>o.OrderMaster.CustomerID == customerId);
                }
            }


            return View(await orderDetails.ToListAsync());
        }


        // GET: OrderDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderDetail = await _context.OrderDetails
                .Include(o => o.Item)
                .Include(o => o.OrderMaster)
                .FirstOrDefaultAsync(m => m.OrderedDetailsId == id);
            if (orderDetail == null)
            {
                return NotFound();
            }

            return View(orderDetail);
        }

        // GET: OrderDetails/Create
        public IActionResult Create()
        {
            // Populate OrderId and ItemId for the dropdowns
            ViewData["OrderId"] = new SelectList(_context.OrderMasters, "OrderId", "OrderId");
            ViewData["ItemId"] = new SelectList(_context.Items, "ItemId", "ItemDesc");
            return View();
        }

        // POST: OrderDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,ItemId,Quantity,Cost")] OrderDetail orderDetail)
        {

            // Save the OrderDetail to the database
            _context.Add(orderDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
            
          
          

        // GET: OrderDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderDetail = await _context.OrderDetails.FindAsync(id);
            if (orderDetail == null)
            {
                return NotFound();
            }
            ViewData["ItemId"] = new SelectList(_context.Items, "ItemId", "ItemDesc", orderDetail.ItemId);
            ViewData["OrderId"] = new SelectList(_context.OrderMasters, "OrderId", "OrderId", orderDetail.OrderId);
            return View(orderDetail);
        }

        // POST: OrderDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderedDetailsId,OrderId,ItemId,Quantity,Cost")] OrderDetail orderDetail)
        {
            if (id != orderDetail.OrderedDetailsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //Fetch the latest cost for the item

                           var item = await _context.Items.FindAsync(orderDetail.ItemId);
                    if (item != null)
                    {
                        // Update cost based on the item price and quantity
                        orderDetail.Cost = item.ItemCost * orderDetail.Quantity;
                    }
                    _context.Update(orderDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderDetailExists(orderDetail.OrderedDetailsId))
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
            ViewData["ItemId"] = new SelectList(_context.Items, "ItemId", "ItemId", orderDetail.ItemId);
            ViewData["OrderId"] = new SelectList(_context.OrderMasters, "OrderId", "OrderId", orderDetail.OrderId);
            return View(orderDetail);
        }





        public async Task<IActionResult> GetCustomerId(int orderId)
        {
            var orderMaster = await _context.OrderMasters.FindAsync(orderId);
            if (orderMaster != null)
            {
                return Json(new { customerId = orderMaster.CustomerID });

            }
            return Json(new { customerId = " " });


        }

        // GET: OrderDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderDetail = await _context.OrderDetails
                .Include(o => o.Item)
                .Include(o => o.OrderMaster)
                .FirstOrDefaultAsync(m => m.OrderedDetailsId == id);
            if (orderDetail == null)
            {
                return NotFound();
            }

            return View(orderDetail);

        }

        // POST: OrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orderDetail = await _context.OrderDetails.FindAsync(id);
            if (orderDetail != null)
            {
                _context.OrderDetails.Remove(orderDetail);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderDetailExists(int id)
        {
            return _context.OrderDetails.Any(e => e.OrderedDetailsId == id);
        }
    }
}
