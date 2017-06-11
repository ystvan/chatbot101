using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace chatbot101.Dialogs.InternshipDialogs.Contract
{
    [Serializable]
    public class ContractDialog : IDialog<object>
    {
        /// <summary>
        /// Closing Dialog
        /// </summary>
        /// <param name="context">The context for the execution of a dialog's conversational process.</param>
        /// <returns>No return type, only a task that represents the state transition</returns>
        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("You can download the approved contract formular from the following page: https://zibat.dk/contract");
            await context.PostAsync("All you need to do is to have it signed by the school and your company, and you're good to go!");

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
    }
}