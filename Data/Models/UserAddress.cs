
using Data.Models.Enums;

namespace Data.Models
{
    public class UserAddress
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string MobileNo { get; set; }
        public string Pincode { get; set; }
        public string HouseNo { get; set; }
        public int AreaId { get; set; }
        public string AreaName { get; set; }
        public string Landmark { get; set; }
        public string TownCity { get; set; }
        public string StateName { get; set; }
        public bool IsActive { get; set; }
        public bool IsDefault { get; set; }
        public AddressType AddressTypeID { get; set; }
    }

    public class UserAddressColumn : UserAddress
    {

    }
}
