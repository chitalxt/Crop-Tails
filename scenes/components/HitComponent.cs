using Godot;
using System;

public partial class HitComponent : Area2D
{
    [Export]
    public DataTypes.Tools CurrentTool = DataTypes.Tools.None;

    [Export]
    public int hitDamage = 1;

}

