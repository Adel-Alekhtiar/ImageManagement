using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Customer:ContactBase
    {
        public int? NumberOfTasks { get; set; }
        public string? CustomerCode { get; set; }
        public string? CustomerSince { get; set; } //date only using string for simplicity

    }
}
