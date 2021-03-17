using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestBOL
{
    [Table("BankAccount")]
    public class BankAccount
    {
        [Key]
        public int AccountId { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public DateTime CreatedOn { get; set; }

        [ForeignKey("TestUser")]
        public string Id { get; set; }
        public TestUser TestUser { get; set; }
    }
}