using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobManager.Core.Data.DataTransferObjects.Request.Authentication
{
    public class AuthenticationRequestDto
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
    }
}
