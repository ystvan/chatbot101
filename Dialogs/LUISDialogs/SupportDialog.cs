using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace chatbot101.Dialogs.LUISDialogs
{
    /// <summary>
    /// A mimicking a support team response for error calls
    /// </summary>
    [Serializable]
    public class SupportDialog : IDialog<int>
    {
        /// <summary>
        /// StartAsync is part of IDialog interface, so it must be implemented
        /// The first medthod which is called in each Dialog
        /// </summary>
        /// <param name="context">The context for the execution of a dialog's conversational process.</param>
        /// <returns>A task that represents the dialog start.</returns>
        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(this.MessageReceivedAsync);
        }

        /// <summary>
        /// The bot's response to user's activity
        /// </summary>
        /// <param name="context">The context for the execution of a dialog's conversational process.</param>
        /// <param name="result">A message in a conversation</param>
        /// <returns>No return type, HOWEVER it passes back an integer (the ticketnumber) to the method which has called the SupportDialog</returns>
        public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;

            var ticketNumber = new Random().Next(0, 20000);

            await context.PostAsync($"Your message '{message.Text}' was registered. Once we resolve it; we will get back to you.");

            context.Done(ticketNumber);
        }
    }
}