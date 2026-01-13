using System.ComponentModel.DataAnnotations;

namespace EfConsoleApp.Console
{
    public class FootballerAgent
    {
        public int FootballerId { get; set; }
        public Footballer Footballer { get; set; }

        public int AgentId { get; set; }
        public Agent Agent { get; set; }
    }
}