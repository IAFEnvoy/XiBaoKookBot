namespace XiBaoKookBot {
    internal class Generator {
        private static readonly Dictionary<string, Template> templates = new();
        public static void LoadTemplate() {
            string[] files = Directory.GetFiles(@$"{Application.StartupPath}\template");
            foreach (string file in files) {
                FileInfo info = new(file);
                if (info.Extension == ".txt") {
                    StreamReader sr = new StreamReader(file);
                    string path = @$"{Application.StartupPath}\template\{sr.ReadLine()}";
                    string[] s1 = sr.ReadLine().Split(' '), s2 = sr.ReadLine().Split(' ');
                    templates.Add(info.Name.Replace(info.Extension, ""), new Template(path, new(int.Parse(s1[0]), int.Parse(s1[1])), new(int.Parse(s2[0]), int.Parse(s2[1]))));
                }
            }
        }

        public static Image? Render(string key, string text) {
            if (!templates.ContainsKey(key)) return null;
            Template template = templates[key];
            Image image = Image.FromFile(template.ImagePath);
            Graphics g = Graphics.FromImage(image);
            Font font = new("微软雅黑", 80);
            SizeF size = g.MeasureString(text, font);
            Point middle = template.GetMiddle();
            g.DrawString(text, font, Brushes.Red, middle.X - size.Width / 2, middle.Y - size.Height / 2);
            return image;
        }


        class Template {
            public readonly string ImagePath;
            public readonly Point x1, x2;
            public Template(string ImagePath, Point x1, Point x2) {
                this.ImagePath = ImagePath;
                this.x1 = x1;
                this.x2 = x2;
            }

            public Point GetMiddle() => new((this.x1.X + this.x2.X) / 2, (this.x1.Y + this.x2.Y) / 2);
        }
    }
}
