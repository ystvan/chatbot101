using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Bot.Builder.Dialogs;

namespace chatbot101.Dialogs.SynopsisDialogs.Requirements
{
    [Serializable]
    public class RequirementsDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Formal requirements to the synopsis:\nReason(s) for choosing the topic\nSources\nOutline\nReferences (including all sources referred to in the synopsis)\nAppendices (only appendices of core importance to the synopsis)");

            // State transition - complete this Dialog and remove it from the stack
            context.Done<object>(new object());
        }
    }
}