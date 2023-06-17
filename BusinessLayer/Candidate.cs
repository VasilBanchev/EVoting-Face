using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Candidate
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(20)]
        public string MidName { get; set; }
        [Required]
        [MaxLength(35)]
        public string LastName { get; set; }
        public Party FK_Party { get; set; }
        [Required]
        public int Region { get; set; }
       // public int PersonalVotes { get; set; } = 0;
        public Candidate(string firstName, string midName, string lastName, Party party, int region)
        {
            this.FirstName = firstName;
            this.MidName = midName;
            this.LastName = lastName;
            this.FK_Party = party;
            this.Region = region;
        }

        public Candidate()
        {
            //Region = -1;
        }
    }
}
