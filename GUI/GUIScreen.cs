using System;

namespace scurses
{
	public class GUIScreen : Container
	{
		private Screen s;

		public GUIScreen()
		{
			s = new Screen();
		}

		public void drawGraphics()
		{
			s.begin();
			base.drawGraphics(s);
			s.end();
		}
	}
}

