using Newtonsoft.Json;

namespace WMG.DVDCentral.UI.Extensions
{
    public static class SessionExtensions
    {
        public static void SetObject(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObject<T>(this ISession session, string key) // T is going to be considered a generic type // Angle brackets inside of an Of Type
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value); // Convert the value to a JSON value if it is not null
        }
    }
}
