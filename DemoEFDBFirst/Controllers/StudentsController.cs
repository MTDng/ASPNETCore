using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Demo.Models;

namespace Demo.Controllers
{
    public class StudentsController : Controller
    {
        private readonly EFCoreDBFirstDemoContext _context;
        public StudentsController(EFCoreDBFirstDemoContext context)
        {
            _context = context;
        }

        //GET: Students
        public async Task<IActionResult> Index()
        {
            return View(await _context.Student.ToListAsync());
        }

        //GET Students/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student.SingleOrDefaultAsync(m => m.StudentId == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        //GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }

        //POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentId,FirstName,LastName,Gender,DateOfBirth,DateOfRegistration,PhoneNumber,Email,Address1,Address2,City,State,Zip")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
            }
            return View(student);
        }

        //GET Students/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student.SingleOrDefaultAsync(m => m.StudentId == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("StudentId,FirstName,LastName,Gender,DateOfBirth,DateOfRegistration,PhoneNumber,Email,Address1,Address2,City,State,Zip")] Student student)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.StudentId))
                    {
                        return NotFound();
                    }
                    else{
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student
                .SingleOrDefaultAsync(m => m.StudentId == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var student = await _context.Student.SingleOrDefaultAsync(m => m.StudentId == id);
            _context.Student.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        private bool StudentExists(long id)
        {
            return _context.Student.Any(e => e.StudentId == id);
        }
    }
}