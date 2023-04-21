
using System.Data;
using System.Reflection;
using System.Text;
using AppUtility.Extensions;
using System.Web;


namespace AppUtility.Helper
{
    public class Utility
    {
        public static Utility O => instance.Value;
        private static Lazy<Utility> instance = new Lazy<Utility>(() => new Utility());
        private Utility() { }
        public string GetErrorDescription(int errorCode)
        {
            string error = ((Errorcodes)errorCode).DescriptionAttribute();
            return error;
        }
       

       

        public Dictionary<string, dynamic> ConvertToDynamicDictionary(object someObject)
        {
            var res = someObject.GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .ToDictionary(prop => prop.Name, prop => (dynamic)prop.GetValue(someObject, null));
            return res;
        }

        public Dictionary<string, string> ConvertToDictionary(object someObject)
        {
            var res = someObject.GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .ToDictionary(prop => prop.Name, prop => (string)prop.GetValue(someObject, null));
            return res;
        }

        public string GenrateRandom(int length, bool isNumeric = false)
        {
            string valid = "abcdefghjkmnpqrstuvwxyzABCDEFGHJKMNPQRSTUVWXYZ23456789";
            if (isNumeric)
            {
                valid = "1234567890";
            }
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }

        public string GetQueryString(object obj)
        {
            var properties = from p in obj.GetType().GetProperties()
                             where p.GetValue(obj, null) != null
                             select p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(obj, null).ToString());

            return String.Join("&", properties.ToArray());
        }

        
    }
}
