using Api.Models;
using Api.Dtos.Transactions;

namespace Api.Profiles
{
    public class TransactionProfile : AutoMapper.Profile
    {
        public TransactionProfile()
        {
            CreateMap<Transaction, TransactionReadDto>();
            CreateMap<TransactionCreateDto, Transaction>();
            CreateMap<TransactionUpdateDto, Transaction>();
        }
    }
}