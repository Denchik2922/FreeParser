using BurseParse.Core;

namespace BurseParse.Burses.Freelancehunt
{
    public class FrelancehuntSettings : IParserSettings
    {

        public FrelancehuntSettings(int start, int end)
        {
            StartPoint = start;
            EndPoint = end;
        }

        public string BurseName { get; set; } = "Freelancehunt";

        public string BaseUrl { get; set; } = "https://freelancehunt.com/projects";

        public string Prefix { get; set; } = "?page={CurrentId}";

        public int StartPoint { get; set; }

        public int EndPoint { get; set; }
    }
}
