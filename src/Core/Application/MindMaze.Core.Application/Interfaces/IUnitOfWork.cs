﻿using MindMaze.Core.Domain.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMaze.Core.Application.Interfaces
{
    public interface IUnitOfWork
    {
        int SaveChanges();

        Task<int> SaveChangesAsync();
    }
}
