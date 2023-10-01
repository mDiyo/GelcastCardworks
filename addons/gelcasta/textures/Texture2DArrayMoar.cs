#if TOOLS
using Godot;
using System;

namespace Gelcast
{
    public partial class Texture2DArrayMoar : EditorInspectorPlugin
    {
        [Export] public Texture2D tex;
        [Export] public Vector2I textureSize;

        public override bool _CanHandle(GodotObject @object)
        {
            GD.Print("Print a lot?");
            // We support all objects in this example.
            return true;
            //return @object is Texture2DArray;
        }

        public override bool _ParseProperty(GodotObject @object, Variant.Type type,
            string name, PropertyHint hintType, string hintString,
            PropertyUsageFlags usageFlags, bool wide)
        {
            // We handle properties of type integer.
            if (type == Variant.Type.Int)
            {
                // Create an instance of the custom property editor and register
                // it to a specific property path.
                AddPropertyEditor(name, new RandomIntEditor());
                // Inform the editor to remove the default property editor for
                // this property type.
                return true;
            }

            return false;
        }
    }
}
#endif