using Godot;
using System.Collections;
using System.Collections.Generic;

namespace Gelcast
{
	/* Base menu class. Holds basic behavior for selection boxes
     * Inventory base class is found in InventoryMenu
     */
	public abstract partial class Menu : Control
	{
		[Export] public MenuManager menuManager;
		[Export] public string menuType;
		[Export] public Control selectionBox;

		//Controls
		protected double horizontal;
		protected double vertical;
		protected bool up;
		protected bool down;
		protected bool left;
		protected bool right;
		protected double inputDelay;
		protected double shortInputDelay = 7f * (1f / 60f);
		protected double longInputDelay = 15f * (1f / 60f);

		//Selection management
		[Export] public bool isList; //If true, the items are presented in a vertical list instead of a matrix
		protected int selectedItem;
		[Export] public Vector2I itemsSize = new Vector2I(6, 5);
		[Export] public Vector2 itemSpacing = new Vector2(36, 36);
		[Export] public Vector2 anchorPoint = new Vector2(-90, 72); //Upper left usually. The menu knows best
		protected bool reverseAnchorVertical = false;

		public override void _Ready()
		{
			base._Ready();
			if (menuType == null || menuType.Length == 0)
				menuType = Name;

			MoveSelection(selectedItem);
		}

		public virtual void OpenMenu()
		{
			this.ProcessMode = ProcessModeEnum.Inherit;
			this.Visible = true;
		}

		public virtual void CloseMenu()
		{
			this.ProcessMode = ProcessModeEnum.Disabled;
			this.Visible = false;
		}

		public override void _Process(double delta)
		{
			base._Process(delta);
			if (isList)
				ListControls(delta);
			else
				BoxControls(delta);
		}

		public virtual void ListControls(double delta)
		{
			vertical = Input.GetAxis("up", "down");

			if (reverseAnchorVertical)
				vertical = -vertical;

			if (inputDelay > 0)
			{
				if (up && vertical >= 0)
					up = false;
				if (down && vertical <= 0)
					down = false;

				if (!up && !down)
					inputDelay = 0;
				else
				{
					inputDelay -= delta;
				}
			}
			else if (inputDelay <= 0)
			{
				up = vertical < 0;
				down = vertical > 0;

				int offset = 0;
				if (up)
					offset += 1;
				if (down)
					offset -= 1;

				if (offset != 0)
					MoveSelection(offset);
			}

			//Lists may have selection arrows. This is for you.
			if (vertical == 0)
			{
				horizontal = Input.GetAxis("left", "right");

				if (inputDelay > 0)
				{
					if (right && horizontal >= 0)
						right = false;
					if (left && horizontal <= 0)
						left = false;

					if (!left && !right)
						inputDelay = 0;
				}
				else if (inputDelay <= 0)
				{
					left = horizontal < 0;
					right = horizontal > 0;

					int offset = 0;
					if (left)
						offset += 1;
					if (right)
						offset -= 1;

					if (offset != 0)
						CycleList(offset > 0);
				}
			}
		}

		public virtual void BoxControls(double delta)
		{
			if (selectionBox != null)
			{
				Vector2 moveDirection = Input.GetVector("left", "right", "up", "down");
				float spinDirection = Input.GetAxis("clockwise", "anticlockwise");

				horizontal = moveDirection.X;
				vertical = moveDirection.Y;

				if (reverseAnchorVertical)
					vertical = -vertical;

				if (inputDelay > 0)
				{
					if (up && vertical >= 0)
						up = false;
					if (down && vertical <= 0)
						down = false;
					if (left && horizontal >= 0)
						left = false;
					if (right && horizontal <= 0)
						right = false;

					if (!up && !down && !left && !right)
						inputDelay = 0;
					else
						inputDelay -= delta;
				}
				else if (inputDelay <= 0)
				{
					left = horizontal < 0;
					right = horizontal > 0;
					up = vertical < 0;
					down = vertical > 0;

					if (!isList)
					{
						int offset = 0;
						if (left)
							offset -= 1;
						if (right)
							offset += 1;
						if (up)
							offset += itemsSize.X;
						if (down)
							offset -= itemsSize.X;

						if (offset != 0)
							MoveSelection(offset);
					}
					else
					{
						int offset = 0;
						if (up)
							offset += 1;
						if (down)
							offset -= 1;

						if (offset != 0)
							MoveSelection(offset);

						offset = 0;
						if (left)
							offset += 1;
						if (right)
							offset -= 1;

						if (offset != 0)
							CycleList(offset > 0);
					}
				}
			}
		}

		protected virtual void MoveSelection(int amount)
		{
			if (!isList) //grid
			{
				if (selectedItem % itemsSize.X == itemsSize.X - 1 && amount % itemsSize.X == 1
				) //Shifting for left/right edges
					selectedItem -= itemsSize.X;
				else if (selectedItem % itemsSize.X == 0 && amount % itemsSize.X == -1)
					selectedItem += itemsSize.X;

				if (selectedItem == itemsSize.X * (itemsSize.Y - 1) && amount == itemsSize.X - 1) //Corner cases
					selectedItem = itemsSize.X - 1;
				else if (selectedItem == itemsSize.X * itemsSize.Y - 1 && amount == itemsSize.X + 1)
					selectedItem = 0;
				else if (selectedItem == 0 && amount == -(itemsSize.X + 1))
					selectedItem = itemsSize.X * itemsSize.Y;
				else if (selectedItem == itemsSize.X - 1 && amount == -(itemsSize.X - 1))
					selectedItem = itemsSize.X * (itemsSize.Y - 1);
				else
					selectedItem += amount;

				if (selectedItem >= itemsSize.X * itemsSize.Y)
					selectedItem -= itemsSize.X * itemsSize.Y;
				if (selectedItem < 0)
					selectedItem += itemsSize.X * itemsSize.Y;
			}
			else //list
			{
				selectedItem += amount;
				if (selectedItem >= itemsSize.X)
					selectedItem -= itemsSize.X;
				if (selectedItem < 0)
					selectedItem += itemsSize.X;

			}

			AdjustSelectPosition();
			UpdateMenu();

			inputDelay = shortInputDelay;
		}

		protected virtual void CycleList(bool forward)
		{
			inputDelay = shortInputDelay;
		}

		protected virtual void UpdateMenu()
		{

		}

		protected virtual void AdjustSelectPosition()
		{
			if (!isList)
			{
				//Move the selected box
				float posX = anchorPoint.X + (selectedItem % itemsSize.X * itemSpacing.X);
				float posY = anchorPoint.Y - (selectedItem / itemsSize.X * itemSpacing.Y);
				if (reverseAnchorVertical)
					posY = anchorPoint.Y + (selectedItem / itemsSize.X * itemSpacing.Y);

				selectionBox.Position = new Vector2(posX, posY);
			}
			else
			{
				selectionBox.Position = new Vector2(anchorPoint.X, anchorPoint.Y - selectedItem * itemSpacing.X);
			}
		}
	}
}
