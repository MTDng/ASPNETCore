using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DemoRepository.Entities;
using DemoRepository.Models;
using DemoRepository.Interface;

namespace DemoRepository.Controllers
{
    public class CourseController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public CourseController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // GET: Course
        public async Task<IActionResult> Index()
        {
            return View(await unitOfWork.CourseRepository.GetAllAsyn());
        }

        // GET: Course/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var course = await unitOfWork.CourseRepository.FindByAsync(x => x.CourseID == id);

            return View(course.FirstOrDefault());
        }

        // GET: Course/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Course/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CourseID,Title,Credits")] Course course)
        {
            if (ModelState.IsValid)
            {
                await unitOfWork.CourseRepository.AddAsyn(course);
                await unitOfWork.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        // GET: Course/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await unitOfWork.CourseRepository.FindByAsync(x => x.CourseID == id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course.FirstOrDefault());
        }

        // POST: Course/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CourseID,Title,Credits")] Course course)
        {
            if (id != course.CourseID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await unitOfWork.CourseRepository.UpdateAsync(course, course.CourseID);
                    await unitOfWork.SaveAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.CourseID))
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
            return View(course);
        }

        // GET: Course/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await unitOfWork.CourseRepository.FindByAsync(x => x.CourseID == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Course/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await unitOfWork.CourseRepository.FindByAsync(x => x.CourseID == id);
            await unitOfWork.CourseRepository.DeleteAsyn(course.FirstOrDefault());
            await unitOfWork.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(int id)
        {
            return unitOfWork.CourseRepository.FindBy(x => x.CourseID == id) == null ? false : true;
        }
    }
}
