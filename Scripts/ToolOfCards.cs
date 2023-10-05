using Gelcast;
using Gelcast.Example.Card;
using Godot;
using System;
using System.Collections.Generic;
public partial class ToolOfCards : ItemInstance
{
	List<Item> items = new List<Item>();
	List<Node> models = new List<Node>();

	public void Modify(Item item, bool add, bool build = true)
	{
		//GD.Print("All hail " + item.identifier);
		if (add)
			items.Add(item);
		else
			items.Remove(item);

		if (build)
			Build();
	}

	public void Build()
	{
		foreach (Node2D model in models)
			model.QueueFree();
		models.Clear();

		ToolPart head = null;
		ToolPart handle = null;
		List<Sprite2D> upgrades = new List<Sprite2D>();
		foreach (Item item in items)
		{
			Sprite2D model = null;
			if (item.model != null)
				model = item.model.Instantiate<Sprite2D>();
			else
				model = GameFlow.UpgradePrefab.Instantiate<Sprite2D>();
			model.Texture = item.GetWorldSprite();
			AddChild(model);

			if (item.identifier.Contains("head"))
				head = (ToolPart)model;
			else if (item.identifier.Contains("handle"))
				handle = (ToolPart)model;
			else
				upgrades.Add(model);
		}

		GD.Print("Head: " + head + ", Handle: " + handle);

		if (handle != null && head != null)
		{
			this.RemoveChild(head);
			handle.AddChild(head);
			head.Position = handle.GetAttachPoint(head.attachTo);
		}

		Vector2[] upgradePositions = null;
		if (head != null)
		{
			upgradePositions = head.GetAttachSlots(upgrades.Count);
			for (int i = 0; i < upgrades.Count; i++)
			{
				if (i >= upgradePositions.Length)
					break;

				this.RemoveChild(upgrades[i]);
				head.AddChild(upgrades[i]);
				upgrades[i].Position = upgradePositions[i];
			}
		}
		if (handle != null)
		{
			int headConsumed = upgradePositions == null ? 0 : upgradePositions.Length;
			upgradePositions = handle.GetAttachSlots(upgrades.Count - headConsumed);
			for (int i = headConsumed; i < upgrades.Count; i++)
			{
				if (i >= upgradePositions.Length)
					break;

				this.RemoveChild(upgrades[i]);
				handle.AddChild(upgrades[i]);
				upgrades[i].Position = upgradePositions[i];
			}
		}


		Texture = null;
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
