using Infragistics;
using Reveal.Sdk;

namespace RevealSdk.Server.Reveal
{
    public class UserContextProvider : IRVUserContextProvider
    {
        public IRVUserContext GetUserContext(HttpContext aspnetContext)
        {
            var userId = "guest";
            if (aspnetContext.Request.Headers.TryGetValue("x-reveal-user", out var userID))
            {
                userId = userID;
            }
            var props = new Dictionary<string, object>() {};
            return new RVUserContext(userId, props);
        }
    }
}
