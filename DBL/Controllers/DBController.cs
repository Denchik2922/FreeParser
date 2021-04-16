using DBL.DataAccess;
using DBL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;


namespace DBL.Controllers
{
	public class DBController : BaseController
	{
		/// <summary>
		/// Связь с базой данных.
		/// </summary>
		private readonly DBContext db;

		public DBController(DBContext db)
		{
			this.db = db;
		}

		/// <summary>
		/// Добавление новых данных.
		/// </summary>
		/// <typeparam name="T"> Тип модели. </typeparam>
		/// <param name="model"> Модель. </param>
		/// <returns> Результат выполнения. </returns>
		public override void Add<T>(T model)
		{
			try
			{
				db.Set<T>().Add(model);
				Save();
			}
			catch (Exception e)
			{
				throw new ArgumentException($"Произошла ошибка при добавлении данных!\n Код ошибки: {e}", nameof(model));
			}

		}

		/// <summary>
		/// Добавление новых данных.
		/// </summary>
		/// <typeparam name="T"> Тип модели. </typeparam>
		/// <param name="model"> Модели. </param>
		/// <returns> Результат выполнения. </returns>
		public override void AddRange<T>(List<T> models)
		{
			try
			{
				db.Set<T>().AddRange(models);
				Save();
			}
			catch (Exception e)
			{
				throw new Exception($"Произошла ошибка при добавлении данных!\n Код ошибки: {e}");
			}

		}

		/// <summary>
		/// Удаление данных по Id.
		/// </summary>
		/// <param name="id"> Id модели. </param>
		/// <returns> Результат выполнения. </returns>
		public override void Delete<T>(int id)
		{
			try
			{
				T model = db.Set<T>().First(m => m.Id == id);
				db.Set<T>().Remove(model);
				Save();
			}
			catch (Exception e)
			{
				throw new Exception($"Произошла ошибка при добавлении данных!\n Код ошибки: {e}");
			}

		}

		/// <summary>
		/// Вывести все данные.
		/// </summary>
		/// <typeparam name="T"> Тип модель.</typeparam>
		/// <returns> Список моделей. </returns>
		public override List<T> GetAll<T>()
		{
			try
			{
				return db.Set<T>().ToList();
			}
			catch(Exception e)
			{
				throw new Exception($"Произошла ошибка при выводе данных!\n Код ошибки: {e}");
			}
		}

		/// <summary>
		/// Вывести данные по Id.
		/// </summary>
		/// <typeparam name="T"> Тип модели. </typeparam>
		/// <param name="id"> ID модели. </param>
		/// <returns> Модель. </returns>
		public override T GetId<T>(int id)
		{
			try
			{
				return db.Set<T>().First(m => m.Id == id);
			}
			catch (Exception e)
			{
				throw new Exception($"Произошла ошибка при выводе данных!\n Код ошибки: {e}");
			}

		}

		/// <summary>
		/// Сохранить изменение.
		/// </summary>
		/// <returns> Результат выполнения. </returns>
		public override void Save()
		{
			db.SaveChanges();
		}

		/// <summary>
		/// Изменить даные по Id.
		/// </summary>
		/// <typeparam name="T"> Тип модели. </typeparam>
		/// <param name="model"> Модель. </param>
		/// <returns> Результат выполнения. </returns>
		public override void Update<T>(T model)
		{
			try
			{
				var entity = db.Set<T>().Find(model.Id);
				if (entity == null)
				{
					return;
				}

				db.Entry(entity).CurrentValues.SetValues(model);
				db.SaveChanges();
			}
			catch (Exception e)
			{
				throw new Exception($"Произошла ошибка при Обновлении данных!\n Код ошибки: {e}");
			}

		}
	}
}
