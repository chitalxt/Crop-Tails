using Godot;
using System;

public partial class  GameDialogueManager : Node
{   
    // 单例模式
    public static GameDialogueManager Instance  { get; private set; }

    [Signal]
    public delegate void GiveCropSeedsEventHandler();

    [Signal]
    public delegate void FeedTheAnimalsEventHandler();

    public override void _Ready(){
        Instance = this;
    }

    public void actionGiveCropSeeds(){
        EmitSignal("GiveCropSeeds");
    }

    public void actionFeedTheAnimals(){
        EmitSignal("FeedTheAnimals"); 
    }

}
