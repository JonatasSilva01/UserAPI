using AutoMapper;
using CadastroDeUsuario.Data.Dtos;
using CadastroDeUsuario.Models;
using Microsoft.AspNetCore.Identity;

namespace CadastroDeUsuario.Services
{
    public class UserServices
    {
        private UserManager<User> _userManager;
        private IMapper _mapper;
        private SignInManager<User> _signInManager;
        private TokenServices _tokenService;

        public UserServices(IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager, TokenServices tokenService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public async Task RegisterAsync(CreateUserDto dto)
        {
            User user = _mapper.Map<User>(dto);
            IdentityResult result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded) throw new ApplicationException("Falha ao cadastrar usuário!");

        }

        public async Task<string> LoginAsync(LoginUserDto dto)
        {
            var result = await _signInManager.PasswordSignInAsync(dto.UserName, dto.Password, false, false);
            if (!result.Succeeded) throw new ApplicationException("usuario não autenticado!");

            var user = 
                _signInManager
                .UserManager
                .Users
                .FirstOrDefault(x => x.UserName == dto.UserName.ToUpper());

            var token = _tokenService.GenereteToken(user);

            return token;
        }
    }
}
