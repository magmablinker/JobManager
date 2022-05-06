using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobManager.Core.Data.DataTransferObjects
{
    public sealed class HashingOptionsDto
    {
        public int Iterations { get; set; } = 10000;
    }
}
