using System.ComponentModel.DataAnnotations;

namespace CadastroDeUsuario.Data.Dtos
{
    public class CreateUserDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public DateTime Birthday { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string RePassword { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Created_At { get; set; } = DateTime.Now;

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Updated_At { get; set; }

    }
}
