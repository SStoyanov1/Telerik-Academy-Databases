namespace ATMEngine.Model
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class TransactionsHistory
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(10)]
        public string CardNumber { get; set; }

        public DateTime TransactionDate { get; set; }

        public decimal Amount { get; set; }
    }
}
