using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.UI.Models
{
    public class BaseModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public int IsDeleted { get; set; }
    }
}
