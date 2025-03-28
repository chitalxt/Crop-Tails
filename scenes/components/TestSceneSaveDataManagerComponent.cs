using Godot;
using System;

public partial class TestSceneSaveDataManagerComponent : Node
{
    /* 用于 加载 测试场景数据 的管理组件 */
    public override void _Ready(){
        CallDeferred(nameof(LoadTestScene));
    }

    public void LoadTestScene(){
        SaveGameManager.LoadGame();
    }
        
}
