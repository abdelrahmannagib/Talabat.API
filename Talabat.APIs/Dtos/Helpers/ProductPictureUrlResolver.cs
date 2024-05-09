using AutoMapper;
using Talabat.Core.Entities;

namespace Talabat.APIs.Dtos.Helpers
{
	public class ProductPictureUrlResolver : IValueResolver<Product, ProductToReturnDto, string>
	{
		private readonly IConfiguration _configuration;

		public ProductPictureUrlResolver(IConfiguration configuration)
		{
			_configuration = configuration;
		}
		public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
		{
			return $"{_configuration["ApiBaseUrl"]}/{source.PictureUrl}";
		}
	}
}
