using Godot;
using System;

public partial class CropCursorComponent : Node
{
    /* 作物种植移除管理器 */
    [Export]
    TileMapLayer tilledSoilMapPlayer;

    private Player player;

    PackedScene cornSceneLoader = ResourceLoader.Load<PackedScene>("res://scenes/objects/plants/Corn.tscn");
    PackedScene tomatoSceneLoader = ResourceLoader.Load<PackedScene>("res://scenes/objects/plants/Tomato.tscn");


    Vector2 mousePosition;
    Vector2I cellPosition;
    int cellSourceid;
    Vector2 localCellPosition;
    float distance;

    public override void _Ready(){
        CallDeferred(nameof(PlayerSetup));
        
    }

    public async void PlayerSetup()
    {  
        await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);
         
        player = GetTree().GetFirstNodeInGroup("player") as Player; 
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if(@event.IsActionPressed("remove_dirt")){
            if(ToolManager.Instance.selectedTool == DataTypes.Tools.TillGround){
                getCellUnderMouse();
                removeCrop();
            }
        }
        else if(@event.IsActionPressed("hit")){
            if((ToolManager.Instance.selectedTool == DataTypes.Tools.PlantCorn) || (ToolManager.Instance.selectedTool == DataTypes.Tools.Plantomato)){
                getCellUnderMouse();
                addCrop();
            }
        }
    }

    public void getCellUnderMouse(){
        // 获取鼠标位置
        mousePosition = tilledSoilMapPlayer.GetLocalMousePosition();
        cellPosition = tilledSoilMapPlayer.LocalToMap(mousePosition);
        cellSourceid = tilledSoilMapPlayer.GetCellSourceId(cellPosition);

        localCellPosition = tilledSoilMapPlayer.MapToLocal(cellPosition);
        distance = player.GlobalPosition.DistanceTo(localCellPosition);

    }

    public void addCrop(){
        if(distance < 20.0){
            if(ToolManager.Instance.selectedTool == DataTypes.Tools.PlantCorn){
                var cornScene = cornSceneLoader.Instantiate() as Node2D;
                // 设置节点位置
                cornScene.GlobalPosition = localCellPosition;
                this.GetParent().FindChild("CropFields").AddChild(cornScene);
            }
            if(ToolManager.Instance.selectedTool == DataTypes.Tools.Plantomato){
                var tomatoScene = tomatoSceneLoader.Instantiate() as Node2D;
                // 设置节点位置
                tomatoScene.GlobalPosition = localCellPosition;
                this.GetParent().FindChild("CropFields").AddChild(tomatoScene);
            }
        }
    }

    public void removeCrop(){
        if(distance < 20.0){
            var cropNodes = GetParent().FindChild("CropFields").GetChildren();
            foreach(Node2D node in cropNodes){
                if(node.GlobalPosition == localCellPosition){
                    node.QueueFree();
                }
            }
        }
    }
}
