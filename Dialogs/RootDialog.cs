using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AdaptiveCards;
using chatbot101.Dialogs.InternshipDialogs;
using chatbot101.Dialogs.ITSupportDialogs;
using chatbot101.Dialogs.LUISDialogs;
using chatbot101.Dialogs.SynopsisDialogs;
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

            //State transition: the RootDialog initiates and waits for the message from the user
            //waiting for the first message, when received call the method MessageReceivedAsync();
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            //sending message to user
            var message = SimpleCards.CreateSimpleCard(context.MakeMessage(), $"Hi! How can I help you?",
                new string[] {"IT Support", "Internship info", "Synopsis info", "Other..."});

            await context.PostAsync(message);
            //state transition: wait for the User to respond
            context.Wait(MessageReceivedOperationChoice);
        }

        private async Task MessageReceivedOperationChoice(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            //We have a message from the user!
            var message = await argument;

            if (message.Text.Equals("Synopsis info", StringComparison.CurrentCultureIgnoreCase))
            {
                context.Call<object>(new CheckSynopsisDialog(), ResumeAfterChildDialog);
            }
            else if (message.Text.Equals("Internship info", StringComparison.InvariantCultureIgnoreCase))
            {
                context.Call<object>(new CheckInternshipDialog(), ResumeAfterChildDialog);
            }
            else if (message.Text.Equals("IT Support", StringComparison.InvariantCultureIgnoreCase))
            {
                context.Call<object>(new CheckITSupportDialog(), ResumeAfterChildDialog);
            }
            else if (message.Text.Equals("Other...", StringComparison.InvariantCultureIgnoreCase))
            {
                context.Call<object>(new CheckLUISDialog(), ResumeAfterChildDialog);
            }
            else
            {
                context.Wait(MessageReceivedAsync);
            }
            await context.PostAsync(message);
        }

        private async Task ResumeAfterChildDialog(IDialogContext context, IAwaitable<object> result)
        {
            var message = SimpleCards.CreateSimpleCard(context.MakeMessage(),
                $"Can I help you with anything else? {Environment.NewLine}",
                new string[] {"IT Support", "Internship info", "Synopsis info", "Other..."});

            await context.PostAsync(message);

            // State transition - wait for 'operation choice' message from user (loop back)
            context.Wait(MessageReceivedOperationChoice);
        }
    }
}