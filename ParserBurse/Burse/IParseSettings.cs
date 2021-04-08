using System;
using System.Collections.Generic;
using System.Text;

namespace ParserBurse.Burse
{
    public interface IParserSettings
    {
        string BurseName { get; set; }

        string BaseUrl { get; set; }

        string Prefix { get; set; }

        int StartPoint { get; set; }

        int EndPoint { get; set; }
    }
}
