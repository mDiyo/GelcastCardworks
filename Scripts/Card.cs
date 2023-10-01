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
		[Export] public string identifier;
	}
}