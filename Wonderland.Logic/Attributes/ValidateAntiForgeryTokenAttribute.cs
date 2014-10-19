
namespace Wonderland.Logic.Attributes
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Helpers;
    using System.Web.Http.Controllers;
    using System.Web.Http.Filters;

    /// <summary>
    /// taken from : http://stackoverflow.com/questions/11476883/web-api-and-validateantiforgerytoken
    /// and : http://stackoverflow.com/questions/11725988/problems-implementing-validatingantiforgerytoken-attribute-for-web-api-with-mvc/11726560#11726560
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public sealed class ValidateAntiForgeryTokenAttribute : FilterAttribute, IAuthorizationFilter
    {
        public Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {
            try
            {
                // AntiForgery.Validate();

                var headers = actionContext.Request.Headers;
                var cookie = headers
                    .GetCookies()
                    .Select(x => x[AntiForgeryConfig.CookieName])
                    .FirstOrDefault();
                var requestVerificationToken = headers.GetValues("__RequestVerificationToken").FirstOrDefault();
                AntiForgery.Validate(cookie != null ? cookie.Value : null, requestVerificationToken);
            }
            catch
            {
                actionContext.Response = new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.Forbidden,
                    RequestMessage = actionContext.ControllerContext.Request
                };
                return FromResult(actionContext.Response);
            }
            return continuation();
        }

        private Task<HttpResponseMessage> FromResult(HttpResponseMessage result)
        {
            var source = new TaskCompletionSource<HttpResponseMessage>();
            source.SetResult(result);
            return source.Task;
        }
    }
}
