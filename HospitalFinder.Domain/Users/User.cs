using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalFinder.Domain.Users
{
    public class User
    {
        #region Constructor

        public User()
        {

        }

        #endregion


        #region Properties

        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        public string EmailAddress { get; set; }

        [MaxLength(64)]
        public string PasswordHash { get; set; }

        [MaxLength(64)]
        public string? TokenHash { get; set; }

        public DateTime? TokenExpirationDateTime { get; set; }

        #endregion

    }
}
