using Gelcast;
using Gelcast.Example.Card;
using Godot;
using System;
using System.Collections.Generic;
public partial class ToolOfCards : ItemInstance
{
	List<Item> items = new List<Item>();
	List<Node> models = new List<Node>();

	private int durability;
	private int power;
	private int style;
	private int slots;

	//UI related fields
	[Export] public Sprite2D durabilityIcon;
	[Export] public Sprite2D powerIcon;
	[Export] public Sprite2D styleIcon;
	[Export] public Sprite2D slotIcon;
	private const int durabilitySpacing = 11;
	private const int powerSpacing = 10;
	private const int styleSpacing = 10;
	private const int slotSpacing = 9; //Slots are vertical
	private Vector2 durabilityAnchor;
	private Vector2 powerAnchor;
	private Vector2 styleAnchor;
	private Vector2 slotAnchor;

	public override void Init(Item newItem)
	{
		//base.Init(newItem);
		durabilityAnchor = durabilityIcon.Position;
		powerAnchor = powerIcon.Position;
		styleAnchor = styleIcon.Position;
		slotAnchor = slotIcon.Position;
	}

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

		durability = power = style = slots = 0;

		ToolPart head = null;
		ToolPart handle = null;
		List<Sprite2D> upgrades = new List<Sprite2D>();
		foreach (Item item in items)
		{
			Sprite2D model = null;
			if (item.model != null)
				model = item.model.Instantiate<Sprite2D>();
			else
				model = GameFlow.UpgradeTemplate.Instantiate<Sprite2D>();
			model.Texture = item.GetWorldSprite();
			AddChild(model);

			if (item.identifier.Contains("head"))
				head = (ToolPart)model;
			else if (item.identifier.Contains("handle"))
				handle = (ToolPart)model;
			else
				upgrades.Add(model);

			if (item is Card)
			{
				Card card = (Card)item;
				this.durability += card.durability;
				this.power += card.power;
				this.style += card.style;
				this.slots += card.slots;
			}
		}

		//GD.Print("Head: " + head + ", Handle: " + handle);
		GD.Print("Dur: " + durability + ", Power: " + power + ", Style: " + style + ", Slots: " + slots);

		//Build the model

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

		//Adjust visible stats on the card
		Rect2 rect = durabilityIcon.RegionRect;
		rect.Size = new Vector2(Math.Max(durability * durabilitySpacing - 1, 0), rect.Size.Y);
		durabilityIcon.RegionRect = rect;

		rect = powerIcon.RegionRect;
		rect.Size = new Vector2(Math.Max(power * powerSpacing, 0), rect.Size.Y);
		powerIcon.RegionRect = rect;

		rect = styleIcon.RegionRect;
		rect.Size = new Vector2(Math.Max(style * styleSpacing, 0), rect.Size.Y);
		styleIcon.RegionRect = rect;

		rect = slotIcon.RegionRect;
		rect.Size = new Vector2(rect.Size.X, slots * slotSpacing);
		slotIcon.RegionRect = rect;

		//Texture = null;
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
