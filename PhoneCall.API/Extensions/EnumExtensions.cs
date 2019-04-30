using System.Reflection;
using System.ComponentModel;
namespace PhoneCall.API.Extensions{
    public static class EnumExtensions{
        public static string ToDescriptionString<T>(this T @enum){
            FieldInfo info = @enum.GetType().GetField(@enum.ToString());
            var attributes = (DescriptionAttribute[])info.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes?[0].Description ?? @enum.ToString();

        }
    }
}