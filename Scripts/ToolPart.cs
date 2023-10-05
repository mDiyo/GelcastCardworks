using Godot;
using System;

namespace Gelcast.Example.Card
{
    public enum AttachPoint { High, Low, Slot, None }
    public partial class ToolPart : Sprite2D
    {
        [Export] public Node2D singleSlotAttach;
        [Export] public Node2D[] multiSlotAttach;
        [Export] public Node2D highAttachPoint; // Standard heads
        [Export] public Node2D lowAttachPoint;  // Long heads, like blades
        [Export] public AttachPoint attachTo;

        // Nodes are for nicely viewing parts in the editor. 
        // We don't actually want the extra nodes, just their position
        private Vector2[] attachPoints;
        private Vector2 singleSlot;
        private Vector2[] multiSlot;

        public override void _Ready()
        {
            base._Ready();

            if (highAttachPoint != null && lowAttachPoint != null)
                attachPoints = new Vector2[] { highAttachPoint.Position, lowAttachPoint.Position };
            singleSlot = singleSlotAttach.Position;
            multiSlot = new Vector2[multiSlotAttach.Length];
            for (int i = 0; i < multiSlotAttach.Length; i++)
                multiSlot[i] = multiSlotAttach[i].Position;

            if (highAttachPoint != null)
                highAttachPoint.QueueFree();
            if (lowAttachPoint != null)
                lowAttachPoint.QueueFree();
            singleSlotAttach.QueueFree();
            for (int i = 0; i < multiSlotAttach.Length; i++)
                multiSlotAttach[i].QueueFree();
        }

        public Vector2 GetAttachPoint(AttachPoint attachPoint)
        {
            switch (attachPoint)
            {
                case AttachPoint.High: return attachPoints[0];
                case AttachPoint.Low: return attachPoints[1];
                case AttachPoint.Slot: return singleSlot;
            }
            throw new NotImplementedException();
        }

        public Vector2[] GetAttachSlots(int numberOfCards)
        {
            if (numberOfCards == 1)
                return new Vector2[] { singleSlot };
            return multiSlot;
        }
    }
}
