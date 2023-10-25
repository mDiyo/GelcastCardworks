using Godot;
using System.Collections;
using System.Collections.Generic;

namespace Gelcast
{
	/* Manages any game menus or screens that Gelcast keeps track of.
     * Singleton pattern
     */
	public partial class MenuManager : Node
	{
		public static MenuManager instance;
		public Menu[] menus;
		private static Menu openMenu;

		void Awake()
		{
			instance = this;
		}

		public void Start()
		{
			foreach (Menu m in menus)
				m.CloseMenu();
		}

		public static void OpenMenu(string menu)
		{
			bool found = false;
			foreach (Menu m in instance.menus)
			{
				// Debug.Log("Menu: " + m.menuType);
				if (menu.Equals(m.menuType))
				{
					if (openMenu != null)
						openMenu.CloseMenu();
					m.OpenMenu();
					openMenu = m;
					found = true;
				}
			}
			if (!found)
				GD.PrintErr("No menu of type " + menu + " could be found.");
		}

		public static void CloseMenu()
		{
			if (openMenu != null)
			{
				openMenu.CloseMenu();
				openMenu = null;
			}
		}
	}
}
