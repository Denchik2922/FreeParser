using AngleSharp.Html.Dom;
using DBL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BurseParse.Core
{
    public interface IParser
    {
        List<Order> ParseOrder(IHtmlDocument document);

        List<Category> ParseCategory(IHtmlDocument document);
    }
}
