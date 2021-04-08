using AngleSharp.Html.Parser;
using ParserBurse.Burse;
using System;
using System.Collections.Generic;

namespace ParserBurse.Core
{
    public class ParserWorker
    {
        public IParser parser { get; set; }
        IParserSettings parserSettings;

        HtmlLoader loader;

        bool isActive;

        public IParserSettings Settings
        {
            get
            {
                return parserSettings;
            }
            set
            {
                parserSettings = value;
                loader = new HtmlLoader(value);
            }
        }

        public bool IsActive
        {
            get => isActive;
        }

        public event Action<object, List<object>> OnNewData;

        public event Action<object, List<object>> OnNewCategory;

        public event Action<object, List<object>> OnNewExtraCategory;

        public ParserWorker(IParser parser)
        {
            this.parser = parser;
        }

        public ParserWorker(IParser parser, IParserSettings parserSettings) : this(parser)
        {
            this.parserSettings = parserSettings;
        }

        public void Start()
        {
            isActive = true;
            Worker();
        }

        private async void Worker()
        {
            for (int i = parserSettings.StartPoint; i <= parserSettings.EndPoint; i++)
            {
                if (!isActive)
                {
                    return;
                }

                var source = loader.GetSourceByPageId(i);
                var domParser = new HtmlParser();

                var document = await domParser.ParseDocumentAsync(source);

                var result = parser.ParseOrder(document);

                OnNewData?.Invoke(this, result);
            }
            isActive = false;
        }

        public async void GetCategories()
        {
            var source = await loader.GetSourceByMainPage();
            var domParser = new HtmlParser();

            var document = await domParser.ParseDocumentAsync(source);

            var result = parser.ParseCategory(document);

            OnNewCategory?.Invoke(this, result);
        }

        public async void GetExtraCategories()
        {
            var source = await loader.GetSourceByMainPage();
            var domParser = new HtmlParser();

            var document = await domParser.ParseDocumentAsync(source);

            var result = parser.ParseExtraCategory(document);

            OnNewExtraCategory?.Invoke(this, result);
        }


    }
}
