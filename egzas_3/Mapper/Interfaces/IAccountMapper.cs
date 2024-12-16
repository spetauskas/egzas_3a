using egzas_3.Dtos.Requests;
using egzas_3.Entities;


namespace egzas_3.Mapper.Interfaces
{
    public interface IAccountMapper
    {
        Account Map(AccountRequestDto dto);
    }
}
