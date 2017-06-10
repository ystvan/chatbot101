using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Bot.Builder.Dialogs;

namespace chatbot101.Dialogs.InternshipDialogs.Vacancies
{
    public class VacanciesDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Sorry, currently there are no vacant positions offered by any company!");

            // State transition - complete this Dialog and remove it from the stack
            context.Done<object>(new object());
        }
    }
}