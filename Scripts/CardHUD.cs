using Godot;
using System;

namespace Gelcast.Example.Card
{
	public partial class CardHUD : Control
	{
		private CardGame topLevel;
		private GameFlow flow;

		public Node2D morale;
		public CardInstance[] cardHand;
		private int currentCard = 3;

		public RichTextLabel text;

		[Export] public Node playerCardAnchor;

		public void Init(CardGame game)
		{
			this.topLevel = game;
			this.flow = game.gameFlow;
		}

		public void SetHand(CardInstance[] newHand)
		{
			this.cardHand = newHand;
			ChangeTo(currentCard);
		}

		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
		}

		private const int cardWidth = 64;
		private const int cardHeight = 96;
		[Export] public int cardDepth = 10;
		[Export] public int selectOffset = -24;

		public void ChangeTo(int index)
		{
			currentCard = index;
			for (int i = 0; i < cardHand.Length; i++)
			{
				int cardOffset = 18;
				if (i < index)
					cardOffset = -cardOffset * cardHand.Length + cardOffset * 2 * i;
				else if (i == index)
					cardOffset = -cardOffset * (cardHand.Length - 1) + cardOffset * 2 * i;
				else if (i > index)
					cardOffset = -cardOffset * (cardHand.Length - 2) + cardOffset * 2 * i;

				cardHand[i].Position = new Vector2(cardOffset, 0);
				cardHand[i].VisibilityLayer = (uint)(cardDepth + i);
				GD.Print("Card " + cardHand[i].Position);
			}

			cardHand[currentCard].MoveLocalY(-24);
			cardHand[currentCard].VisibilityLayer = (uint)(cardDepth + cardHand.Length);
		}

		public void CycleCard(bool forward)
		{
			if (forward)
			{
				currentCard++;
				if (currentCard >= cardHand.Length)
					currentCard = 0;
			}
			else
			{
				currentCard--;
				if (currentCard < 0)
					currentCard = cardHand.Length - 1;
			}
			ChangeTo(currentCard);
		}
	}
}