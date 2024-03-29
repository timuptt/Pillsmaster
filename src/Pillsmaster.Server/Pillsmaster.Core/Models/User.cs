﻿
namespace Pillsmaster.Domain.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        
        public Guid UserDetailsId { get; set; }
        public UserDetails UserDetails { get; set; }
    }
}
