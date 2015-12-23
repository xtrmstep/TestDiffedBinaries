using System;
using System.Net.Http.Headers;
using System.Web.Http.Filters;

namespace TestDiffedBinaries.Api.Controllers.Filters
{
    [AttributeUsage(AttributeTargets.Method)]
    public class CachingAttribute : ActionFilterAttribute
    {
        public int MaxAge { get; set; }

        public CachingAttribute()
        {
            MaxAge = 60; // seconds
        }

        public override void OnActionExecuted(HttpActionExecutedContext context)
        {
            context.Response.Headers.CacheControl = new CacheControlHeaderValue()
            {
                Public = true,
                MaxAge = TimeSpan.FromSeconds(MaxAge)
            };

            base.OnActionExecuted(context);
        }
    }
}