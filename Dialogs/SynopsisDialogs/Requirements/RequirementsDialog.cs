using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace chatbot101.Dialogs.SynopsisDialogs.Requirements
{
    [Serializable]
    public class RequirementsDialog : IDialog<object>
    {
        /// <summary>
        /// Closing Dialog
        /// </summary>
        /// <param name="context">The context for the execution of a dialog's conversational process.</param>
        /// <returns>No return type, only a task that represents the state transition</returns>
        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Formal requirements to the synopsis:\nReason(s) for choosing the topic\nSources\nOutline\nReferences (including all sources referred to in the synopsis)\nAppendices (only appendices of core importance to the synopsis)");

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