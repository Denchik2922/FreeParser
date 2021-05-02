﻿using DBL.Controllers;
using DBL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace FreeParser.Models.Commands
{
	public class CategoryCommand : BaseCommand
	{
		public override string Name => "category:";

		public override async Task Execute(Message message, TelegramBotClient client, DBController db)
		{
			var chatId = (int)message.Chat.Id;
			string messageForUser = "Выберите категорию: ";

			int idCategory = Convert.ToInt32(message.Text.Split(':')[1]);

			await client.SendTextMessageAsync(chatId, messageForUser, replyMarkup: GetButtons(idCategory, db));
		}

		private IReplyMarkup GetButtons(int idCategory, DBController db)
		{
			var keys = new List<InlineKeyboardButton>();
			var keyboard = new List<List<InlineKeyboardButton>>();

			List<ExtraCategory> categories = db.GetAll<ExtraCategory>().Where(c => c.CategoryId == idCategory).ToList();

			foreach (var c in categories)
			{
				keyboard.Add(new List<InlineKeyboardButton>() { new InlineKeyboardButton { Text = c.Name, CallbackData = $"category:{c.Id}" } });
			}

			return new InlineKeyboardMarkup(keyboard);
		}
	}
}
