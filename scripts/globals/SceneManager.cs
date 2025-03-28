using Godot;
using System;
using System.Collections.Generic;

public partial class SceneManager : Node
{
    // 单例
    public static SceneManager Instance { get; private set; }

    string mainScenePath = "res://scenes/levels/MainScene.tscn";
    string mainSceneRootPath = "/root/MainScene";
    string mainSceneLevelRootPath = "/root/MainScene/GameRoot/LevelRoot";

    Dictionary<string, string> levelScenes = new (){
        ["Level1"] = "res://scenes/levels/Level1.tscn", 
    }; 

    public override void _Ready(){
        Instance = this;
    }

    public static void LoadMainSceneContainer(){
        if(Instance.GetTree().Root.HasNode(Instance.mainSceneRootPath)){
            return; 
        }

        PackedScene mainSceneLoader = ResourceLoader.Load<PackedScene>("res://scenes/levels/MainScene.tscn");
        Node node = mainSceneLoader.Instantiate() as Node;

        if(node != null){
            Instance.GetTree().Root.AddChild(node);
        }
    }

    public static void LoadLevel(string level){
        if(Instance.levelScenes.TryGetValue(level, out string scenePath)){
            PackedScene levelSceneLoader = ResourceLoader.Load<PackedScene>(scenePath);
            Node levelScene = levelSceneLoader.Instantiate() as Node;
            Node levelRoot = Instance.GetNode(Instance.mainSceneLevelRootPath);

            if(levelRoot != null){
                var nodes = levelRoot.GetChildren();
                foreach(Node node in nodes){
                    node.QueueFree();
                }

                Instance.CallDeferred(nameof(LoadLevelSetup), levelRoot,levelScene);

            }

        }

    }

    public async void LoadLevelSetup(Node levelRoot, Node levelScene)
    {  
        // 等待一帧，否则levelSce ne的节点名会随机
        await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);
        levelRoot.AddChild(levelScene);
    }


}
