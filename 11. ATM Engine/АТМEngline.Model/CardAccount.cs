namespace ATMEngine.Model
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class CardAccount
    {
        private ICollection<TransactionsHistory> transactions;

        public CardAccount()
        {
            this.transactions = new HashSet<TransactionsHistory>();
        }

        [Key]
        public int CardId { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(10)]
        public string CardNumber { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(4)]
        public string CardPin { get; set; }

        public decimal CardCash { get; set; }

        public virtual ICollection<TransactionsHistory> Transactions
        {
            get { return this.transactions; }
            set { this.transactions = value; }
        }
    }
}
