using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Data.Models.Enums
{
    public enum UserRoles
    {
        Admin = 1,
        Franchise = 2,
        Investor = 3,
        Borrower = 4
    }
    public enum AddressType
    {
        Home = 1,
        Office = 2,
    }
}
