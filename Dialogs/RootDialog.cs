using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace chatbot101.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        //StartAsync is part of IDialog interface, so it must be implemented
        //The first medthod which is called in each Dialog
        public Task StartAsync(IDialogContext context)
        {
            //waiting for the first message, when received call the method MessageReceivedAsync();
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;

            // calculate something for us to return
            int length = (activity.Text ?? string.Empty).Length;

            // return our reply to the user
            await context.PostAsync($"You sent {activity.Text} which was {length} characters");

            context.Wait(MessageReceivedAsync);
        }
    }
}