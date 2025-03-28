 using Godot;
using Godot.Collections;
using System;
using System.Threading.Tasks;

public partial class FieldCursorComponent : Node
{
    /* 耕地地块的添加与移除 */
    [Export]
    TileMapLayer grassTileMapLayer;
    [Export]
    TileMapLayer tilledSoilTileMapLayer;
    [Export]
    int terrainset = 0;
    [Export]
    int terrain = 3;

    private Player player;

    Vector2 mousePosition;
    Vector2I cellPosition;
    int cellSourceid;
    Vector2 localCellPosition;
    float distance;

    public override void  _Ready(){
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
                removeTilledSoilCell();
            }
        }
        else if(@event.IsActionPressed("hit")){
                if(ToolManager.Instance.selectedTool == DataTypes.Tools.TillGround){
                    getCellUnderMouse();
                    addTilledSoilCell();
                }
        }
    }

    public void getCellUnderMouse(){
        // 获取鼠标位置
        mousePosition = grassTileMapLayer.GetLocalMousePosition();
        cellPosition = grassTileMapLayer.LocalToMap(mousePosition);
        cellSourceid = grassTileMapLayer.GetCellSourceId(cellPosition);

        localCellPosition = grassTileMapLayer.MapToLocal(cellPosition);
        distance = player.GlobalPosition.DistanceTo(localCellPosition);

    }

    public void addTilledSoilCell(){
        if(distance < 20.0f && cellSourceid != -1){
            // 添加耕地地块
            tilledSoilTileMapLayer.SetCellsTerrainConnect(new Array<Vector2I>(){ cellPosition }, terrainset, terrain, true);
        }
    }

    public void removeTilledSoilCell(){
        if(distance < 20.0f && cellSourceid != -1){
            // 移除耕地地块
            tilledSoilTileMapLayer.SetCellsTerrainConnect(new Array<Vector2I>(){ cellPosition }, 0, -1, true);
        }
    }
}
