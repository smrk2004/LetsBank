using AutoMapper;

namespace LetsBank.Infrastructure.Mapping
{
	public class AutoMapperConfiguration
	{
		public static IMapper Configure()
		{
			var config = new MapperConfiguration(cfg =>
			{
				cfg.AddProfile<LetsBankProfile>();
			});
			return config.CreateMapper();
		}
	}
}
