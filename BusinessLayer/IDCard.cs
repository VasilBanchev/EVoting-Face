using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class IDCard
    {
        [Key]
        public string IDCardNumber { get; set; }
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
        [Required]
        [MaxLength(20)]
        public string MidName { get; set; }
        [Required]
        [MaxLength(35)]
        public string LastName { get; set; }
        [Required]
        public string EGN { get; set; }

        public string BornPlace { get; set; }
        public int Region { get; set; }
        public string RegionText { get; set; }
        public IDCardPhotos CardPhotos { get; set; }//външен ключ към снимките на личната карта
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public DateTime DateOfExpiry { get; set; }
        [Required]
        public DateTime DateOfIssue { get; set; }
        [Required]
        public Char Gender { get; set; }
        public IDCard(string iDCardNumber, string name, string midName, string lastName, string egn, IDCardPhotos cardPhotos, DateTime dateOfExpiry, int region)
        {
            IDCardNumber = iDCardNumber;
            Name = name;
            MidName = midName;
            LastName = lastName;
            EGN = egn;
            BornPlace = BornRegion(egn);
            CardPhotos = cardPhotos;
            DateOfBirth = BirthdayDate(egn);
            DateOfExpiry = dateOfExpiry;
            DateOfIssue = dateOfExpiry.AddYears(-10);
            Gender = GenderChar(egn);
            Region = region;
            RegionText = RegionPlace(region);
        }

        private char GenderChar(string egn)
        {
            if (Convert.ToInt16(this.EGN[8]) % 2 == 0)
            {
                return 'm';
            }
            else
            {
                return 'f';
            }
        }

        private DateTime BirthdayDate(string egn)
        {
            try
            {
                int year = Convert.ToInt32(egn.Substring(0, 2));
                int month = Convert.ToInt32(egn.Substring(2, 2));
                int date = Convert.ToInt32(egn.Substring(4, 2));

                if (month > 40)
                {
                    year += 2000;
                    month -= 40;
                }
                else if (month > 20)
                {
                    year += 1800;
                    month -= 20;
                }
                else
                {
                    year += 1900;
                }

                if (year < 1 || year > 9999 || month < 1 || month > 12 || date < 1 || date > DateTime.DaysInMonth(year, month))
                {
                    throw new ArgumentException("Invalid EGN");
                }

                return new DateTime(year, month, date);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string BornRegion(string egn)
        {
            try
            {
                int placeCode = Convert.ToInt32(egn.Substring(6, 3));

                Dictionary<string, Tuple<int, int>> regions = new Dictionary<string, Tuple<int, int>>()
                {
                {"Благоевград", Tuple.Create(0, 43)},
                {"Бургас", Tuple.Create(44, 93)},
                {"Варна", Tuple.Create(94, 139)},
                {"Велико Търново", Tuple.Create(140, 169)},
                {"Видин", Tuple.Create(170, 183)},
                {"Враца", Tuple.Create(184, 217)},
                {"Габрово", Tuple.Create(218, 233)},
                {"Кърджали", Tuple.Create(234, 281)},
                {"Кюстендил", Tuple.Create(282, 301)},
                {"Ловеч", Tuple.Create(302, 319)},
                {"Монтана", Tuple.Create(320, 341)},
                {"Пазарджик", Tuple.Create(342, 377)},
                {"Перник", Tuple.Create(378, 395)},
                {"Плевен", Tuple.Create(396, 435)},
                {"Пловдив", Tuple.Create(436, 501)},
                {"Разград", Tuple.Create(502, 527)},
                {"Русе", Tuple.Create(528, 555)},
                {"Силистра", Tuple.Create(556, 575)},
                {"Сливен", Tuple.Create(576, 601)},
                {"Смолян", Tuple.Create(602, 623)},
                {"София – град", Tuple.Create(624, 721)},
                {"София – окръг", Tuple.Create(722, 751)},
                {"Стара Загора", Tuple.Create(752, 789)},
                {"Добрич", Tuple.Create(790, 821)},
                {"Търговище", Tuple.Create(822, 843)},
                {"Хасково", Tuple.Create(844, 871)},
                {"Шумен", Tuple.Create(872, 903)},
                {"Ямбол", Tuple.Create(904, 925)},
                {"Друг/Неизвестен", Tuple.Create(926, 999)}
                };

                foreach (var reg in regions)
                {
                    if (placeCode <= reg.Value.Item2)
                    {
                        return reg.Key;
                    }
                }

                return "Друг/Неизвестен";
            }
            catch (Exception ex)
            {

                throw ex;
            }
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
        public IDCard(string iDCardNumber, string name)
        {
            IDCardNumber = iDCardNumber;
            Name = name;


        }
        public IDCard()
        {

        }
    }
}
