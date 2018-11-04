using AutoMapper;
using LetsBank.Core.Entities;
using LetsBank.Core.Models;

namespace LetsBank.Infrastructure.Mapping
{
	public class LetsBankProfile : Profile
	{
		public LetsBankProfile()
		{
			CreateMap<TransactionRecord, TxnRecordModel>()
				.ForMember(tr => tr.Amount			, opt => opt.MapFrom(entity => entity.Amount			))
				.ForMember(tr => tr.Type			, opt => opt.MapFrom(entity => entity.Type				))
				.ForMember(tr => tr.InitialBalance	, opt => opt.MapFrom(entity => entity.InitialBalance	))
				.ForMember(tr => tr.FinalBalance	, opt => opt.MapFrom(entity => entity.FinalBalance		))
				.ForMember(tr => tr.Date			, opt => opt.MapFrom(entity => entity.Date				));
			CreateMap<TxnRecordModel, TransactionRecord>();
		}
	}
}
