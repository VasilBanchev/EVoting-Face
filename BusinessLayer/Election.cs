using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BusinessLayer
{
    public class Election
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public int Type { get; set; }
        public List<Party> Parties { get; set; }
        [ForeignKey("Questions")]
        public int QuestionID { get; set; }
        public Question Question { get; set; }
        [Required]
        public DateTime DateFrom { get; set; }
        [Required]
        public DateTime Dateto { get; set; }

        public Election( int type, Question question, DateTime datefrom, DateTime dateto)
        {
            Type = type;
            Question = question;
            DateFrom = datefrom;
            Dateto = dateto;
            Parties = new List<Party>();
        }
        public Election(int type, DateTime datefrom, DateTime dateto)
        {
            Type = type;
            DateFrom = datefrom;
            Dateto = dateto;
            Parties = new List<Party>();
        }
        private Election()
        {

        }
    }
}
