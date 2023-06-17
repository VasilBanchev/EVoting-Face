using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class IDCardPhotos
    {
        [Key]
        public int IDCardNumber { get; set; }
      //  [Required]
        public byte[] PersonPhoto { get; set; }  // снимката на човека
        public byte[] IDCardImage1 { get; set; } // снимка на самата лична карта
       // [Required]
        public byte[] IDCardImage2 { get; set; } // снимка на самата лична карта

        public IDCardPhotos( byte[] photoCard1, byte[] photoCard2, byte[] personPhoto)
        {
            PersonPhoto = personPhoto;
            IDCardImage1 = photoCard1;
            IDCardImage2 = photoCard2;
        }
        public IDCardPhotos()
        {

        }
    }
}
