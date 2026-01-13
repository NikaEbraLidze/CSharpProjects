using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace EfConsoleApp.Console
{
    public class Footballer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte JerseyNumber { get; set; }
        public DateTime BirthDate { get; set; }

        public ICollection<FootballerAgent> FootballerAgent { get; set; }

        public TransferMarketData TransferMarketData { get; set; }

        public int ClubId { get; set; }
        public Club Club { get; set; }
    }
}