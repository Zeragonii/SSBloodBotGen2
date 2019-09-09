using System;
using Discord;
using Discord.WebSocket;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Data.SQLite;

namespace SSBloodBotGen2
{
    public class Program
    {

        static void Main(string[] args)
        => new Program().StartAsync().GetAwaiter().GetResult();

        private DiscordSocketClient _client;

        private CommandHandler _handler;

        public async Task StartAsync()
        {
            _client = new DiscordSocketClient();

            await _client.LoginAsync(TokenType.Bot, "NDkxNjYwNjUzMTYxODczNDA4.DoLVGQ.WHV7jLS6vC3HdOTonx7wKEU2Dig");

            await _client.StartAsync();

            _handler = new CommandHandler(_client);

            await Task.Delay(-1);
        }

    }
}
