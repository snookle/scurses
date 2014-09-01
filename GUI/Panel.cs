using System;

namespace scurses
{
	public class Panel : Container
	{
		public int Width;
		public int Height;
		public string Title;

		public Panel(Point p, int width, int height, String title)
		{
			location = p;
			Width = width;
			Height = height;
			Title = title;
		}

		public override void drawGraphics(Screen screen)
		{
			screen.begin();
			screen.drawBox(location, Width, Height);
			screen.drawString(((location.X + Width / 2) - Title.Length / 2) + 1, location.Y, Title);
			base.drawGraphics(screen);
			screen.end();
		}

	}
}
