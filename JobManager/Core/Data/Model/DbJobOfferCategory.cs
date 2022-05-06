using System;

namespace JobManager.Core.Data.Model
{
    public class DbJobOfferCategory
    {
        public Guid JobOfferId { get; set; }
        public DbJobOffer JobOffer { get; set; }
        public Guid CategoryId { get; set; }
        public DbCategory Category { get; set; }
    }
}
