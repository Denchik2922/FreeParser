using BurseParse.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace BurseParse.Burses.Kabanchik
{
	public class KabanchikSettings : IParserSettings
	{
        public KabanchikSettings(int start, int end)
        {
            StartPoint = start;
            EndPoint = end;
        }

        public string BurseName { get; set; } = "Kabanchik";

        public string BaseUrl { get; set; } = "https://freelancehunt.com/projects";

        public string Prefix { get; set; } = "?page={CurrentId}";

        public int StartPoint { get; set; }

        public int EndPoint { get; set; }
    }
}
