﻿using Data.Infrastructure;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class PictureRepository : RepositoryBase<Picture>, IPictureRepository
    {
        public PictureRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
    public interface IPictureRepository : IRepository<Picture>
    {

    }
}
