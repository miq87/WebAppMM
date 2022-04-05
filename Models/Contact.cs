using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;


namespace WebAppMM.Models
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }
        [StringLength(32)]
        public string? FirstName { get; set; }
        [StringLength(32)]
        public string? LastName { get; set; }
        [StringLength(128)]
        public string? Email { get; set; }
        [StringLength(32)]
        public string? PhoneNumber { get; set; }
        [StringLength(32)]
        public string? PhoneCategory { get; set; }
        [StringLength(32)]
        public string? Position { get; set; }
        [StringLength(32)]
        public string? BirthdayDate { get; set; }
    }
}
