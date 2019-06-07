﻿using System.Web.Mvc;
using PagedList;

namespace Blog.Smoothies.Helpers
{
    public static class TotalesListaHelper
    {
        public static MvcHtmlString TotalesListaPaginada(this HtmlHelper helper, IPagedList listaPaginada, string textoFormateado)
        {
            var builder = new TagBuilder("span");

            var indicePrimeraLineaPagina = ((listaPaginada.PageNumber - 1) * listaPaginada.PageSize) + 1;
            var indiceUltimaLineaPagina = listaPaginada.TotalItemCount > listaPaginada.PageNumber * listaPaginada.PageSize ? listaPaginada.PageNumber * listaPaginada.PageSize : listaPaginada.TotalItemCount;

            builder.InnerHtml = string.Format(textoFormateado, indicePrimeraLineaPagina, indiceUltimaLineaPagina,listaPaginada.TotalItemCount);

            var tag = builder.ToString(TagRenderMode.Normal);

            return MvcHtmlString.Create(tag);

        }
    }
}
