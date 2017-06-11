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
        /// A custom Hero card is a card with a single large image
        /// </summary>
        /// <param name="replyMsg">Post a message to be sent to the user.</param>
        /// <param name="title">The string passed when the method called</param>
        /// <param name="values">The options (buttons) that the user can tap/click on</param>
        /// <returns>The string value representation of the button clicked by the user</returns>
        public static IMessageActivity CustomHeroCard(IMessageActivity replyMsg, string title, string subtitle, string text, string imagePath, string[] values)
        {

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
               
                Text = text,
                Buttons = cardButtons,
                Images = new List<CardImage>
                {
                    new CardImage(imagePath)
                },
                Subtitle = subtitle
                
            };
            Attachment attachment = card.ToAttachment();
            replyMsg.Attachments.Add(attachment);

            return replyMsg;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="subtitle"></param>
        /// <param name="text"></param>
        /// <param name="cardImage"></param>
        /// <param name="cardAction"></param>
        /// <returns></returns>
        public static Attachment GetHeroCard(string title, string subtitle, string text, CardImage cardImage, CardAction cardAction)
        {
            var heroCard = new HeroCard
            {
                Title = title,
                Subtitle = subtitle,
                Text = text,
                Images = new List<CardImage>() { cardImage },
                Buttons = new List<CardAction>() { cardAction },
            };

            return heroCard.ToAttachment();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="subtitle"></param>
        /// <param name="text"></param>
        /// <param name="cardImage"></param>
        /// <param name="cardAction"></param>
        /// <returns></returns>
        private static Attachment GetThumbnailCard(string title, string subtitle, string text, CardImage cardImage, CardAction cardAction)
        {
            var heroCard = new ThumbnailCard
            {
                Title = title,
                Subtitle = subtitle,
                Text = text,
                Images = new List<CardImage>() { cardImage },
                Buttons = new List<CardAction>() { cardAction },
            };

            return heroCard.ToAttachment();
        }

        /// <summary>
        /// Populate a Hero-card with all possible properties
        /// </summary>
        /// <returns>A list of cards ready for carousel styling</returns>
        private static IList<Attachment> GetCardsAttachments()
        {
            return new List<Attachment>()
            {
                Cards.GetHeroCard(
                    "Internship",
                    "All information available about your mandatory internship",
                    "Student reports from previous years, vacant positions at Denmark's top 5 IT company, suggested job contract and more.",
                    new CardImage(url: "http://i.imgur.com/hwQjecp.jpg"),
                    new CardAction(ActionTypes.OpenUrl, "Learn More", value: "url")),
                Cards.GetHeroCard(
                    "title",
                    "subtitle",
                    "text",
                    new CardImage(url: ""),
                    new CardAction(ActionTypes.OpenUrl, "buttontext", value: "url")),
                Cards.GetHeroCard(
                    "title",
                    "subtitle",
                    "text",
                    new CardImage(url: ""),
                    new CardAction(ActionTypes.OpenUrl, "buttontext", value: "url")),

            };
        }
    }


}