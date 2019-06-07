using System.Threading;
using System.Web.Mvc;
using System.Web.UI;
using Blog.Datos;
using LG.Web.RssAtom;
using LG.Web.Servicios.RssAtom;

namespace LG.Web.Controllers
{
    public class RssController : Controller
    {
        private readonly IFeedService _feedService;
        
        public RssController()
        {
           _feedService = new FeedService(new UrlHelper(System.Web.HttpContext.Current.Request.RequestContext), new ContextoBaseDatos());
        }

        public RssController(IFeedService feedService)
        {
            _feedService = feedService;
        }

        /// <summary>
        /// Gets the Atom 1.0 feed for the current site. Note that Atom 1.0 is used over RSS 2.0 because Atom 1.0 is a 
        /// newer and more well defined format. Atom 1.0 is a standard and RSS is not. See
        /// http://rehansaeed.com/building-rssatom-feeds-for-asp-net-mvc/
        /// </summary>
        /// <returns>The Atom 1.0 feed for the current site.</returns>
        [OutputCache(Duration = 86400, Location = OutputCacheLocation.Any, VaryByParam = "none")]
        public ActionResult Feed()
        {
            // A CancellationToken signifying if the request is cancelled. See
            // http://www.davepaquette.com/archive/2015/07/19/cancelling-long-running-queries-in-asp-net-mvc-and-web-api.aspx
            CancellationToken cancellationToken = this.Response.ClientDisconnectedToken;
            return new AtomActionResult(_feedService.GetFeed(cancellationToken));
        }
    }
}