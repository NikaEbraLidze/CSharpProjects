using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EfConsoleApp.Console
{
    public class TransferMarketData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public decimal Marketvalue { get; set; }

        public DateTime ContractExpirationDate { get; set; }


        public int FootballerId { get; set; }
        public Footballer Footballer { get; set; }

    }
}