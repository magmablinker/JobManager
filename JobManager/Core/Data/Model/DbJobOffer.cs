using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobManager.Core.Data.Model
{
    [Table("jobOffer")]
    public class DbJobOffer : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public float PayPerHour { get; set; }
        [ForeignKey("employerId")]
        public Guid EmployerId { get; set; }
        public DbUser Employer { get; set; }
        public virtual ICollection<DbJobOfferCategory> JobOfferCategories { get; set; }

        public DbJobOffer()
        {
            JobOfferCategories = new HashSet<DbJobOfferCategory>();
        }
    }
}
