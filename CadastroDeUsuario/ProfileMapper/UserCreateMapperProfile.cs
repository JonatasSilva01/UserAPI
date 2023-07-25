using AutoMapper;
using CadastroDeUsuario.Models;
using CadastroDeUsuario.Data.Dtos;

namespace CadastroDeUsuario.ProfileMapper
{
    public class UserCreateMapperProfile : Profile
    {
        public UserCreateMapperProfile() 
        {
            CreateMap<CreateUserDto, User>();
        }
    }
}
