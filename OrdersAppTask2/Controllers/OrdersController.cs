using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OrdersAppTask2.Models;

namespace OrdersAppTask2.Controllers
{
    public class OrdersController : Controller
    {
        private readonly Prog7311task2Context _context;

        public OrdersController(Prog7311task2Context context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {

                if (HttpContext.Session.GetString("LoggedStatus") == "Employee")
                {
                    var prog7311task2Context = _context.Orders.Include(o => o.ProductNameNavigation).Include(o => o.UsernameNavigation);
                    return View(await prog7311task2Context.ToListAsync());
                }
                else if (HttpContext.Session.GetString("LoggedStatus") == "Customer")
                {
                     var prog7311task2Context = _context.Orders.Include(o => o.ProductNameNavigation).Include(o => o.UsernameNavigation).Where(s => s.Username.Contains(HttpContext.Session.GetString("LoggedInUser")));
                     return View(await prog7311task2Context.ToListAsync());
                }
            else
                {
                    return RedirectToAction("Privacy", "Home");
                }

        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.ProductNameNavigation)
                .Include(o => o.UsernameNavigation)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return RedirectToAction("Privacy", "Home");
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("LoggedStatus") != null)
            {
                int i = 0;
                foreach (var item in _context.Orders)
                {
                     i++;
                }
                HttpContext.Session.SetInt32("Identity",i);
                ViewData["ProductName"] = new SelectList(_context.Products, "ProductName", "ProductName");
                ViewData["Username"] = new SelectList(_context.Users, "Username", "Username");
                return View();
            }
            else
            {
                return RedirectToAction("Log", "Home");
            }
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,Username,ProductName")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction("Transaction", "Home");
            }
            ViewData["ProductName"] = new SelectList(_context.Products, "ProductName", "ProductName", order.ProductName);
            ViewData["Username"] = new SelectList(_context.Users, "Username", "Username", order.Username);
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["ProductName"] = new SelectList(_context.Products, "ProductName", "ProductName", order.ProductName);
            ViewData["Username"] = new SelectList(_context.Users, "Username", "Username", order.Username);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,Username,ProductName")] Order order)
        {
            if (id != order.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrderId))
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
            ViewData["ProductName"] = new SelectList(_context.Products, "ProductName", "ProductName", order.ProductName);
            ViewData["Username"] = new SelectList(_context.Users, "Username", "Username", order.Username);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.ProductNameNavigation)
                .Include(o => o.UsernameNavigation)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return RedirectToAction("Privacy", "Home");
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.OrderId == id);
        }
    }
}
