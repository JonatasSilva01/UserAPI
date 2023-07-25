using Microsoft.AspNetCore.Identity;

namespace CadastroDeUsuario.Models
{
    public class User : IdentityUser
    {
        public DateTime Birthday { get; set; }
        public User() : base () {}
    }
}
