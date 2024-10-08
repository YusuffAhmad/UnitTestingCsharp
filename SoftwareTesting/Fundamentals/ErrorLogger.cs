﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareTesting.Fundamentals
{
    public class ErrorLogger
    {
        public string LastError { get; set; }
        public event EventHandler<Guid> ErrorLogged;

        public void Log(string error)
        {
            if(string.IsNullOrWhiteSpace(error))
                throw new ArgumentNullException();

            LastError = error;
            // Write the log to a meessage
            // ...
            ErrorLogged?.Invoke(this, Guid.NewGuid());
        }
    }
}
