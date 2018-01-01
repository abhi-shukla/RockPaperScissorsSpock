using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GameApp.Models
{
    public class SystemMessages
    {
        public async Task Handle(ConnectorClient connector, Activity message)
        {
            switch(message.Type)
            {
                case ActivityTypes.ContactRelationUpdate:
                    HandleContactRelation(message);
                    break;
                case ActivityTypes.ConversationUpdate:
                    HandleConversationUpdateAsync(connector, message);
                    break;
                case ActivityTypes.DeleteUserData:
                    HandleDeleteUserDataAsync(message);
                    break;
                case ActivityTypes.Ping:
                    HandlePing(message);
                    break;
                case ActivityTypes.Typing:
                    HandleTyping(message);
                    break;
                default:
                    break;
            }
        }

        void HandleContactRelation(IContactRelationUpdateActivity activity)
        {
            if(activity.Action == "add")
            {
                // user has added chatbot to contact list
            }
            else
            {
                // user has removed chatbot from the contact list
            }
        }

        async Task HandleConversationUpdateAsync(ConnectorClient connector, IConversationUpdateActivity activity)
        {
            const string welcomeMessage = "Welcome to the Rock, Paper, Scissors, Lizard, Spock game! " +
                "To begin, type \"rock\", \"paper\", \"scissors\", \"lizard\" or \"spock\". " +
                "To know the current scores type \"score\" and to remove all your info type \"delete\"";

            Func<ChannelAccount, bool> isBot =
                channelAcct => channelAcct.Id == activity.Recipient.Id;

            if(activity.MembersAdded.Any())
            {
                Activity replyActivity = (activity as Activity).CreateReply(welcomeMessage);
                await connector.Conversations.ReplyToActivityAsync(replyActivity);
            }

            if(activity.MembersRemoved.Any())
            {
                // do memeber removal stuff
            }
        }

        async Task HandleDeleteUserDataAsync(Activity activity)
        {
            await new GameState().DeleteScoresAsync(activity);
        }

        void HandlePing(IActivity activity)
        {
            // Handle 401 and 403 exceptions
        }

        void HandleTyping(ITypingActivity activity)
        {
            // show the user typing status since user has started typing and not submitted the message yet.
        }
    }
}