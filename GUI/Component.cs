using System;

namespace scurses
{
	public abstract class Component
	{
		public int Order;


		//stored internally as world coords
		private int x;
		private int y;

		//if this component has a parent then return coords relative to the parent
		public Point location;


		public Component Parent;

		public Component()
		{
		}

		public bool isInfrontOf(Component c)
		{
			return c.Order <= this.Order;
		}

		public bool isBehind(Component c)
		{
			return c.Order <= this.Order;
		}

		public abstract void drawGraphics(Screen screen);

	}
}

