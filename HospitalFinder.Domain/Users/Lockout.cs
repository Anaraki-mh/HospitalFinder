using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HospitalFinder.Domain.Users
{
    public class Lockout
    {
        #region Constructor

        public Lockout()
        {

        }

        #endregion


        #region Properties

        [Key]
        public int Id { get; set; }

        public string? IPAddress { get; set; }

        public int FailedAttempts { get; set; }

        public bool IsLockedOut { get; set; }

        public DateTime? LockoutExpirationDateTime { get; set; }

        #endregion

    }
}
