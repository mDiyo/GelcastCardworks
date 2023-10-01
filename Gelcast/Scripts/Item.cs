using Godot;
using System;

namespace Gelcast
{
	public partial class Item : Sprite2D
	{
		[Export] public string identifier;
		[Export] public Texture2D inventorySprite;
		[Export] public Attributes attribues;

		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
		}

		public virtual string GetName()
		{
			return Localizer.GetString(identifier + ".name");
		}

		public virtual string GetDescription()
		{
			return Localizer.GetString(identifier + ".description");
		}

		public virtual bool IsSameItem(Item other)
		{
			if (other == this)
				return true;
			return false;
		}

		public virtual bool IsSimilarItem(Item other)
		{
			if (other == null)
				return false;
			if (this.identifier.Equals(other.identifier))
				return true;
			return false;
		}

		public virtual Texture2D GetInventorySprite()
		{
			if (inventorySprite == null)
				return Texture;
			return inventorySprite;
		}

		public virtual void SetInventorySprite(Texture2D sprite)
		{
			inventorySprite = sprite;
		}

		public virtual int GetValue() { return (int)GetMeta("Value"); }
		public virtual int GetQuantity() { return (int)GetMeta("Quantity"); }
		public virtual int GetQuantityMax() { return (int)GetMeta("Quantity-Max"); }
		public virtual void SetQuantity(int amount) { SetMeta("Quantity", amount); }

		public virtual int AddQuantity(int amount)
		{
			int quanta = GetQuantity();
			int max = GetQuantityMax();
			if (quanta + amount > max)
			{
				SetQuantity(max);
				return max - quanta;
			}
			else
			{
				SetQuantity(quanta + amount);
				return 0;
			}

		}
	}
}