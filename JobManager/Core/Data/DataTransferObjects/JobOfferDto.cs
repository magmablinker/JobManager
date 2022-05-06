using System;
using System.Collections.Generic;

namespace JobManager.Core.Data.DataTransferObjects
{
    public class JobOfferDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float PayPerHour { get; set; }
        public UserDto Employer { get; set; }
        public List<CategoryDto> Categories { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
