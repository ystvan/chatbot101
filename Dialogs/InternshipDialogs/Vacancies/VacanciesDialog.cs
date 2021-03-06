﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace chatbot101.Dialogs.InternshipDialogs.Vacancies
{
    [Serializable]
    public class VacanciesDialog : IDialog<object>
    {
        /// <summary>
        /// Closing dialog
        /// </summary>
        /// <param name="context">The context for the execution of a dialog's conversational process.</param>
        /// <returns>No return type, only a task that represents the state transition</returns>
        public async Task StartAsync(IDialogContext context)
        {
            var reply = context.MakeMessage();

            reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;
            reply.Attachments = GetCardsAttachments();

            await context.PostAsync(reply);

            context.Wait(DeconstructionOfDialog);
            
        }

        /// <summary>
        /// Closing and removing the current Dialog from the stack
        /// </summary>
        /// <param name="context">The context for the execution of a dialog's conversational process.</param>
        /// <param name="result">A message in a conversation</param>
        /// <returns>No return type, only a task that represents the state transition</returns>
        private async Task DeconstructionOfDialog(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;
            // State transition - complete this Dialog and remove it from the stack
            context.Done<object>(new object());
        }

        /// <summary>
        /// Populate Hero-cards with all possible properties
        /// </summary>
        /// <returns>A list of cards ready for carousel styling</returns>
        private static IList<Attachment> GetCardsAttachments()
        {
            return new List<Attachment>()
            {
                Cards.GetHeroCard(
                    "Student Assistant for the PMO Department",
                    "Maersk Line, Copenhagen Area",
                    "Working as a Student Assistant will give you the opportunity to use your competencies from your studies in daily tasks in a multinational company. You will get responsibility that will help you learn, develop and build new knowledge within your area.",
                    new CardImage(url: "http://i.imgur.com/wBI9LbU.png"),
                    new CardAction(ActionTypes.OpenUrl, "Learn More", value: "http://bit.ly/2r7Sbe7")),
                Cards.GetHeroCard(
                    "Oracle Digital Cloud Solution Specialist",
                    "Oracle, Copenhagen Area",
                    "Oracle (NASDAQ: ORCL) is the world's most complete, open, and integrated business software and hardware systems company. Can you work with our Cloud Platform sales representatives to engage at a senior level with our highest potential mid-market clients?",
                    new CardImage(url: "http://i.imgur.com/PSdvNWI.png"),
                    new CardAction(ActionTypes.OpenUrl, "Learn More", value: "http://bit.ly/2rP7qeB")),
                Cards.GetHeroCard(
                    "Junior Data Specialist",
                    "Miracle Search, Copenhagen Area",
                    "As part of the Databank Team, you will be part of a team providing a large variety of different services. The team is responsible for the operation and development of Deloitte’s IT databank, and your job will be to maintain and structure the data in close cooperation with our global IT organization.",
                    new CardImage(url: "http://i.imgur.com/0guKa68.png"),
                    new CardAction(ActionTypes.OpenUrl, "Learn More", value: "http://bit.ly/2rP7wTv")),

            };
        }
    }
}