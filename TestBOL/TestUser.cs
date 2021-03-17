using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TestBOL
{
    [Table("TestUser")]
    public class TestUser : IdentityUser
    {
        [MaxLength (10, ErrorMessage = "maximum {1} characters allowed")]
        public override string UserName { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        public IEnumerable<BankAccount> BankAccounts { get; set; }
    }
}
