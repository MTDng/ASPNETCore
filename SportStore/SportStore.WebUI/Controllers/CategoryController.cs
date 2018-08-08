using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SportStore.Abstract;
using SportStore.Domain.Concrete;
using SportStore.Domain.Entities;

namespace SportStore.WebUI.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork uow;

        public CategoryController(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        // GET: Category
        public async Task<IActionResult> Index()
        {
            return View(await uow.Repository<Category>().GetAllAsync());
        }

        // GET: Category/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await uow.Repository<Category>().FindByAsync(x => x.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Category/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Category/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,CategoryName")] Category category)
        {
            if (ModelState.IsValid)
            {
                // _context.Add(category);
                // await _context.SaveChangesAsync();
                await uow.Repository<Category>().AddAsync(category);
                await uow.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Category/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // var category = await _context.Categories.FindAsync(id);
            var category = await uow.Repository<Category>().FindByAsync(x => x.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Category/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryId,CategoryName")] Category category)
        {
            if (id != category.CategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // _context.Update(category);
                    // await _context.SaveChangesAsync();
                    await uow.Repository<Category>().UpdateAsync(category, id);
                    await uow.SaveAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.CategoryId))
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
            return View(category);
        }

        // GET: Category/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // var category = await _context.Categories
            //     .FirstOrDefaultAsync(m => m.CategoryId == id);
            var category = await uow.Repository<Category>().FindByAsync(x => x.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // var category = await _context.Categories.FindAsync(id);
            var category = await uow.Repository<Category>().FindByAsync(x => x.CategoryId == id);
            // _context.Categories.Remove(category);
            await uow.Repository<Category>().DeleteAsync(category.FirstOrDefault());
            // await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            //return _context.Categories.Any(e => e.CategoryId == id);
            return uow.Repository<Category>().FindBy(x => x.CategoryId == id) == null ? false : true;
        }
    }
}
