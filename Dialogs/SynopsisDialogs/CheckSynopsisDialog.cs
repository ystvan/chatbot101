using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using chatbot101.Dialogs.LUISDialogs;
using chatbot101.Dialogs.SynopsisDialogs.Deadlines;
using chatbot101.Dialogs.SynopsisDialogs.Exam;
using chatbot101.Dialogs.SynopsisDialogs.Requirements;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace chatbot101.Dialogs.SynopsisDialogs
{
    [Serializable]
    public class CheckSynopsisDialog : IDialog<object>
    {
        /// <summary>
        /// StartAsync is part of IDialog interface, so it must be implemented
        /// The first medthod which is called in each Dialog
        /// </summary>
        /// <param name="context">The context for the execution of a dialog's conversational process.</param>
        /// <returns>A task that represents the dialog start.</returns>
        public async Task StartAsync(IDialogContext context)
        {
            //sending message to user
            var message = Cards.CustomHeroCard(context.MakeMessage(), $"I narrowed the scope! Do you find any useful below?", "This is the Synopsis menu", "You can tap or type to reply and I'll collect all the necessary info available currently on Fronter", "",
                new string[] { "Deadlines", "Exam", "Requirements" });

            await context.PostAsync(message);

            //state transition: wait for the User to respond
            context.Wait(MessageReceivedOperationChoice);
        }

        /// <summary>
        /// The bot's response to user's activity
        /// </summary>
        /// <param name="context">The context for the execution of a dialog's conversational process.</param>
        /// <param name="result">A message in a conversation</param>
        /// <returns>No return type, only a task that represents the state transition</returns>
        public async Task MessageReceivedOperationChoice(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;

            
            if (message.Text.Equals("Deadlines", StringComparison.CurrentCultureIgnoreCase))
            {
                context.Call<object>(new DeadlinesDialog(), ResumAfterChildDialog);
            }
            else if (message.Text.Equals("Exam", StringComparison.InvariantCultureIgnoreCase))
            {
                context.Call<object>(new ExamDialog(), ResumAfterChildDialog);
            }
            else if (message.Text.Equals("Requirements", StringComparison.InvariantCultureIgnoreCase))
            {
                context.Call<object>(new RequirementsDialog(), ResumAfterChildDialog);
            }
            //User has sent something else, for simplycity ignore this input and wait for the next message
            else
            {
                context.Wait(MessageReceivedOperationChoice);
            }
        }

        /// <summary>
        /// Close the current Dialog and removing from the stack
        /// </summary>
        /// <param name="context">The context for the execution of a dialog's conversational process.</param>
        /// <param name="result">If there is a parameter passed from the previous stack during deconstrucion</param>
        /// <returns>No return type, only a task that represents the state transition</returns>
        private async Task ResumAfterChildDialog(IDialogContext context, IAwaitable<object> result)
        {
            await result;
            context.Done<object>(new object());
        }

    }
}