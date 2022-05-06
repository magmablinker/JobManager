using JobManager.Core.Enum;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobManager.Core.Data.Model
{
    [Table("user")]
    public class DbUser : BaseModel
    {
        [MaxLength(16)]
        public string Username { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        public string EmailAddress { get; set; }
        public DateTime DateOfBirth { get; set; }
        public UserType UserType { get; set; }
    }
}
