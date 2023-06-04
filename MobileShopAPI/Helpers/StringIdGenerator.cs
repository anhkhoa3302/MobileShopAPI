using Org.BouncyCastle.Utilities.Encoders;

namespace MobileShopAPI.Helpers
{
    public class StringIdGenerator
    {
        public static string GenerateUniqueId()
        {
            long uniqueNumber = DateTime.Now.Ticks;
            string uniqueString = Convert.ToBase64String(BitConverter.GetBytes(uniqueNumber));
            return uniqueString;
        }
    }
}
