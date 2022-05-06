using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobManager.Service.Interface
{
    public interface ILanguageService
    {
        public string Get(string key, params string[] args);
    }
}
