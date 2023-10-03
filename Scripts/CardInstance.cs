using Godot;
using System;

namespace Gelcast.Example.Card
{
	public partial class CardInstance : ItemInstance
	{
		public CardClass type;
		private Exam examData;
		public string description;

		public void Init(Card data)
		{
			base.Init(data);
			item = data;
			type = data.type;
			Texture = data.GetInventorySprite();
			Name = data.identifier + ".name";
			description = data.identifier + ".description";
		}

		public void Init(Exam data)
		{
			examData = data;
			type = CardClass.Field;
			Texture = data.texture;
			Name = data.identifier + ".name";
			description = data.identifier + ".description";
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
}