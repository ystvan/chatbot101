using System;
using System.Threading;
using System.Threading.Tasks;
using chatbot101.Dialogs.InternshipDialogs;
using chatbot101.Dialogs.LUISDialogs;
using chatbot101.Dialogs.SynopsisDialogs;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace chatbot101.Dialogs
{

    //IDialog is a suspendable conversational process that produces a result of type TResult.
    //The start of the code that represents the conversational dialog.
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        /// <summary>
        /// StartAsync is part of IDialog interface, so it must be implemented
        /// The first medthod which is called in each Dialog
        /// </summary>
        /// <param name="context">The context for the execution of a dialog's conversational process.</param>
        /// <returns>A task that represents the dialog start.</returns>
        public Task StartAsync(IDialogContext context)
        {

            //State transition: the RootDialog initiates and waits for the message from the user
            //waiting for the first message, when received: call the method MessageReceivedAsync();
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        /// <summary>
        /// The bot's response to user's activity
        /// </summary>
        /// <param name="context">The context for the execution of a dialog's conversational process.</param>
        /// <param name="argument">A message in a conversation</param>
        /// <returns>No return type, only a task that represents the state transition</returns>
        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            //sending message to user
            var replyMessage = Cards.CustomHeroCard(context.MakeMessage(), $"Hi, I am ZIBOT! How can I help you?", "This is the main menu", "You can tap or type to reply", "http://i.imgur.com/hwQjecp.jpg",
                new string[] { "Internship info", "Synopsis info", "Other..." });

            await context.PostAsync(replyMessage);
            
            //state transition: wait for the User to respond
            context.Wait(MessageReceivedOperationChoice);
        }
        
        /// <summary>
        /// The bot's response to user's activity
        /// </summary>
        /// <param name="context">The context for the execution of a dialog's conversational process.</param>
        /// <param name="argument">A message in a conversation</param>
        /// <returns>No return type, only a task that represents the state transition</returns>
        private async Task MessageReceivedOperationChoice(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            //We have a message from the user!
            var message = await argument;


            //User has chosen, so invoke the relevant Dialog and wait for it to finish, then call the 'callback' Dialog

            if (message.Text.ToLower().Contains("help") || message.Text.ToLower().Contains("support") || message.Text.ToLower().Contains("problem"))
            {
                await context.Forward(new SupportDialog(), this.ResumeAfterSupportDialog, message, CancellationToken.None);
            }
            else if (message.Text.Equals("Synopsis info", StringComparison.CurrentCultureIgnoreCase))
            {
                context.Call<object>(new CheckSynopsisDialog(), ResumeAfterOptionDialog);
            }
            else if (message.Text.Equals("Internship info", StringComparison.InvariantCultureIgnoreCase))
            {
                context.Call<object>(new CheckInternshipDialog(), ResumeAfterOptionDialog);
            }
            else if (message.Text.Equals("Other...", StringComparison.InvariantCultureIgnoreCase))
            {
                context.Call<object>(new CheckLUISDialog(), ResumeAfterOptionDialog);
            }
            //User has sent something else, for simplycity ignore this input and wait for the next message
            else
            {
                context.Wait(MessageReceivedAsync);
            }

            
        }

        /// <summary>
        /// A callback Dialog which provides the support ticket number reference
        /// </summary>
        /// <param name="context">The context for the execution of a dialog's conversational process.</param>
        /// <param name="result">The ticket number passed from the previous Dialog</param>
        /// <returns></returns>
        private async Task ResumeAfterSupportDialog(IDialogContext context, IAwaitable<int> result)
        {
            var ticketNumber = await result;

            await context.PostAsync($"Thanks for contacting our Customer Service Support Team. Your ticket number is {ticketNumber}.");

            //loop back to start if user sends a message again
            context.Wait(this.MessageReceivedAsync);
        }

        /// <summary>
        /// The 'callback' function (method) after the child dialog's finished or closed /with context.Done();/
        /// </summary>
        /// <param name="context">The context for the execution of a dialog's conversational process.</param>
        /// <param name="result">If there is a parameter passed from the previous stack during deconstrucion</param>
        /// <returns>No return type, only a task that represents the state transition</returns>
        private async Task ResumeAfterOptionDialog(IDialogContext context, IAwaitable<object> result)
        {
            try
            {
                var message = await result;
                if (message.ToString().ToLower().Contains("thank"))
                {
                    await context.PostAsync("My pleasure!");
                }
            }
            catch (Exception ex)
            {
                await context.PostAsync($"Oops, something went wrong: {ex.Message}");
            }
            finally
            {
                var replyMessage = Cards.CustomHeroCard(context.MakeMessage(), $"Anything else I can do you for?", "This is the main menu", "You can tap or type to reply", "http://i.imgur.com/hwQjecp.jpg",
                new string[] { "Internship info", "Synopsis info", "Other..." });

                await context.PostAsync(replyMessage);
                // State transition - wait for 'operation choice' message from user (loop back)
                context.Wait(this.MessageReceivedOperationChoice);
            }

        }
    }
}