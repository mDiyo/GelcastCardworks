using Godot;
using System;
using System.Text;

[Tool]
public partial class SpritesheetSlicer : Sprite2D
{
    [Export] public bool sliceSheet = false;
    public Texture2D sliceMe;
    [Export] public Vector2I regionSize = new Vector2I(32, 32);
    public override void _Process(double delta)
    {
        if (Engine.IsEditorHint())
        {
            if (sliceSheet)
            {
                sliceSheet = false;

                GD.Print("Creating AtlasTextures for " + Texture);
                sliceMe = Texture;
                int count = 0;
                for (int y = 0; y < sliceMe.GetHeight(); y += regionSize.Y)
                {
                    for (int x = 0; x < sliceMe.GetWidth(); x += regionSize.X)
                    {
                        AtlasTexture at = new AtlasTexture();
                        at.Atlas = sliceMe;
                        at.Region = new Rect2(x, y, regionSize.X, regionSize.Y);
                        string path = sliceMe.ResourcePath;
                        string substring = path.Substring(path.LastIndexOf('/'));
                        path = path.Replace(substring, "");
                        string namestring = substring.Substring(substring.LastIndexOf('.'));
                        namestring = substring.Replace(namestring, "");
                        GD.Print("Slice : " + path + "/Slices" + namestring + "_" + count + ".tres");
                        ResourceSaver.Save(at, path + "/Slices/" + namestring + "_" + count + ".tres");

                        count++;
                    }
                }
            }
        }
    }
}
