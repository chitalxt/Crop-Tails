using Godot;
using System;
using DialogueManagerRuntime;

public partial class Guide : Node2D
{
    private InteractableComponent interactableComponent;
    private Control interactableLabelComponent;

    PackedScene balloonScene = ResourceLoader.Load<PackedScene>("res://dialogue/GameDialogueBalloon.tscn");

    bool inRange;

    public override void _Ready(){
        interactableComponent = GetNode<InteractableComponent>("InteractableComponent");
        interactableLabelComponent = GetNode<Control>("InteractableLabelComponent");

        interactableComponent.Connect("InteractableActivated", new Callable(this, nameof(onInteractableActivated)));
        interactableComponent.Connect("InteractableDeActivated", new Callable(this, nameof(onInteractableDeActivated)));

        interactableLabelComponent.Hide();

        GameDialogueManager.Instance.Connect("GiveCropSeeds", new Callable(this, nameof(onGiveCropSeeds)));
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if(inRange){
            if (@event.IsActionPressed("show_dialogue")){
                BaseGameDialogueBalloon balloon = balloonScene.Instantiate() as BaseGameDialogueBalloon;
                
                GetTree().Root.AddChild(balloon);    
                // 开始对话，加载对话脚本
                balloon.Start(GD.Load<Resource>("res://dialogue/conversations/guide.dialogue"), "start");
            }
        }
    }

     private void onInteractableActivated(){
        interactableLabelComponent.Show();
        inRange = true;
    }
    private void onInteractableDeActivated(){
        interactableLabelComponent.Hide();
        inRange = false;
    }

    public void onGiveCropSeeds(){
        // 对话结束，选择收取种子后，启用工具 
        ToolManager.EnableToolButton(DataTypes.Tools.TillGround);
        ToolManager.EnableToolButton(DataTypes.Tools.WaterCrops);
        ToolManager.EnableToolButton(DataTypes.Tools.PlantCorn);
        ToolManager.EnableToolButton(DataTypes.Tools.Plantomato);
    }

}
