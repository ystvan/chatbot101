using System;
using Microsoft.Bot.Builder.FormFlow;

namespace chatbot101.Dialogs.LUISDialogs.Models
{
    [Serializable]
    public class MeetingsQuery
    {
        [Prompt("When would you like to meet?")]
        [Optional]
        public string Date { get; set; }

        [Prompt("Who would you like to meet with?")]
        [Optional]
        public string Name { get; set; }
    }
}