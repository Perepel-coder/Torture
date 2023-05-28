using System;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class BaseEntity
    {
        [Key]
        public int ID { get; set; }
        //public DateTime? AddedDate { get; set; }
        //public DateTime? ModifiedDate { get; set; }
        //public string? IPAddress { get; set; }
    }
}
