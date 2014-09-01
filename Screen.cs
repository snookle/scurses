using System;
using System.Collections.Generic;
using System.Text;

namespace scurses
{

	public enum Orientation
	{
		Horizontal,
		Vertical
	}

	public class Screen
	{

		private struct ConsoleState
		{
			int oldCursorLeft;
			int oldCursorTop;
			ConsoleColor oldBackgroundColour;
			ConsoleColor oldForegroundColour;

			public void Save()
			{
				oldCursorLeft = Console.CursorLeft;
				oldCursorTop = Console.CursorTop;
				oldBackgroundColour = Console.BackgroundColor;
				oldForegroundColour = Console.ForegroundColor;
			}

			public void Restore()
			{
				Console.CursorLeft = oldCursorLeft;
				Console.CursorTop = oldCursorTop;
				Console.BackgroundColor = oldBackgroundColour;
				Console.ForegroundColor = oldForegroundColour;
			}
		};

		private Stack<ConsoleState> stateStack = new Stack<ConsoleState>();
		private Encoding encoding = Encoding.GetEncoding("IBM437");
		private char[] boxChars;

		public Screen()
		{
			boxChars = encoding.GetChars(new byte[6]{ 179, 196, 218, 191, 192, 217 });
		}


		public void begin()
		{
			ConsoleState state;
			state.Save();
			stateStack.Push(state);
		}

		public void end()
		{
			stateStack.Pop().Restore();
		}

		private void setColour(ConsoleColor foreground, ConsoleColor background)
		{
			Console.ForegroundColor = foreground;
			Console.BackgroundColor = background;
		}

		public void drawChar(int x, int y, char ch)
		{
			moveCursor(x, y);
			Console.Write(ch);
		}

		public void drawChar(Point pos, char ch)
		{
			drawChar(pos.X, pos.Y, ch);
		}

		public void drawString(int x, int y, String str)
		{
			moveCursor(x, y);
			Console.Write(str);
		}

		public void drawString(Point pos, String str)
		{
			drawString(pos.X, pos.Y, str);
		}

		public void drawLine(Point pos, int length, Orientation orientation)
		{
			drawLine(pos.X, pos.Y, length, orientation);
		}

		public void drawLine(int x, int y, int length, Orientation orientation)
		{
			if (orientation == Orientation.Vertical) {
				int endPos = y + length;
				if (endPos >= Console.BufferHeight) {
					endPos = (y + length) - Console.BufferHeight;
				}
				for (; y < endPos; y++) {
					drawString(x, y, new string(boxChars[0], 1));
				}
			} else if (orientation == Orientation.Horizontal) {
				//can do a single draw call
				String line = "".PadLeft(length, boxChars[1]);
				drawString(x, y, line);
			}
		}

		public void drawBox(Point pos, int width, int height)
		{
			drawBox(pos.X, pos.Y, width, height);
		}

		public void drawBox(int x, int y, int width, int height)
		{
			//top
			drawLine(x + 1, y, width, Orientation.Horizontal);
			//bottom
			drawLine(x + 1, y + height + 1, width, Orientation.Horizontal);
			//left
			drawLine(x, y + 1, height, Orientation.Vertical);
			//right
			drawLine(x + 1 + width, y + 1, height, Orientation.Vertical);

			//top left corner
			drawChar(x, y, boxChars[2]);
			//top right corner
			drawChar(x + width + 1, y, boxChars[3]);
			//bottom left corner
			drawChar(x, y + height + 1, boxChars[4]);
			//bottom right corner
			drawChar(x + width + 1, y + height + 1, boxChars[5]);

		}

		public void moveCursor(Point pos)
		{
			moveCursor(pos.X, pos.Y);
		}

		public void moveCursor(int x, int y)
		{
			Console.SetCursorPosition(x, y);
		}
	}
}

