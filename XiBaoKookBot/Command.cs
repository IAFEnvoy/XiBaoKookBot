using Kook.WebSocket;

namespace XiBaoKookBot {
    internal class Command {
        public static void OnCommandReceive(SocketMessage message, KookSocketClient client) {
            string[] keys = message.Content.Split(' ');
            if (keys[0] != "/xb") return;
            if (keys.Length != 3) return;
            Image? image = Generator.Render(keys[1], keys[2]);
            if (image == null) {
                message.Channel.SendTextAsync("生成的时候粗了亿点点小问题");
                return;
            }
            string path = @$"{Application.StartupPath}\temporary\{keys[1]}-{keys[2]}.png";
            image.Save(path);
            message.Channel.SendFileAsync(path);
        }
    }
}
