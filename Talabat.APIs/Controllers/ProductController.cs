using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Dtos;
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
		private readonly IMapper _mapper;

		public ProductController(IGenericRepository<Product> productsReop,IMapper mapper)
		{
			_productsReop = productsReop;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<ProductToReturnDto>>> GetProducts()
		{
			//var products = await _productsReop.GetAllAsync();
			var spec =new ProductWithBrandAndCategorySpecifications();
			var products= await _productsReop.GetAllWithSpecAsync(spec);

			return Ok(_mapper.Map<IEnumerable<Product>, IEnumerable<ProductToReturnDto>>(products));
		}

		// baseurl/apiProducts/1

		[HttpGet("{id}")]
		public async Task<ActionResult<Product>> GetProduct(int id)
		{
			var spec = new ProductWithBrandAndCategorySpecifications(id);
			var product = await _productsReop.GetWithSpecAsync(spec);
			if (product is null)
				return NotFound(new { Message = "Not Found", StatusCode = 404 }); //404

			return Ok(_mapper.Map<Product, ProductToReturnDto>(product)); // 200
		}

	}
}
