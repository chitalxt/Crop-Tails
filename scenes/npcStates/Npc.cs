using Godot;
using System;

public partial class Npc : NonPlayableCharacter
{
    public bool isMoving = false;

    public override void _Ready(){

        walkCircles = GD.RandRange(minWalkCircle, maxWalkCircle);
    }
}
