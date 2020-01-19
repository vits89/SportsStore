using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace SportsStore.Infrastructure
{
    public static class SessionExtensions
    {
        public static void SetJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        public static T GetJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);

            return value != null ? JsonSerializer.Deserialize<T>(value) : default;
        }
    }
}
