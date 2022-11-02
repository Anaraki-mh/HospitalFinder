using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalFinder.Core.Database
{
    public class HospitalFinderContext : DbContext
    {
        public HospitalFinderContext(DbContextOptions<HospitalFinderContext> options) : base(options)
        {
        }


    }
}
