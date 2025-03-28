using Godot;


public partial class GameMenuScreenBackground : Node2D
{
    public override void _Ready(){
        CallDeferred(nameof(SetSceneProcess));
    }

    public void SetSceneProcess(){
        ProcessMode = ProcessModeEnum.Disabled;
    }
        
}
