using DTO.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class SeatRepository : GenericRepository<Seat>
    {
        public SeatRepository(PRN221_ProjectContext ProjectContext) : base(ProjectContext)
        {
        }
        
    }
}
