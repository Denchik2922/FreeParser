using AngleSharp.Html.Dom;
using System;
using System.Collections.Generic;
using System.Text;

namespace BurseParse.Core
{
    public interface IParser
    {
        List<object> ParseOrder(IHtmlDocument document);

        List<object> ParseCategory(IHtmlDocument document);

        List<object> ParseExtraCategory(IHtmlDocument document);
    }
}
