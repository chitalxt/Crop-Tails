using Godot;

public partial class SaveLevelDataComponent : Node
{
    /* 场景数据保存组件 */
    string levelSceneName;
    string saveGameDataPath = "user://game_data/";
    string saveFileName = "save_{0}_game_data.tres";

    SaveGameDataResource gameDataResource;

    public override void _Ready(){
        AddToGroup("save_level_data_component");

        levelSceneName = GetParent().Name;

    }

    public void SaveNodeData(){
        var nodes = GetTree().GetNodesInGroup("save_data_component");
        gameDataResource = new SaveGameDataResource();
         
        if(nodes != null){
            foreach(SaveDataComponent node in nodes){
                if(node is SaveDataComponent){
                    NodeDataResource saveDataResource = node.SaveData();
                    var saveFinialResource = saveDataResource.Duplicate() as NodeDataResource;
                    // 存储到资源文件中
                    gameDataResource.saveDataNodes.Add(saveFinialResource);
                }
            }
        }
            
    }

    public void SaveGame(){
        if(!DirAccess.DirExistsAbsolute(saveGameDataPath)){
            // 创建文件夹
            DirAccess.MakeDirAbsolute(saveGameDataPath);
        }

        string levelFileSaveName = string.Format(saveFileName, levelSceneName);

        // 保存资源文件
        SaveNodeData();

        var result = ResourceSaver.Save(gameDataResource, saveGameDataPath + levelFileSaveName);

    }

    public void LoadGame(){
        string levelFileSaveName = string.Format(saveFileName, levelSceneName);
        string saveGamePath = saveGameDataPath + levelFileSaveName;
        if(!FileAccess.FileExists(saveGamePath)){
            return;
        }

        // 加载资源文件
        gameDataResource = ResourceLoader.Load(saveGamePath) as SaveGameDataResource;
        if(gameDataResource == null){
            return;
        }

        // 加载节点数据
        Window rootNode = GetTree().Root;
        foreach(NodeDataResource resource in gameDataResource.saveDataNodes){
            if(resource is NodeDataResource){
                resource._LoadData(rootNode);
            }
                
        }
 
    }

}
