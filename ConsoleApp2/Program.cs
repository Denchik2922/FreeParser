using BurseParse.Burses.Freelancehunt;
using BurseParse.Burses.Kabanchik;
using BurseParse.Core;
using DBL.Models;
using System;
using System.Collections.Generic;

namespace ConsoleApp2
{


class Program
	{
		
		static void Main(string[] args)
		{

			var freelance = new FreelancehuntParser();
			var freelanceSetting = new FrelancehuntSettings(2, 3);
			

			var kabanchik = new KabanchikParser();
			var kabanchikSetting = new KabanchikSettings(2, 3);

			ParserWorker parser;
			parser = new ParserWorker();

			parser.OnNewData += Parser_OnNewData;
			parser.OnNewCategory += Parser_OnNewCategory;
			parser.OnNewExtraCategory += Parser_OnNewExtraCategory;


			try
			{
				parser.AddParser(freelance, freelanceSetting);
				parser.AddParser(kabanchik, kabanchikSetting);
			}
			catch(Exception e)
			{
				Console.WriteLine(e.Message);
			}
			
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


		private static void Parser_OnNewData(object arg1, List<Order> arg2)
		{
			if(arg2 != null)
			{
				foreach (var o in arg2)
				{
					
					Console.WriteLine($"{o.Description}");
				}
			}
			

		}
	}
}