using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DemoMySqlEF.Models;
using DemoMySQLEF.Models.Interface;

namespace DemoMySQLEF.Controllers
{
    public class StudentController : Controller
    {
        private readonly IUnitOfWork uow;

        public StudentController(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        // GET: Student
        public async Task<IActionResult> Index()
        {
            return View(await uow.StudentRepository.GetAllAsync());
        }

        // GET: Student/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // var student = await _context.Student
            //     .FirstOrDefaultAsync(m => m.StudentID == id);
            var student = await uow.StudentRepository.FindByAsync(x => x.StudentID == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student.FirstOrDefault());
        }

        // GET: Student/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Student/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentID,LastName,FirstMidName,EnrollmentDate")] Student student)
        {
            if (ModelState.IsValid)
            {
                // _context.Add(student);
                // await _context.SaveChangesAsync();
                await uow.StudentRepository.AddAsync(student);
                await uow.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Student/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // var student = await _context.Student.FindAsync(id);
            var student = await uow.StudentRepository.FindByAsync(x => x.StudentID == id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student.FirstOrDefault());
        }

        // POST: Student/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StudentID,LastName,FirstMidName,EnrollmentDate")] Student student)
        {
            if (id != student.StudentID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // _context.Update(student);
                    // await _context.SaveChangesAsync();
                    await uow.StudentRepository.UpdateAsync(student, id);
                    await uow.SaveAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.StudentID))
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
            return View(student);
        }

        // GET: Student/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // var student = await _context.Student
            //     .FirstOrDefaultAsync(m => m.StudentID == id);
            var student = await uow.StudentRepository.FindByAsync(x => x.StudentID == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // var student = await _context.Student.FindAsync(id);
            // _context.Student.Remove(student);
            // await _context.SaveChangesAsync();
            var student = await uow.StudentRepository.FindByAsync(x => x.StudentID == id);
            await uow.StudentRepository.DeleteAsync(student.FirstOrDefault());
            await uow.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            // return _context.Student.Any(e => e.StudentID == id);
            var student = uow.StudentRepository.FindBy(x => x.StudentID == id);
            return student == null ? false : true;
        }
    }
}
