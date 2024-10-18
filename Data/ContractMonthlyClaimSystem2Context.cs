using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ContractMonthlyClaimSystem2.Models;

namespace ContractMonthlyClaimSystem2.Data
{
    public class ContractMonthlyClaimSystem2Context : DbContext
    {
        public ContractMonthlyClaimSystem2Context (DbContextOptions<ContractMonthlyClaimSystem2Context> options)
            : base(options)
        {
        }

        public DbSet<ContractMonthlyClaimSystem2.Models.Claim> Claim { get; set; } = default!;
    }
}
