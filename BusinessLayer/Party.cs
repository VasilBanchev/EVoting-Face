using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Party
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public List<Candidate> Candidates { get; set; } = new List<Candidate>();
        [Required]
        [MaxLength(70)]
        public string PartyLeader { get; set; } = string.Empty;

        public Party(string name, string partyLeader)
        {
            this.Name = name;
            this.PartyLeader = partyLeader;
          //  Candidates = new List<Candidate>();
        }

        public Party()
        {

        }
    }
}
