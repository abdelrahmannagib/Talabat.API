using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Specifications.ProductSpecs;

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

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
		{
			//var products = await _productsReop.GetAllAsync();
			var spec =new ProductWithBrandAndCategorySpecifications();
			var products= await _productsReop.GetWithSpecAsync(spec);

			return Ok(products);
		}

		// baseurl/apiProducts/1

		[HttpGet("{id}")]
		public async Task<ActionResult<Product>> GetProduct(int id)
		{
			var spec = new ProductWithBrandAndCategorySpecifications(id);
			var products = await _productsReop.GetWithSpecAsync(spec);
			if (products is null)
				return NotFound(new { Message = "Not Found", StatusCode = 404 }); //404

			return Ok(products); //200
		}

	}
}
