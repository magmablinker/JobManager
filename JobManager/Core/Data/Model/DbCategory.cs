using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobManager.Core.Data.Model
{
    [Table("category")]
    public partial class DbCategory : BaseModel
    {
        public string Name { get; set; }
        public virtual ICollection<DbJobOfferCategory> JobOfferCategories { get; set; }

        public DbCategory()
        {
            JobOfferCategories = new HashSet<DbJobOfferCategory>();
        }
    }
}
