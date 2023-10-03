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
		GD.Print("All hail " + item);
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

		foreach (Item item in items)
		{
			Node model = null;
			GD.Print("Item: " + item);
			if (item.model != null)
				model = item.model.Instantiate();
			else
				model = GameFlow.UpgradePrefab.Instantiate();
			AddChild(model);
		}
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
