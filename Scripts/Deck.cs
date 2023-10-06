using Gelcast.Example.Card;
using Godot;
using System;
using System.Collections.Generic;

namespace Gelcast.Example.Card
{
	public class Deck
	{
		public Control parent;
		public List<CardInstance> deck = new List<CardInstance>();
		public List<CardInstance> discard = new List<CardInstance>();

		public Deck(Control node)
		{
			parent = node;
		}

		public void AddNewCards(PackedScene cardTemplate, Card[] cards, System.Random rng, bool shuffle = false)
		{
			for (int i = 0; i < cards.Length; i++)
				deck.Add(BuildNewCard(cardTemplate, cards[i]));
			if (shuffle)
				deck.Shuffle(rng);
		}

		public void AddNewCards(PackedScene cardTemplate, Exam[] exams, System.Random rng, bool shuffle = false)
		{
			for (int i = 0; i < exams.Length; i++)
				deck.Add(BuildNewCard(cardTemplate, exams[i]));
			if (shuffle)
				deck.Shuffle(rng);
		}

		public CardInstance BuildNewCard(PackedScene cardTemplate, Card data)
		{
			CardInstance card = cardTemplate.Instantiate() as CardInstance;
			card.Position = new Godot.Vector2(-800 + 4 * deck.Count, -100);
			card.Init(data);
			parent.AddChild(card);
			return card;
		}

		public CardInstance BuildNewCard(PackedScene cardTemplate, Exam data)
		{
			CardInstance card = cardTemplate.Instantiate() as CardInstance;
			card.Position = new Godot.Vector2(-100, -100);
			card.Init(data);
			return card;
		}

		public void Draw(ref List<CardInstance> hand, int amount = 1, bool shuffle = true)
		{
			for (int i = 0; i < amount; i++)
			{
				if (deck.Count == 0)
				{
					deck = discard;
					discard = new List<CardInstance>();
					if (shuffle)
						deck.Shuffle();
				}
				hand.Add(deck.Draw());
			}
		}

		public void Discard(int amount = 1, bool shuffle = true)
		{
			Draw(ref deck, amount, shuffle);
		}

		public void AddToDiscard(CardInstance card)
		{
			discard.Add(card);
		}
	}
}