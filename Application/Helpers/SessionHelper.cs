using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Application.Helpers;

public static class SessionHelper
{
    /// <summary>
    /// Сохранение JSON объекта в сессии
    /// </summary>
    public static void SetObjectAsJson(this ISession session, string key, object value)
    {
        session.SetString(key, JsonConvert.SerializeObject(value));
    }

    /// <summary>
    /// Взятие JSON объекта в сессии
    /// </summary>
    public static T GetObjectFromJson<T>(this ISession session, string key)
    {
        var value = session.GetString(key);
        return value == null ? default : JsonConvert.DeserializeObject<T>(value);
    }
}