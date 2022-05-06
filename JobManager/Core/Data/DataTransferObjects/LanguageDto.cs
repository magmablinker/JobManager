using System.Collections.Generic;

namespace JobManager.Core.Data.DataTransferObjects
{
    public class LanguageDto
    {
        public Dictionary<string, LanguageItem> LanguageItems { get; set; }

        public LanguageDto()
        {
            LanguageItems = new Dictionary<string, LanguageItem>();
        }
    }

    public class LanguageItem
    {
        public Dictionary<string, string> Languages { get; set; }

        public LanguageItem()
        {
            Languages = new Dictionary<string, string>();
        }
    }
}
