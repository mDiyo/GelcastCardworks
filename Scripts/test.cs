using Godot;
using System;

namespace Gelcast.Example.Card
{
	public partial class test
	{
		GameFlow game;
		public void Test(GameFlow game, Item[] playerCards, Item[] opponentCards, int offset = 0)
		{
			this.game = game;

			Variant w = ProjectSettings.GetSetting("display/window/size/viewport_width");
			Variant h = ProjectSettings.GetSetting("display/window/size/viewport_height");
			Variant scale = ProjectSettings.GetSetting("display/window/stretch/scale");
			GD.Print("Viewport: " + w + " | " + h + " : " + scale);

			ToolOfCards tool1 = BuildTool((int)w * 1 / 4 / (int)scale - offset, (int)h / 2 / (int)scale + 8, playerCards);
			ToolOfCards tool2 = BuildTool((int)w * 3 / 4 / (int)scale + offset, (int)h / 2 / (int)scale + 8, opponentCards);
			//GD.Print("Tool 1: " + tool1 + " " + tool1.Position);
			//GD.Print("Tool 2: " + tool2 + " " + tool2.Position);
		}

		private int count = 1;
		public ToolOfCards BuildTool(int x, int y, Item[] toolParts)
		{
			ToolOfCards tool = game.toolPrefab.Instantiate<ToolOfCards>();
			tool.Position = new Vector2(x, y);
			tool.Name = "Tool " + count;
			game.AddChild(tool);
			foreach (Item part in toolParts)
				tool.Modify(part, true, false);
			tool.Build();

			count++;
			return tool;
		}
	}
}