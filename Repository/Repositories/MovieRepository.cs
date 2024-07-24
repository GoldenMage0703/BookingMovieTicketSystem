﻿using DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class MovieRepository : GenericRepository<Movie>
    {
        public MovieRepository(PRN221_ProjectContext ProjectContext) : base(ProjectContext)
        {
        }

    }
}