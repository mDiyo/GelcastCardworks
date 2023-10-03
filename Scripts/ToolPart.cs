using Godot;
using System;

namespace Gelcast.Example.Card
{
    public partial class ToolPart : Node2D
    {
        [Export] public Node2D singleSlotAttach;
        [Export] public Node2D[] multiSlotAttach;
        [Export] public Node2D highAttachPoint; // Standard heads
        [Export] public Node2D lowAttachPoint;  // Long heads, like blades

        //Nodes are for nicely viewing parts in the editor. We don't actually want the extra nodes
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
    }
}
