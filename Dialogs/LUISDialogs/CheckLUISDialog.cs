using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;

namespace chatbot101.Dialogs.LUISDialogs
{
    [LuisModel("6eeb5164-90ed-42e9-a3ee-8e1c414a5e78", "7b611596b0264a1eb0c40d2c3e9d81bd")]
    [Serializable]
    public class CheckLuisDialog : LuisDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            //to be implemented
            await context.PostAsync("So, how can I help?");

            context.Wait(this.MessageReceived);
        }

        [LuisIntent("")]
        [LuisIntent("None")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            string message = $"Sorry, I did not understand '{result.Query}'. Type 'help' if you need assistance.";

            await context.PostAsync(message);

            context.Wait(this.MessageReceived);
        }


        [LuisIntent("BookSupervision")]
        public async Task BookAppointment(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            var message = await activity;

            //do a fromflow to book an appointment and show caarousel calendar elements
        }


        [LuisIntent("Help")]
        public async Task Help(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Hi! Try asking me things like 'book a meeting with Peter', 'I need help with my project' or 'schedule appointment for tomorrow'");

            context.Wait(this.MessageReceived);
        }


        private async Task DeconstructionOfDialog(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;
            // State transition - complete this Dialog and remove it from the stack
            context.Done<object>(new object());
        }
    }
}