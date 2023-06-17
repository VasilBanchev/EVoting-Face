using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Question
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [MaxLength(1000)]
        public string Text{ get; set; }
        public List<Answer> Answers { get; set; }

        public Question(string text, List<Answer> answers)
        {

            Text = text;
            Answers = answers;
        }
        public Question(string text)
        {

            Text = text;
            Answers = new List<Answer>();
        }
        private Question()
        {

        }
    }
}
