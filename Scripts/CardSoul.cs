using Godot;
using System;

namespace Gelcast.Example.Card
{
	public partial class CardSoul : Card
	{
		[Export] public string soulType;
		[Export] public int durabilityBoost;
		[Export] public int powerBoost;
		[Export] public int styleBoost;
		[Export] public int drawBoost;
		[Export] public int drawBase;
	}
}