using System.ComponentModel.DataAnnotations;

namespace Warsztat.Data.Entity
{
    public class Users
    {
        [Key]
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public bool Active { get; set; }
        public string Email { get; set; }
        public int? DictionaryType_Id { get; set; }

    }
}
