using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class User : IdentityUser
    {

        public IDCard FK_IDCard { get; set; } // Foreignkey to IDCard
        public bool Authenticated { get; set; } = false;
        public bool EligibleForVoting { get; set; }=false;
        public bool BlackList { get; set; } = false;
        public List<Election> ElectionsVoted { get; set; } = new List<Election>();

        public User(IDCard idcard )
        {
            this.FK_IDCard = idcard;
            this.ElectionsVoted = new List<Election>();
            this.UserName = idcard.EGN;
            if (DateTime.Now.CompareTo(idcard.DateOfBirth.AddYears(18)) >=0)
            {
                EligibleForVoting = true;
            }
          
        }
        public User(string name):base(name)
        {
            
        }

        public User()
        {

        }
    }
}
