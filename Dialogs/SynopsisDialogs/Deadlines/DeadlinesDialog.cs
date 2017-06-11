using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace chatbot101.Dialogs.SynopsisDialogs.Deadlines
{
    [Serializable]
    public class DeadlinesDialog : IDialog<object>
    {
        /// <summary>
        /// StartAsync is part of IDialog interface, so it must be implemented
        /// The first medthod which is called in each Dialog
        /// </summary>
        /// <param name="context">The context for the execution of a dialog's conversational process.</param>
        /// <returns>A task that represents the dialog start.</returns>
        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("The deadline for handing-in your report on wiseflow is 02/06 11AM");

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