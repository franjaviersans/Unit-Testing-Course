﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNinja.Mocking
{
    public interface IEmailFileSender
    {
        void EmailFile(string emailAddress, string emailBody, string filename, string subject);
    }
}
