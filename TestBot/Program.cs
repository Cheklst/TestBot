using System;
using System.Collections.Generic;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;

namespace TestBot
{
    class Program
    {
        private static string token { get; set; } = "1489838419:AAHSFby54bMrBwd7TqrJ8pHV7iO9kTVkCyE";
        private static TelegramBotClient client;
        static void Main(string[] args)
        {
            client = new TelegramBotClient(token);
            client.StartReceiving();
            client.OnMessage += OnMessageHandler;
            Console.ReadLine();
            client.StopReceiving();
        }

        private static async void OnMessageHandler(object sender, MessageEventArgs e)
        {
            var msg = e.Message;
            if (msg.Text != null)
            {
                Console.WriteLine($"Сообщение получено: {msg.Text}");
                switch (msg.Text)
                {
                    case "Стикер":
                        var stic = await client.SendStickerAsync(
                            chatId: msg.Chat.Id,
                            sticker: "https://tlgrm.ru/_/stickers/215/071/215071f0-39ec-35bd-afed-d3532357c66d/192/19.webp",
                            replyMarkup: GetButtons());
                        break;
                    case "Картинка":
                        var pic = await client.SendPhotoAsync(
                            chatId: msg.Chat.Id,
                            photo: "https://sun9-3.userapi.com/impf/c846320/v846320551/2b990/KjX3KFrHsjk.jpg?size=924x1280&quality=96&sign=cbb968607ccdeb551122cfae1be2b633&type=album",
                            replyMarkup: GetButtons());
                        break;
                    case "Видео":
                        var message = await client.SendTextMessageAsync(
                            msg.Chat.Id,
                            "Если бы это работало, я был бы рад",
                            replyToMessageId: msg.MessageId,
                            replyMarkup: GetButtons());
                        //var video = await client.SendVideoAsync(
                        //    chatId: msg.Chat.Id,
                        //    video: "https://raw.githubusercontent.com/TelegramBots/book/master/src/docs/video-countdown.mp4",
                        //    thumb: "https://raw.githubusercontent.com/TelegramBots/book/master/src/2/docs/thumb-clock.jpg",
                        //    supportsStreaming: true,
                        //    replyMarkup: GetButtons());
                        break;
                    case "Опрос":
                        await client.SendPollAsync(
                            msg.Chat.Id,
                            question: "Do you like what you see?",
                            options: new []
                            {
                            "Oh, yes, Master!",
                            "No, i'm leaving... "
                            }
                         );
                        break;
                    default:
                        await client.SendTextMessageAsync(msg.Chat.Id, "Выберите команду: ", replyMarkup: GetButtons());
                        break;
                }



                //эхо
                //await client.SendTextMessageAsync(msg.Chat.Id, msg.Text, replyToMessageId: msg.MessageId); 

                //Отправка стикеров
                //var stic = await client.SendStickerAsync(
                //    chatId: msg.Chat.Id,
                //    sticker: "https://tlgrm.ru/_/stickers/215/071/215071f0-39ec-35bd-afed-d3532357c66d/192/19.webp",
                //    replyToMessageId: msg.MessageId);


                //Сообщение с кнопками
                //await client.SendTextMessageAsync(msg.Chat.Id, msg.Text, replyMarkup: GetButtons());
            }
        }

        //Кнопочки
        private static IReplyMarkup GetButtons()
        {
            return new ReplyKeyboardMarkup
            {
                Keyboard = new List<List<KeyboardButton>>
                {
                    new List<KeyboardButton>{ new KeyboardButton { Text = "Стикер" }, new KeyboardButton { Text = "Картинка"} },
                    new List<KeyboardButton>{ new KeyboardButton { Text = "Видео"}, new KeyboardButton { Text = "Опрос"} }
                }
            };
        }
    }
}
