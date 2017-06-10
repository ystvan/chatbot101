using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Bot.Builder.Dialogs;

namespace chatbot101.Dialogs.SynopsisDialogs.Exam
{
    [Serializable]
    public class ExamDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("The exam schedule will be available on next week! Please try again later.");

            // State transition - complete this Dialog and remove it from the stack
            context.Done<object>(new object());
        }
    }
}