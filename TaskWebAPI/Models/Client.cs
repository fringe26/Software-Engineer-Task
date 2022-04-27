using System.ComponentModel.DataAnnotations;

namespace TaskWebAPI.Models
{
    public class Client
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }

        //public User Owner { get; set; }
        //public int OwnerId { get; set; }

    }
}
