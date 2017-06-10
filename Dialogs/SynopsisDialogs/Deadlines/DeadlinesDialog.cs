using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Bot.Builder.Dialogs;

namespace chatbot101.Dialogs.SynopsisDialogs.Deadlines
{
    [Serializable]
    public class DeadlinesDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("The deadline for handing-in your report on wiseflow is 02/06 11AM");

            // State transition - complete this Dialog and remove it from the stack
            context.Done<object>(new object());
        }
    }
}