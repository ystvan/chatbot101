using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Bot.Connector;

namespace chatbot101
{
    public class Cards
    {
        /// <summary>
        /// A Hero card is a card with a single large image
        /// </summary>
        /// <param name="replyMsg">Post a message to be sent to the user.</param>
        /// <param name="title">The string passed when the method called</param>
        /// <param name="values">The options (buttons) that the user can tap/click on</param>
        /// <returns></returns>
        public static IMessageActivity CreateHeroCard(IMessageActivity replyMsg, string title, string[] values)
        {
            string path = @"http://cdn.makeuseof.com/wp-content/uploads/2015/11/swipesbot.png?x59455";

            replyMsg.Text = title;

            var cardButtons = new List<CardAction>();
            foreach (var value in values)
            {
                CardAction button = new CardAction()
                {
                    Value = value,
                    Title = value
                };
                cardButtons.Add(button);
            }
            HeroCard card = new HeroCard()
            {
                Buttons = cardButtons,
                Images = new List<CardImage>
                {
                    new CardImage(path)
                }
            };
            Attachment attachment = card.ToAttachment();
            replyMsg.Attachments.Add(attachment);

            return replyMsg;
        }
    }
}