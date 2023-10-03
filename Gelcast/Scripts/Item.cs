using Godot;
using System;

namespace Gelcast
{
	public partial class Item : Resource
	{
		[Export] public string identifier;
		[Export] public Texture2D inventorySprite;
		[Export] public Texture2D worldSprite;
		[Export] public Attributes attribues;

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
			return inventorySprite;
		}

		public virtual Texture2D GetWorldSprite()
		{
			return worldSprite;
		}

		public virtual Vector2 GetWorldPosition()
		{
			return Vector2.Zero;
		}

		public virtual void SetInventorySprite(Texture2D sprite)
		{
			inventorySprite = sprite;
		}
	}
}