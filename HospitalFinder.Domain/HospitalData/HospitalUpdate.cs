using HospitalFinder.Domain.BaseData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalFinder.Domain.HospitalData
{
    public class HospitalUpdate : BaseEntity
    {
        #region Constructor

        public HospitalUpdate()
        {

        }

        #endregion


        #region Properties

        [MaxLength(30)]
        public string? Name { get; set; }

        [MaxLength(150)]
        public string? FullAddress { get; set; }

        public decimal? Latitude { get; set; }

        public decimal? Longtitude { get; set; }

        public int? OpenTime { get; set; }

        public int? CloseTime { get; set; }

        public long? Telephone { get; set; }

        [MaxLength(30)]
        public string? Website { get; set; }

        #endregion


        #region

        public int HospitalId { get; set; }
        public virtual Hospital Hospital { get; set; }

        #endregion

    }
}
