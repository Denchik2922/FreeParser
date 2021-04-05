using DBL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBL.Controllers
{
	public abstract class BaseController
	{

		public abstract List<T> GetAll<T>() where T : class, IModel;

		public abstract T GetId<T>(int id) where T : class, IModel;

		public abstract void Add<T>(T model) where T : class, IModel;

		public abstract void AddRange<T>(List<T> models) where T : class, IModel;

		public abstract void Delete<T>(int id) where T : class, IModel;

		public abstract void Update<T>(T model) where T : class, IModel;

		public abstract void Save();
	}
}
