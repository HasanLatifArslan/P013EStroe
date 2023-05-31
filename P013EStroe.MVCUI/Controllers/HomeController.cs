using Microsoft.AspNetCore.Mvc;
using P013EStore.Core.Entities;
using P013EStroe.MVCUI.Models;
using P013EStroe.Service.Abstract;
using System.Diagnostics;

namespace P013EStroe.MVCUI.Controllers
{
	public class HomeController : Controller
	{
		private readonly IService<Slider> _serviceSlider;

		public HomeController(IService<Slider> serviceSlider)
		{
			_serviceSlider = serviceSlider;
		}

		public async Task<IActionResult> Index()
		{
			var model = await _serviceSlider.GetAllAsync();
			return View(model);
		}
		[Route("/AccessDenied")]
		public IActionResult AccessDenied()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}