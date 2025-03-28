 using Godot;
using System;

public partial class SaveGameManager : Node
{   
    /* 全局脚本，用于管理游戏存档 */
    // 单例模式
    public static SaveGameManager  Instance { get; private set; }

    public bool allowSaveGame;

    public override void _Ready()
    {
        Instance = this;
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if(@event.IsActionPressed("save_game")){
            SaveGame();
        }
    }

    public static void SaveGame(){
        SaveLevelDataComponent saveLevelDataComponent = Instance.GetTree().GetFirstNodeInGroup("save_level_data_component") as SaveLevelDataComponent;
        if (saveLevelDataComponent != null){
            saveLevelDataComponent.SaveGame();
        }
            
    }

    public static void LoadGame(){
        
        // 等待一帧，否则耕种的地块不会被加载
        Instance.CallDeferred(nameof(_LoadGame));

    }

    public async void _LoadGame(){
        await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);

        SaveLevelDataComponent saveLevelDataComponent = Instance.GetTree().GetFirstNodeInGroup("save_level_data_component") as SaveLevelDataComponent;
        if (saveLevelDataComponent != null){
            saveLevelDataComponent.LoadGame();
        }
            
    }
}
