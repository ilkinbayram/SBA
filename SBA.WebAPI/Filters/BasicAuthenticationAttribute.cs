using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;

public class BasicAuthenticationAttribute : Attribute, IAuthorizationFilter
{
    private const string _username = "adm-defresba";
    private const string _password = "jbDz74drtiYdsbsGdJ832GYgerjgfdbCMciu.wEF4g.ewS4Df";


    public void OnAuthorization(AuthorizationFilterContext context)
    {
        string authHeader = context.HttpContext.Request.Headers["Authorization"];
        if (authHeader != null && authHeader.StartsWith("Basic "))
        {
            var encodedCredentials = authHeader.Substring("Basic ".Length).Trim();
            var decodedCredentials = Encoding.UTF8.GetString(Convert.FromBase64String(encodedCredentials));
            var credentialsArray = decodedCredentials.Split(':');

            var username = credentialsArray[0];
            var password = credentialsArray[1];
            if (username == _username && password == _password)
            {
                return;
            }
        }

        context.Result = new UnauthorizedResult();
    }
}