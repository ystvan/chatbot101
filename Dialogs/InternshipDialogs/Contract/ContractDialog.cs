using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Bot.Builder.Dialogs;

namespace chatbot101.Dialogs.InternshipDialogs.Contract
{
    public class ContractDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("You can download the approved contract formular from the following page: @'https://zibat.dk/contract', ");

            // State transition - complete this Dialog and remove it from the stack
            context.Done<object>(new object());
        }
    }
}