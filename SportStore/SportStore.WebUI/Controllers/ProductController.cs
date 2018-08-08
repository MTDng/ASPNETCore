using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportStore.Abstract;
using SportStore.Concrete;
using SportStore.Domain.Abstract;
using SportStore.Domain.Entities;

namespace DemoRepository.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            return View(await unitOfWork.Repository<Product>().GetAllAsync());
        }
    }
}