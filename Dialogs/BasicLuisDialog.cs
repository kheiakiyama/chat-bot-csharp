using System;
using System.Configuration;
using System.Threading.Tasks;

using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;

namespace Microsoft.Bot.Sample.LuisBot
{
    // For more information about this template visit http://aka.ms/azurebots-csharp-luis
    [Serializable]
    public class BasicLuisDialog : LuisDialog<object>
    {
        public BasicLuisDialog() : base(new LuisService(new LuisModelAttribute(ConfigurationManager.AppSettings["LuisAppId"], ConfigurationManager.AppSettings["LuisAPIKey"])))
        {
        }

        [LuisIntent("None")]
        public async Task NoneIntent(IDialogContext context, LuisResult result)
        {
            await context.PostAsync($"You have reached the none intent. You said: {result.Query}"); //
            context.Wait(MessageReceived);
        }

        [LuisIntent("GetAmazonRanking")]
        public async Task GetAmazonRankingIntent(IDialogContext context, LuisResult result)
        {
            await context.PostAsync($"You have reached the GetAmazonRanking intent. You said: {GetLuisResultString(result)}"); //
            context.Wait(MessageReceived);
        }

        [LuisIntent("GetServiceOfMicrosoft")]
        public async Task GetServiceOfMicrosoftIntent(IDialogContext context, LuisResult result)
        {
            await context.PostAsync($"You have reached the GetServiceOfMicrosoft intent. {GetLuisResultString(result)}"); //
            context.Wait(MessageReceived);
        }

        private string GetLuisResultString(LuisResult result)
        {
            var msg = "";
            foreach(var item in result.Entities)
                msg += $"Entity:{item.ToString()},";
            foreach (var item in result.CompositeEntities)
                msg += $"CEntity:{item.ToString()},";
            msg += $"TopScoringIntent:{result.TopScoringIntent.Intent},Query:{result.Query}";
            return msg;
        }
    }
}