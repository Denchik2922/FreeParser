using AngleSharp.Html.Parser;
using DBL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BurseParse.Core
{
    public class ParserWorker
    {
        /// <summary>
        /// Биржи для парсинга.
        /// </summary>
        private Dictionary<IParser, IParserSettings> parsers { get; set; }

        public ParserWorker()
		{
            parsers = new Dictionary<IParser, IParserSettings>();
		}

        public event Func<List<Order>, Task> OnNewOrderAsync;

        public event Action<object, Dictionary<string,List<Category>>> OnNewCategory;


        /// <summary>
        /// Добавить биржу.
        /// </summary>
        /// <param name="parser"> Биржа. </param>
        /// <param name="settings"> Настройки биржи. </param>
        public void AddParser(IParser parser, IParserSettings settings)
		{
			try
			{
                parsers.Add(parser, settings);
            }
			catch (ArgumentException)
			{
                throw new ArgumentException("Такая биржа уже добавлена.");
			}
            
		}

        // <summary>
        /// Добавить биржи.
        /// </summary>
        /// <param name="parser"> Биржи. </param>
        public void AddParsers(Dictionary<IParser, IParserSettings> parsers)
        {
            foreach(var parser in parsers)
            try
            {
                this.parsers.Add(parser.Key, parser.Value);
            }
            catch (ArgumentException)
            {
                throw new ArgumentException("Такая биржа уже добавлена.");
            }

        }

        public async Task GetOrders()
        {
            foreach (var parser in parsers)
            {
               await Worker(parser.Key, parser.Value);
            }
        }

        public async Task GetCategories()
        {
            
            foreach (var parser in parsers)
            {
               await GetCategory(parser.Key, parser.Value);
            }
        }

        private async Task Worker(IParser parser, IParserSettings settings)
        {
            var loader = new HtmlLoader(settings);
            for (int i = settings.StartPoint; i <= settings.EndPoint; i++)
            {
                var source = await loader.GetSourceByPageId(i);
                var domParser = new HtmlParser();

                var document = await domParser.ParseDocumentAsync(source);

                var result = parser.ParseOrder(document);

                await OnNewOrderAsync?.Invoke(result);

                await Task.Delay(10000);
            }
        }

        private async Task GetCategory(IParser parser, IParserSettings settings)
        {
            var loader = new HtmlLoader(settings);
            var source = await loader.GetSourceByMainPage();
            var domParser = new HtmlParser();

            var document = await domParser.ParseDocumentAsync(source);

            var categories = parser.ParseCategory(document);
            var result = new Dictionary<string, List<Category>>()
            {
                { settings.BurseName, categories}
            };

            OnNewCategory?.Invoke(this, result);
        }
    }
}
