using System;
using System.Threading.Tasks;
using chatbot101.Dialogs.InternshipDialogs.Contract;
using chatbot101.Dialogs.InternshipDialogs.Vacancies;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace chatbot101.Dialogs.InternshipDialogs
{
    [Serializable]
    public class CheckInternshipDialog : IDialog<object>
    {
        /// <summary>
        /// StartAsync is part of IDialog interface, so it must be implemented
        /// The first medthod which is called in each Dialog
        /// </summary>
        /// <param name="context">The context for the execution of a dialog's conversational process.</param>
        /// <returns>A task that represents the dialog start.</returns>
        public async Task StartAsync(IDialogContext context)
        {
            //sending message to user
            var message = Cards.CustomHeroCardNoPicture(context.MakeMessage(), $"Ok, I undestand! I can show you more, select one to narrow the scope!",
                new string[] { "Contract", "Vacancies" });

            await context.PostAsync(message);

            //state transition: wait for the User to respond
            context.Wait(MessageReceivedOperationChoice);
        }

        /// <summary>
        /// The bot's response to user's activity
        /// </summary>
        /// <param name="context">The context for the execution of a dialog's conversational process.</param>
        /// <param name="result">A message in a conversation</param>
        /// <returns>No return type, only a task that represents the state transition</returns>
        public async Task MessageReceivedOperationChoice(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;


            if (message.Text.Equals("Contract", StringComparison.CurrentCultureIgnoreCase))
            {
                context.Call<object>(new ContractDialog(), ResumAfterChildDialog);
            }
            else if (message.Text.Equals("Vacancies", StringComparison.InvariantCultureIgnoreCase))
            {
                context.Call<object>(new VacanciesDialog(), ResumAfterChildDialog);
            }
            //User has sent something else, for simplycity ignore this input and wait for the next message
            else
            {
                context.Wait(MessageReceivedOperationChoice);
            }
        }

        /// <summary>
        /// Close the current Dialog and removing from the stack
        /// </summary>
        /// <param name="context">The context for the execution of a dialog's conversational process.</param>
        /// <param name="result">If there is a parameter passed from the previous stack during deconstrucion</param>
        /// <returns>No return type, only a task that represents the state transition</returns>
        private async Task ResumAfterChildDialog(IDialogContext context, IAwaitable<object> result)
        {
            await result;
            context.Done<object>(new object());
        }
    }
}