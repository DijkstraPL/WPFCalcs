using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_Infrastructure.Data
{
    public class Address
    {
        public static string Url { get; set; } = @"https://localhost:44322/";        
        public static string ImagesUrl => Url + @"clientapp/uploads/parameters/";
    }
}
