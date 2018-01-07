using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using GameApp.Models;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.ConnectorEx;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;

namespace GameApp
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            HttpStatusCode statusCode = HttpStatusCode.OK;

            var connector = new ConnectorClient(new Uri(activity.ServiceUrl));

            if (activity.Type == ActivityTypes.Message)
            {
                string message = await GetMessage(connector, activity);
                Activity reply = activity.BuildMessageActivity(message);
                await connector.Conversations.ReplyToActivityAsync(reply);

                SaveActivity(activity);
            }
            else
            {
                try
                {
                    await new SystemMessages().Handle(connector, activity);
                }
                catch(HttpException ex)
                {
                    statusCode = (HttpStatusCode) ex.GetHttpCode();
                }
            }

            HttpResponseMessage response = Request.CreateResponse(statusCode);
            return response;
        }

        void SaveActivity(Activity activity)
        {
            ConversationReference convRef = activity.ToConversationReference();
            string convRefJson = JsonConvert.SerializeObject(convRef);
            string path = HttpContext.Current.Server.MapPath(@"..\ConversationReference.json");
            File.WriteAllText(path, convRefJson);
        }

        async Task<string> GetMessage(ConnectorClient connector, Activity activity)
        {
            var state = new GameState();

            string userText = activity.Text.ToLower();
            string message = string.Empty;

            if (userText.Contains(value: "score"))
            {
                message = await state.GetScoresAsync(connector, activity);
            }
            else if (userText.Contains(value: "delete"))
            {
                message = await state.DeleteScoresAsync(activity);
            }
            else
            {
                var game = new Game();
                message = game.Play(userText);

                bool isValidInput = !message.StartsWith("Type");
                if (isValidInput)
                {
                    if (message.Contains(value: "Tie"))
                    {
                        await state.AddTieAsync(activity);
                    }
                    else
                    {
                        bool userWin = message.Contains(value: "win");
                        await state.UpdateScoresAsync(activity, userWin);
                    }
                }
            }

            return message;
        }
    }
}
