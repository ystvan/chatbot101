using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using chatbot101.Dialogs.LUISDialogs.Models;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;

namespace chatbot101.Dialogs.LUISDialogs
{
    [LuisModel("6eeb5164-90ed-42e9-a3ee-8e1c414a5e78", "7b611596b0264a1eb0c40d2c3e9d81bd", domain: "westeurope.api.cognitive.microsoft.com")]
    [Serializable]
    public class CheckLuisDialog : LuisDialog<object>
    {
        private const string MeetingDate = "Date";
        private const string Teacher = "Name";

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
            await context.PostAsync($"I am analysing your message: '{message.Text}'...");

            var meetingsQuery = new MeetingsQuery();

            EntityRecommendation teacherEntityRecommendation;
            //EntityRecommendation dateEntityRecommendation;

            if (result.TryFindEntity(Teacher, out teacherEntityRecommendation))
            {
                teacherEntityRecommendation.Type = "Teacher";
            }

            var meetingsFormDialog = new FormDialog<MeetingsQuery>(meetingsQuery, this.BuildMeetingsForm, FormOptions.PromptInStart, result.Entities);
            context.Call(meetingsFormDialog, this.ResumeAfterMeetingsFormDialog);
            
        }

        private async Task ResumeAfterMeetingsFormDialog(IDialogContext context, IAwaitable<MeetingsQuery> result)
        {
            try
            {
                var searchQuery = await result;

                var meetings = await this.GetMeetingsAsync(searchQuery);

                await context.PostAsync($"I found {meetings.Count()} available slots:");

                var resultMessage = context.MakeMessage();
                resultMessage.AttachmentLayout = AttachmentLayoutTypes.Carousel;
                resultMessage.Attachments = new List<Attachment>();

                foreach (var meeting in meetings)
                {
                    HeroCard heroCard = new HeroCard()
                    {
                        Title = meeting.Teacher,
                        Subtitle = meeting.Location,
                        Text = meeting.DateTime,
                        Images = new List<CardImage>()
                        {
                            new CardImage() {Url = meeting.Image}
                        },
                        Buttons = new List<CardAction>()
                        {
                            new CardAction()
                            {
                                Title = "Book Appointment",
                                Type = ActionTypes.OpenUrl,
                                Value = $"https://www.bing.com/search?q=easj+roskilde+" + HttpUtility.UrlEncode(meeting.Location)
                            }
                        }
                    };

                    resultMessage.Attachments.Add(heroCard.ToAttachment());
                }

                await context.PostAsync(resultMessage);
            }
            catch (FormCanceledException ex)
            {
                string reply;

                if (ex.InnerException == null)
                {
                    reply = "You have canceled the operation.";
                }
                else
                {
                    reply = $"Oops! Something went wrong :( Technical Details: {ex.InnerException.Message}";
                }

                await context.PostAsync(reply);
            }
            finally
            {
                context.Wait(DeconstructionOfDialog);
            }
        }

        private async Task<IEnumerable<Meeting>> GetMeetingsAsync(MeetingsQuery searchQuery)
        {
            var meetings = new List<Meeting>();

            //some random result manually for demo purposes
            for (int i = 1; i <= 5; i++)
            {
                var random = new Random(i);
                Meeting meeting = new Meeting()
                {
                    DateTime = $" Available time: {searchQuery.Date} At {i} sharp",
                    Teacher = $" Professor {searchQuery.Name}",
                    Location = $" Elisagårdsvej 3, Room {random.Next(1, 300)}",
                    Image = $"https://placeholdit.imgix.net/~text?txtsize=35&txt=Supervision+{i}&w=500&h=260"
                };

                meetings.Add(meeting);
            }

            return meetings;
        }

        private IForm<MeetingsQuery> BuildMeetingsForm()
        {
            OnCompletionAsyncDelegate<MeetingsQuery> processMeetingsSearch = async (context, state) =>
            {
                var message = "Searching for supervision slots";
                if (!string.IsNullOrEmpty(state.Date))
                {
                    message += $" at {state.Date}...";
                }
                else if (!string.IsNullOrEmpty(state.Name))
                {
                    message += $" with professor {state.Name.ToUpperInvariant()}...";
                }
                await context.PostAsync(message);
            };

            return new FormBuilder<MeetingsQuery>()
                .Field(nameof(MeetingsQuery.Date), (state) => string.IsNullOrEmpty(state.Date))
                .Field(nameof(MeetingsQuery.Name), (state) => string.IsNullOrEmpty(state.Name))
                .OnCompletion(processMeetingsSearch)
                .Build();
        }


        [LuisIntent("Help")]
        public async Task Help(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Try asking me things like 'book a meeting with Peter', 'I need help with my project' or 'schedule appointment for tomorrow'");

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