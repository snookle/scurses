using System;
using System.Collections.Generic;

namespace scurses
{
	public abstract class Container : Component
	{
		protected List<Component> children = new List<Component>();

		public void addChild(Component child)
		{
			children.Add(child);
		}

		public void removeChild(Component child)
		{
			children.Remove(child);
		}

		public override void drawGraphics(Screen screen)
		{
			foreach (Component c in children) {
				c.drawGraphics(screen);
			}
		}
	
	}
}

