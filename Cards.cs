using System.Collections.Generic;
using Microsoft.Bot.Connector;

namespace chatbot101
{
    public class Cards
    {
        
        /// <summary>
        /// Constructor of a custom Hero card, a card with a single large image
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

        public static IMessageActivity CustomHeroCardNoPicture(IMessageActivity replyMsg, string title, string[] values)
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

                Buttons = cardButtons,
                
            };
            Attachment attachment = card.ToAttachment();
            replyMsg.Attachments.Add(attachment);

            return replyMsg;
        }

        /// <summary>
        /// Constructor of a Hero Card
        /// </summary>
        /// <param name="title">Title of the card</param>
        /// <param name="subtitle">Subtitle of the card</param>
        /// <param name="text">Text or paragraph of the card</param>
        /// <param name="cardImage">Image of the card</param>
        /// <param name="cardAction">Action type, can be a go-to-url or sign-in action</param>
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
  
    }


}