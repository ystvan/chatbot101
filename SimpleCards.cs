using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Bot.Connector;

namespace chatbot101
{
    public class SimpleCards
    {
        //TODO: Adaptive cards to be implemented, until then just simple cards


        public static IMessageActivity CreateSimpleCard(IMessageActivity replyMsg, string title, string[] values)
        {
            replyMsg.Text = title;

            var cardButtons = new List<CardAction>();
            foreach (var value in values)
            {
                CardAction button = new CardAction()
                {
                    Value = value,
                    Type = "imBack",
                    Title = value
                };
                cardButtons.Add(button);
            }
            HeroCard card = new HeroCard()
            {
                Buttons = cardButtons
            };
            Attachment attachment = card.ToAttachment();
            replyMsg.Attachments.Add(attachment);

            return replyMsg;
        }
    }
}