using Godot;
using System;

namespace Gelcast.Example.Card
{
	public partial class CardGame : Node
	{
		[Export] public CardHUD playerHUD;
		[Export] public CardHUD opponentHUD;
		[Export] public FieldHUD fieldHUD;
		[Export] public GameFlow gameFlow;
		[Export] public int morale = 10;
		[Export] public int moraleOpponent = 10;

		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			gameFlow.Init(this);
			playerHUD.Init(this);
			opponentHUD.Init(this);
			fieldHUD.Init(this);
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
		}
	}
}