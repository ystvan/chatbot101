using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AdaptiveCards;
using chatbot101.Dialogs.InternshipDialogs;
using chatbot101.Dialogs.LUISDialogs;
using chatbot101.Dialogs.SynopsisDialogs;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace chatbot101.Dialogs
{
    [Serializable]

    //IDialog is a suspendable conversational process that produces a result of type TResult.
    //The start of the code that represents the conversational dialog.
    public class RootDialog : IDialog<object>
    {
        /// <summary>
        /// StartAsync is part of IDialog interface, so it must be implemented
        /// The first medthod which is called in each Dialog
        /// </summary>
        /// <param name="context">The context for the execution of a dialog's conversational process.</param>
        /// <returns>A task that represents the dialog start.</returns>
        public Task StartAsync(IDialogContext context)
        {

            //State transition: the RootDialog initiates and waits for the message from the user
            //waiting for the first message, when received: call the method MessageReceivedAsync();
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        /// <summary>
        /// The bot's response to user's activity
        /// </summary>
        /// <param name="context">The context for the execution of a dialog's conversational process.</param>
        /// <param name="argument">A message in a conversation</param>
        /// <returns>No return type, only a task that represents the state transition</returns>
        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            //sending message to user
            var message = SimpleCards.CreateSimpleCard(context.MakeMessage(), $"Hi! How can I help you?",
                new string[] {"Internship info", "Synopsis info", "Other..."});

            await context.PostAsync(message);

            //state transition: wait for the User to respond
            context.Wait(MessageReceivedOperationChoice);
        }

        /// <summary>
        /// The bot's response to user's activity
        /// </summary>
        /// <param name="context">The context for the execution of a dialog's conversational process.</param>
        /// <param name="argument">A message in a conversation</param>
        /// <returns>No return type, only a task that represents the state transition</returns>
        private async Task MessageReceivedOperationChoice(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            //We have a message from the user!
            var message = await argument;

            if (message.Text.Equals("Synopsis info", StringComparison.CurrentCultureIgnoreCase))
            {
                context.Call<object>(new CheckSynopsisDialog(), ResumeAfterChildDialog);
            }
            else if (message.Text.Equals("Internship info", StringComparison.InvariantCultureIgnoreCase))
            {
                context.Call<object>(new CheckInternshipDialog(), ResumeAfterChildDialog);
            }
            else if (message.Text.Equals("Other...", StringComparison.InvariantCultureIgnoreCase))
            {
                context.Call<object>(new CheckLUISDialog(), ResumeAfterChildDialog);
            }
            else
            {
                context.Wait(MessageReceivedAsync);
            }
            await context.PostAsync(message);
        }

        /// <summary>
        /// The 'callback' function (method) after the child dialog's finished or closed /with context.Done();/
        /// </summary>
        /// <param name="context">The context for the execution of a dialog's conversational process.</param>
        /// <param name="result">If there is a parameter passed from the previous stack deconstrucion</param>
        /// <returns>No return type, only a task that represents the state transition</returns>
        private async Task ResumeAfterChildDialog(IDialogContext context, IAwaitable<object> result)
        {
            var message = SimpleCards.CreateSimpleCard(context.MakeMessage(),
                $"Can I help you with anything else? {Environment.NewLine}",
                new string[] {"Internship info", "Synopsis info", "Other..."});

            await context.PostAsync(message);

            // State transition - wait for 'operation choice' message from user (loop back)
            context.Wait(MessageReceivedOperationChoice);
        }
    }
}