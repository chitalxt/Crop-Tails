 using Godot;
using Godot.Collections;
using System;

public partial class TilemapLayerDataResource : NodeDataResource
{
    /* 处理地块资源数据 */   
    [Export]
    Array<Vector2I> tilemapLayerUsedCells;
    [Export]
    int terrainSet  = 0;
    [Export]
    int terrain = 3;

    public override void _SaveData(Node2D node){
        base._SaveData(node);

        TileMapLayer tilemapLayer = node as TileMapLayer;
        Array<Vector2I> cells = tilemapLayer.GetUsedCells();

        tilemapLayerUsedCells = cells;

    }


    public override void _LoadData(Window window){
        var sceneNode = window.GetNodeOrNull(nodePath);

        if(sceneNode != null){
            TileMapLayer tilemapLayer = sceneNode as TileMapLayer;
            tilemapLayer.SetCellsTerrainConnect(tilemapLayerUsedCells, terrainSet, terrain, true);

        }

    }
        


}
