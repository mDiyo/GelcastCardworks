using Godot;
using System;

namespace Gelcast
{
    public partial class ItemInstance : Sprite2D
    {
        [Export] public Item item;

        public virtual void Init(Item newItem)
        {
            Texture = newItem.GetInventorySprite();
        }

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
        }

        // Called every frame. 'delta' is the elapsed time since the previous frame.
        public override void _Process(double delta)
        {
        }

        public virtual string GetName()
        {
            return Localizer.GetString(item.identifier + ".name");
        }

        public virtual string GetDescription()
        {
            return Localizer.GetString(item.identifier + ".description");
        }

        public virtual Texture2D GetInventorySprite()
        {
            Texture2D sprite = item.GetInventorySprite();
            if (sprite != null)
                return item.inventorySprite;
            return Texture;
        }

        public virtual int GetValue() { return (int)GetMeta("Value"); }
        public virtual int GetQuantity() { return (int)GetMeta("Quantity"); }
        public virtual int GetQuantityMax() { return (int)GetMeta("Quantity-Max"); }
        public virtual void SetQuantity(int amount) { SetMeta("Quantity", amount); }

        public virtual int AddQuantity(int amount)
        {
            int quanta = GetQuantity();
            int max = GetQuantityMax();
            if (quanta + amount > max)
            {
                SetQuantity(max);
                return max - quanta;
            }
            else
            {
                SetQuantity(quanta + amount);
                return 0;
            }

        }
    }
}