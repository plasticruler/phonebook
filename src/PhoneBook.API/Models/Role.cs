using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.API.Models
{
    public class Role:BaseEntity<long>
    {
        public Role()
        {
            Users = new HashSet<UserRole>();
        }
        public string RoleName { get; set; }
        public ICollection<UserRole> Users { get; set; }
    }
}
