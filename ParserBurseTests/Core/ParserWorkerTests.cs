using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParserBurse.Burse.Freelancehunt;
using ParserBurse.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParserBurse.Core.Tests
{
	[TestClass()]
	public class ParserWorkerTests
	{
		[TestMethod()]
		public void ParserWorkerTest()
		{

			// Arrange
			var parser = new ParserWorker(new FreelancehuntParser());

			//Act

			parser.Settings = new FrelancehuntSettings(2, 3);
			parser.Start();

			//Assert
			

		}


	}
}