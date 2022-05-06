using System;
using System.Collections.Generic;

namespace JobManager.Core.Data.DataTransferObjects.Request.JobOffer
{
    public class JobOfferRequestDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public float PayPerHour { get; set; }
        public Guid EmployerId { get; set; }
        public List<Guid> CategoryIds { get; set; }
    }
}
