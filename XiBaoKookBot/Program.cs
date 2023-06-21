using Kook;
using Kook.WebSocket;
using System.Runtime.InteropServices;
using XiBaoKookBot.Utils;

namespace XiBaoKookBot {
    internal class Program {
        public static BotConfig config = new(@$"{Application.StartupPath}\main.json");
        private KookSocketClient client=new KookSocketClient();

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        [STAThread]
        public static Task Main(string[] args) {
            return new Program().MainAsync();
        }

        public async Task MainAsync() {
            AllocConsole();

            this.client.Log += this.Log;
            this.client.MessageReceived += this.Client_MessageReceived;

            if (config.kookBotToken == "") {
                Logger.Error("Œ¥’“µΩtoken");
                Environment.Exit(0);
            }
            Generator.LoadTemplate();
            await this.client.LoginAsync(TokenType.Bot, config.kookBotToken);
            await this.client.StartAsync();

            await Task.Delay(Timeout.Infinite);
        }

        private Task Client_MessageReceived(SocketMessage message, SocketGuildUser user, SocketTextChannel channel) {
            Logger.Message($"<- TEXT {message.Author.Username} ({message.Author.Id}) [{message.Channel.Name}] {message.Content}");
            if (message.Author.IsBot ?? false) return Task.CompletedTask;
            if (message.Content == "/whoisnigga") return message.Channel.SendTextAsync("NotLegit");
            if (message.Content.StartsWith("/xb")) Command.OnCommandReceive(message, this.client);
            return Task.CompletedTask;
        }

        private Task Log(LogMessage msg) {
            Logger.Info($"[{msg.Source}] {msg.Message}");
            return Task.CompletedTask;
        }
    }
}