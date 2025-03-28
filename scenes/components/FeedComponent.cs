using Godot;
using System;

public partial class FeedComponent : Area2D
{
    [Signal]
    public delegate void FoodReceivedEventHandler();


    public void onAreaEntered(Area2D area){
        EmitSignal("FoodReceived"); 
    }

}
