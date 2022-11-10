﻿using HospitalFinder.Domain.BaseData;
using HospitalFinder.Domain.Enums;
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

        public HospitalUpdateOperation OperationType { get; set; }

        [MaxLength(30)]
        public string? Name { get; set; }

        [MaxLength(40)]
        public string Country { get; set; }

        [MaxLength(40)]
        public string City { get; set; }

        [MaxLength(150)]
        public string? Address { get; set; }

        public decimal? Latitude { get; set; }

        public decimal? Longtitude { get; set; }

        public int? OpenTime { get; set; }

        public int? CloseTime { get; set; }

        public long? Telephone { get; set; }

        [MaxLength(30)]
        public string? Website { get; set; }

        public bool IsRemoved { get; set; }

        #endregion


        #region

        public int HospitalId { get; set; }
        public virtual Hospital Hospital { get; set; }

        #endregion

    }
}
