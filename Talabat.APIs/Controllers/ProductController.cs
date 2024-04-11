﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;

namespace Talabat.APIs.Controllers
{
	//[Route("api/[controller]")]
	//[ApiController]
	public class ProductController : BaseApiController
	{
		private readonly IGenericRepository<Product> _productsReop;

		public ProductController(IGenericRepository<Product> productsReop)
		{
			_productsReop = productsReop;
		}

	

	}
}