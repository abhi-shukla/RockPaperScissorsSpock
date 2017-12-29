using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameApp.Models
{
    public class Game
    {
        readonly Dictionary<PlayType, string> rockPlays = new Dictionary<PlayType, string>
        {
            [PlayType.Paper] = "Paper covers Rock - You lose!!",
            [PlayType.Scissors] = "Rock crushes Scissors - You Win!!",
            [PlayType.Lizard] = "Rock crushes Lizard - You win!!",
            [PlayType.Spock] = "Spock vaporizes Rock - You lose!!"
        };

        readonly Dictionary<PlayType, string> paperPlays = new Dictionary<PlayType, string>
        {
            [PlayType.Rock] = "Paper cover Rock - You win!!",
            [PlayType.Scissors] = "Scissors cut Paper - You lose!!",
            [PlayType.Lizard] = "Lizard eats Paper - You lose!!",
            [PlayType.Spock] = "Paper disproves Spock - You win!!"
        };

        readonly Dictionary<PlayType, string> scissorsPlays = new Dictionary<PlayType, string>
        {
            [PlayType.Paper] = "Scissors cut Paper - You win!!",
            [PlayType.Rock] = "Rock crushes Scissors - You lose!!",
            [PlayType.Lizard] = "Scissors decapitates Lizard - You win!!",
            [PlayType.Spock] = "Spock smashes Scissors - You lose!!"
        };

        readonly Dictionary<PlayType, string> lizardPlays = new Dictionary<PlayType, string>
        {
            [PlayType.Paper] = "Lizard eats Paper - You win!!",
            [PlayType.Rock] = "Rock crushes Lizard - You lose!!",
            [PlayType.Scissors] = "Scissors decapitates Lizard - You lose!!",
            [PlayType.Spock] = "Lizard poisons Spock - You win!!"
        };

        readonly Dictionary<PlayType, string> spockPlays = new Dictionary<PlayType, string>
        {
            [PlayType.Rock] = "Spock vaporizes Rock - You win!!",
            [PlayType.Paper] = "Paper disproves Spock - You lose!!",
            [PlayType.Scissors] = "Spock smashes Scissors - You win!!",
            [PlayType.Lizard] = "Lizard poisons Spock - You lose!!"
        };

        public string Play(string userText)
        {
            string message = "";

            PlayType userPlay;

            bool isValidPlay = Enum.TryParse(userText, ignoreCase: true, result: out userPlay);

            if(isValidPlay)
            {
                PlayType botPlay = GetBotPlay();
                message = Compare(userPlay, botPlay);
            }
            else
            {
                message = "I can only play Rock, Paper, Scrissors, Lizard, Spock";
            }

            return message;
        }

        public PlayType GetBotPlay()
        {
            long seed = DateTime.Now.Ticks;

            var rnd = new Random(unchecked((int)seed));
            int position = rnd.Next(maxValue: 5);

            return (PlayType)position;
        }

        public string Compare(PlayType userPlay, PlayType botPlay)
        {
            string plays = $"You: {userPlay}, Bot: {botPlay}";
            string result = string.Empty;

            if(userPlay == botPlay)
            {
                result = "Tie!!";
            }
            else
            {
                switch(userPlay)
                {
                    case PlayType.Rock:
                        result = rockPlays[botPlay];
                        break;
                    case PlayType.Paper:
                        result = paperPlays[botPlay];
                        break;
                    case PlayType.Scissors:
                        result = scissorsPlays[botPlay];
                        break;
                    case PlayType.Lizard:
                        result = lizardPlays[botPlay];
                        break;
                    case PlayType.Spock:
                        result = spockPlays[botPlay];
                        break;
                }
            }

            return $"{plays}. {result}";
        }
    }
}