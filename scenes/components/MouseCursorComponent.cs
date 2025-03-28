using Godot;
using System;

public partial class MouseCursorComponent : Node
{
    [Export]
    Texture2D CursorComponentTexture;


    public override void _Ready(){
        Input.SetCustomMouseCursor(CursorComponentTexture, Input.CursorShape.Arrow); 
    }

}
