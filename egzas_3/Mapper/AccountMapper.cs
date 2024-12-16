//using egzas_3.Dtos.Requests;
//using egzas_3.Entities;
//using egzas_3.Mapper.Interfaces;
//using egzas_3.Services.Interfaces;


//namespace egzas_3.Mapper
//{
//    public class AccountMapper : IAccountMapper
//    {
//        private readonly IAccountService _service;

//        public AccountMapper(IAccountService service)
//        {
//            _service = service;
//        }


//        public Account Map(AccountRequestDto dto)
//        {
//            _service.CreatePasswordHash(dto.AccountPassword!, out var passwordHash, out var passwordSalt);
//            return new Account
//            {
//                AccountName = dto.AccountName!,
//                AccountEmail = dto.AccountEmail!,
//                AccountPasswordHash = passwordHash,
//                AccountPasswordSalt = passwordSalt,
//                AccountRole = dto.AccountRole!
//            };
//        }

//    }
//}
