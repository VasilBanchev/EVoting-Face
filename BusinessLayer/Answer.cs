using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Answer
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [MaxLength(100)]
        public string Text{ get; set; }
        public string? PartyName { get; set; }
        public int? Region { get; set; }
        public string? RegionText { get; set; }

        [ForeignKey("Candidates")]
        public int? CandidateID { get; set; }
        public Candidate? Candidate{ get; set; }
        
        public int Votes { get; set; }

        public Answer(string text)
        {
            Text = text;    
            Votes = 0;
        }
        public Answer(Candidate candidate)
        {
            CandidateID= candidate.ID;
            Text = candidate.FirstName + " " + candidate.LastName;
            PartyName = candidate.FK_Party.Name;
            Votes = 0;
            Region= candidate.Region;
            RegionText = RegionPlace(candidate.Region);
        }
        private Answer()
        {
                
        }
        private string RegionPlace(int region)
        {
            try
            {
                List<string> regions = new List<string>() { "Благоевград", "Бургас", "Варна", "Велико Търново", "Видин", "Враца", "Габрово", "Кърджали", "Кюстендил", "Ловеч", "Монтана", "Пазарджик", "Перник", "Плевен", "Пловдив", "Разград", "Русе", "Силистра", "Сливен", "Смолян", "София – град", "София – окръг", "Стара Загора", "Добрич", "Търговище", "Хасково", "Шумен", "Ямбол", "Друг/Неизвестен" };
                return regions[region];
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
