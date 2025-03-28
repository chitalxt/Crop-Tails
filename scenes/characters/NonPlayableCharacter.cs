using Godot;
using System;

public partial class NonPlayableCharacter : CharacterBody2D
{
    [Export]
    public int minWalkCircle = 2;
    [Export]
    public int maxWalkCircle = 6;

    [Export]
    public int walkCircles;
    [Export]
    public int currentWalkCircle;

}
