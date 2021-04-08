using AngleSharp.Html.Dom;
using System.Collections.Generic;

namespace ParserBurse.Burse
{
	public interface IParser
	{
		List<object> ParseOrder(IHtmlDocument document);

		List<object> ParseCategory(IHtmlDocument document);

		List<object> ParseExtraCategory(IHtmlDocument document);
	}
}
