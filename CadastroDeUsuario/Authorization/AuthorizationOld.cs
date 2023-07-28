using Microsoft.AspNetCore.Authorization;

namespace CadastroDeUsuario.Authorization
{
    public class AuthorizationOld : IAuthorizationRequirement
    {
        public AuthorizationOld(int old)
        {
            Old = old;
        }

        public int Old { get; set; }
    }
}
