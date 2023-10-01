using Godot;
using System;

namespace Gelcast.Example.Card
{
	public partial class CardDuringExam : Card
	{
		[Export] public ExamType boostTarget;
		[Export] public int durabilityArmor;
		[Export] public int powerBoost;
		[Export] public int styleBoost;
		[Export] public int durabilityTheft;
	}
}