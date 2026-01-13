using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EfConsoleApp.Console
{
    public class Agent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string AgencyName { get; set; }

        public ICollection<FootballerAgent> FootballerAgent { get; set; }
    }
}