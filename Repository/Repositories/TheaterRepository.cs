using DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class TheaterRepository : GenericRepository<Theater>
    {
        public TheaterRepository(PRN221_ProjectContext ProjectContext) : base(ProjectContext)
        {
        }
    }
}
