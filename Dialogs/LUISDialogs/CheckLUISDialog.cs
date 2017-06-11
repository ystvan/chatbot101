using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace chatbot101.Dialogs.LUISDialogs
{
    [Serializable]
    public class CheckLuisDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            //to be implemented
            await context.PostAsync("Come back anytime soon, I am still figuring out this menu!");

            context.Wait(DeconstructionOfDialog);
        }

        private async Task DeconstructionOfDialog(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;
            // State transition - complete this Dialog and remove it from the stack
            context.Done<object>(new object());
        }
    }
}