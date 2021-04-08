using BurseParse.Burses.Freelancehunt;
using BurseParse.Core;
using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
	class Program
	{
		static void Main(string[] args)
		{
			ParserWorker parser;

			parser = new ParserWorker(new FreelancehuntParser());

			parser.OnNewData += Parser_OnNewData;
			parser.OnNewCategory += Parser_OnNewCategory;
			parser.OnNewExtraCategory += Parser_OnNewExtraCategory;

			parser.Settings = new FrelancehuntSettings(2, 3);
			parser.Start();

			Console.ReadKey();



		}

		private static void Parser_OnNewExtraCategory(object arg1, List<object> arg2)
		{
			throw new NotImplementedException();
		}

		private static void Parser_OnNewCategory(object arg1, List<object> arg2)
		{
			throw new NotImplementedException();
		}

		private static void Parser_OnNewData(object arg1, List<object> arg2)
		{
			
			Console.WriteLine(order);
		}
	}
}
