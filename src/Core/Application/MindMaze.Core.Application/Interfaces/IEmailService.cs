﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMaze.Core.Application.Interfaces
{
    public interface IEmailService
    {

        public Task SendEmailtoGmail(string email);
    }
}
