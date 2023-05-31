﻿using Microsoft.AspNetCore.Mvc;
using P013EStore.Core.Entities;
using P013EStroe.Service.Abstract;

namespace P013EStroe.MVCUI.ViewComponents
{
	public class Categories : ViewComponent
	{
		private readonly IService<Category> _service;

		public Categories(IService<Category> service)
		{
			_service = service;
		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
			return View(await _service.GetAllAsync());
		}
	}
}