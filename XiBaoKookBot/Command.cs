using Kook.WebSocket;
using System.Drawing.Imaging;

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
            MemoryStream ms = new();
            image.Save(ms, ImageFormat.Png);
            message.Channel.SendFileAsync(ms, $"{keys[1]}-{keys[2]}.png", Kook.AttachmentType.Image);
        }
    }
}
