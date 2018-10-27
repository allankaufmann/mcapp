using System;
using System.Collections.Generic;
using System.Text;

namespace MCAPP_Project.Core.Services
{
    public class MCAPPWebserviceException : Exception
    {
        public MCAPPWebserviceException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
