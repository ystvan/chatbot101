using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using chatbot101.Dialogs;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace chatbot101
{
    [BotAuthentication]
    //This API Controller is an application endpoint for the Bot Framework
    public class MessagesController : ApiController
    {
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            //user sends something, and that's a message
            if (activity.Type == ActivityTypes.Message)
            {
                await SendTypingActivity(activity);

                //bot starts the conversation by firing up the RootDialog
                await Conversation.SendAsync(activity, () => new RootDialog());
            }
            //user sends a picture, or other Activity happens
            else
            {
                HandleSystemMessage(activity);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        //sending typing activity, to show the bot is compiling the answer for user's input
        private static async Task SendTypingActivity(Activity activity)
        {
            var connector = new ConnectorClient(new Uri(activity.ServiceUrl));
            Activity isTypingReply = activity.CreateReply();
            isTypingReply.Type = ActivityTypes.Typing;
            await connector.Conversations.ReplyToActivityAsync(isTypingReply);
            Thread.Sleep(2000);
        }


        //Handle other Activties
        private Activity HandleSystemMessage(Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
                // Handle conversation state changes, like members being added and removed
                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                // Not available in all channels
            }
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            }
            else if (message.Type == ActivityTypes.Typing)
            {
                // Handle knowing that the user is typing
            }
            else if (message.Type == ActivityTypes.Ping)
            {
            }

            return null;
        }
    }
}