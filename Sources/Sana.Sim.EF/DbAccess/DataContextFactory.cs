using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Sana.Sim.EF.DbAccess
{
    public class DataContextFactory
    {
        private readonly DbContextOptions<DataContext> options;

        public DataContextFactory(DbContextOptions<DataContext> options)
        {
            this.options = options;
        }

        public DataContext CreateDataContext()
        {
            return new DataContext(options);
        }
    }
}
