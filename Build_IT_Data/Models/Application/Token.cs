using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_Data.Models.Application
{
    public class Token
    {        
        public int Id { get; set; }
        public string ClientId { get; set; }
        public int Type { get; set; }
        public string Value { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
            }
}
