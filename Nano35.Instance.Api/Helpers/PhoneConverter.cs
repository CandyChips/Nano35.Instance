using System.Linq;

namespace Nano35.Instance.Api.Helpers
{
    public static class PhoneConverter
    {
        public static string RuPhoneConverter(string currentPhone)// Comes like +7(800)555-35-35
        {
            var tmp = string.Concat(currentPhone
                .AsEnumerable()
                .Select(f => f.ToString())
                .Where(f => int.TryParse(f, out _)));
            tmp = tmp.Length < 11 || tmp.Length > 11 ? "" : tmp.Substring(1, 10);
            return tmp;
        }
        
    }

}