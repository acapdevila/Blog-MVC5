using System;
using System.Web.Mvc;

namespace Ac.Web.Sitemap
{
    /// <summary>
    /// Requires that a HTTP request does not contain a trailing slash. If it does, return a 404 Not Found. This is
    /// useful if you are dynamically generating something which acts like it's a file on the web server.
    /// E.g. /Robots.txt/ should not have a trailing slash and should be /Robots.txt. Note, that we also don't care if
    /// it is upper-case or lower-case in this instance.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class NoTrailingSlashAttribute : FilterAttribute, IAuthorizationFilter
    {
        private const char QueryCharacter = '?';
        private const char SlashCharacter = '/';

        /// <summary>
        /// Determines whether a request contains a trailing slash and if it does, calls the
        /// <see cref="M:Blog.Smoothies.Sitemap.NoTrailingSlashAttribute.HandleTrailingSlashRequest(System.Web.Mvc.AuthorizationContext)" /> method.
        /// </summary>
        /// <param name="filterContext">An object that encapsulates information that is required in order to use the
        /// <see cref="T:System.Web.Mvc.RequireHttpsAttribute" /> attribute.</param>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="filterContext" /> parameter is null.</exception>
        public virtual void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
                throw new ArgumentNullException(nameof(filterContext));
            string str = filterContext.HttpContext.Request.Url.ToString();
            int num = str.IndexOf('?');
            if (num == -1)
            {
                if ((int)str[str.Length - 1] != 47)
                    return;
                this.HandleTrailingSlashRequest(filterContext);
            }
            else
            {
                if ((int)str[num - 1] != 47)
                    return;
                this.HandleTrailingSlashRequest(filterContext);
            }
        }

        /// <summary>
        /// Handles HTTP requests that have a trailing slash but are not meant to.
        /// </summary>
        /// <param name="filterContext">An object that encapsulates information that is required in order to use the
        /// <see cref="T:System.Web.Mvc.RequireHttpsAttribute" /> attribute.</param>
        protected virtual void HandleTrailingSlashRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = (ActionResult)new HttpNotFoundResult();
        }
    }
}
