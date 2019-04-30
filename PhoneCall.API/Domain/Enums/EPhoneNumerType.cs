using System.Collections;
using System.ComponentModel;

namespace PhoneCall.API.Domain.Enums
{
    public enum EPhoneNumberType : int
    {
        [Description("Home")]
        Home = 1,
        [Description("Work")]
        Work = 2,
        [Description("Mobile_1")]
        Mobile_1 = 3,
        [Description("Mobile_2")]
        Mobile_2 = 4
    }
}
