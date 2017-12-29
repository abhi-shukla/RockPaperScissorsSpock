using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using GameApp.Models;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace GameApp
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            if(activity.Type == ActivityTypes.Message)
            {
                var connector = new ConnectorClient(new Uri(activity.ServiceUrl));

                var game = new Game();

                string message = game.Play(activity.Text);

                Activity reply = activity.CreateReply(message);

                await connector.Conversations.ReplyToActivityAsync(reply);
            }
            else
            {
                HandleSystemMessage(activity);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        private Activity HandleSystemMessage(Activity message)
        {
            if(message.Type == ActivityTypes.DeleteUserData)
            {

            }
            else if(message.Type == ActivityTypes.ConversationUpdate)
            {

            }
            else if(message.Type == ActivityTypes.ContactRelationUpdate)
            {

            }
            else if(message.Type == ActivityTypes.Typing)
            {

            }
            else if(message.Type == ActivityTypes.Ping)
            {

            }

            return null;
        }
    }
}
