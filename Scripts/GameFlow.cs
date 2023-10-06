using Godot;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace Gelcast.Example.Card
{
	public enum Player { Self, Opponent, Field }
	public partial class GameFlow : Node2D
	{
		private static CardGame game;
		private System.Random rng = new System.Random(5);

		[Export] public Card[] headCards;
		[Export] public Card[] handleCards;
		[Export] public Card[] upgradeCards;
		[Export] public Exam[] examCards;
		[Export] public PackedScene cardTemplate;
		[Export] public PackedScene toolTemplate;
		[Export] public PackedScene upgradeTemplate;

		public static PackedScene CardTemplate;
		public static PackedScene ToolTemplate;
		public static PackedScene UpgradeTemplate;

		private Deck partDeck;
		private Deck upgradeDeck;
		private Deck partDeckOpponent;
		private Deck upgradeDeckOpponent;
		private Deck examDeck;

		private List<CardInstance> hand = new List<CardInstance>();
		private List<CardInstance> handOpponent = new List<CardInstance>();
		private List<CardInstance> handField = new List<CardInstance>();

		public void Init(CardGame game)
		{
			GameFlow.game = game;
			GameFlow.CardTemplate = cardTemplate;
			GameFlow.ToolTemplate = toolTemplate;
			GameFlow.UpgradeTemplate = upgradeTemplate;

			partDeck = new Deck(game.playerHUD);
			upgradeDeck = new Deck(game.playerHUD);
			partDeckOpponent = new Deck(game.opponentHUD);
			upgradeDeckOpponent = new Deck(game.opponentHUD);
			examDeck = new Deck(game.fieldHUD);

			BuildDecks();
			DrawCards(4, 4, 4, 4, 2);

			Item[] testHand1 = new Item[] { hand[0].item, hand[1].item, hand[4].item };
			Item[] testHand2 = new Item[] { handOpponent[0].item, handOpponent[1].item, handOpponent[4].item };
			Item[] testHand3 = new Item[] { hand[2].item, hand[3].item, hand[5].item, hand[6].item, hand[7].item };
			Item[] testHand4 = new Item[] { handOpponent[2].item, handOpponent[3].item, handOpponent[5].item, handOpponent[6].item, handOpponent[7].item };

			test test = new test();
			test.Test(this, testHand1, testHand2);
			test.Test(this, testHand3, testHand4, 32);
		}

		void BuildDecks()
		{
			partDeck.AddNewCards(cardTemplate, headCards, rng);
			partDeck.AddNewCards(cardTemplate, handleCards, rng, true);
			upgradeDeck.AddNewCards(cardTemplate, upgradeCards, rng, true);

			partDeckOpponent.AddNewCards(cardTemplate, headCards, rng);
			partDeckOpponent.AddNewCards(cardTemplate, handleCards, rng, true);
			upgradeDeckOpponent.AddNewCards(cardTemplate, upgradeCards, rng, true);

			examDeck.AddNewCards(cardTemplate, examCards, rng, true);
		}

		public void DrawCards(int parts, int upgrades, int partsOpponent, int upgradesOpponent, int exams)
		{
			partDeck.Draw(ref hand, parts);
			upgradeDeck.Draw(ref hand, upgrades);
			partDeckOpponent.Draw(ref handOpponent, partsOpponent);
			upgradeDeckOpponent.Draw(ref handOpponent, upgradesOpponent);
			examDeck.Draw(ref handField, exams);

			game.playerHUD.SetHand(hand.ToArray());
			game.opponentHUD.SetHand(handOpponent.ToArray());
			game.fieldHUD.SetHand(handField.ToArray());
		}

		public void Discard(ToolOfCards tool, Player player)
		{
			//TODO: Tool needs to be torn apart
		}

		public void Discard(CardInstance card, Player player)
		{
			switch (player)
			{
				case Player.Self:
					if (card.type == CardClass.Part)
					{
						hand.Remove(card);
						partDeck.AddToDiscard(card);
					}
					else if (card.type == CardClass.Upgrade)
					{
						hand.Remove(card);
						upgradeDeck.AddToDiscard(card);
					}
					else if (card.type == CardClass.Field)
						GD.PrintErr("Field cards are not player cards!");
					break;

				case Player.Opponent:
					if (card.type == CardClass.Part)
					{
						handOpponent.Remove(card);
						partDeckOpponent.AddToDiscard(card);
					}
					else if (card.type == CardClass.Upgrade)
					{
						handOpponent.Remove(card);
						upgradeDeckOpponent.AddToDiscard(card);
					}
					else if (card.type == CardClass.Field)
						GD.PrintErr("Field cards are not opponent cards!");
					break;

				case Player.Field:
					if (card.type == CardClass.Field)
					{
						handField.Remove(card);
						examDeck.AddToDiscard(card);
					}
					else
						GD.PrintErr("This deck can only accept field cards");
					break;

			}
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