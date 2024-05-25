using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Build_IT_Data.Models.Application
{
    public class ApplicationUser : IdentityUser
    {
        public string DisplayName { get; set; }
        public int Type { get; set; }
        public int Flags { get; set; }
        public DateTime CreatedDate { get; set; }

        public DateTime LastModifiedDate { get; set; }
        public virtual List<Token> Tokens { get; set; }
    }
}
