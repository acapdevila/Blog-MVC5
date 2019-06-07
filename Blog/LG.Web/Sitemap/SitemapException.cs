﻿using System;
using System.Runtime.Serialization;

namespace Blog.Smoothies.Sitemap
{
    /// <summary>Represents errors that occur during sitemap creation.</summary>
    [Serializable]
    public class SitemapException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Boilerplate.Web.Mvc.Sitemap.SitemapException" /> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public SitemapException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Boilerplate.Web.Mvc.Sitemap.SitemapException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner.</param>
        public SitemapException(string message, Exception inner)
            : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Boilerplate.Web.Mvc.Sitemap.SitemapException" /> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the
        /// serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains
        /// contextual information about the source or destination.</param>
        protected SitemapException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
        
    }
}