using Godot;
using System;

namespace Gelcast.Example.Card
{
	public enum CardClass { Part, Upgrade, Field }
	public partial class Card : Resource
	{
		[Export] public CardClass type;
		[Export] public int durability;
		[Export] public int power;
		[Export] public int style;
		[Export] public int slots;
		[Export] public Texture2D texture;
		[Export] public Texture2D itemSprite;
		[Export] public ItemOverride[] itemOverrides;
		[Export] public string identifier;

		public Texture2D GetItemSprite()
		{
			return itemSprite;
		}
	}

	public partial class ItemOverride : Resource
	{
		[Export] public string materialOverride;
		[Export] public string classOverride;
		[Export] public Texture2D itemSprite;
	}
}