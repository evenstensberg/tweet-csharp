using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Tweetinvi;
using Tweetinvi.Models;

namespace tweet_csharp
{
    class Program
    {
        public static void LoadDotEnvFile(string filePath)
        {
            if (!File.Exists(filePath)) return;

            foreach (var line in File.ReadAllLines(filePath))
            {
                var parts =
                    line.Split('=', StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length != 2) continue;

                Environment.SetEnvironmentVariable(parts[0], parts[1]);
            }
        }

        static async Task Main(string[] args)
        {
            var root = Directory.GetCurrentDirectory();
            var dotenv = Path.Combine(root, ".env");
            LoadDotEnvFile (dotenv);
            string CONSUMER_KEY =
                Environment.GetEnvironmentVariable("CONSUMER_KEY");
            string CONSUMER_KEY_SECRET =
                Environment.GetEnvironmentVariable("CONSUMER_KEY_SECRET");
            string ACCESS_TOKEN =
                Environment.GetEnvironmentVariable("ACCESS_TOKEN");
            string ACCESS_TOKEN_SECRET =
                Environment.GetEnvironmentVariable("ACCESS_TOKEN_SECRET");
            var userClient =
                new TwitterClient(CONSUMER_KEY,
                    CONSUMER_KEY_SECRET,
                    ACCESS_TOKEN,
                    ACCESS_TOKEN_SECRET);
            var user = await userClient.Users.GetAuthenticatedUserAsync();
            var tweet = await userClient.Tweets.PublishTweetAsync("Have a nice weekend! :)");
            Console.WriteLine (tweet);
        }
    }
}
