using Godot;
using System;

public partial class SceneDataResource : NodeDataResource
{
    /* 处理场景数据资源 */
    [Export]
    string nodeName;
    [Export]
    string sceneFilePath;

    public override void _SaveData(Node2D node){
        base._SaveData(node);

        nodeName = node.Name;
        sceneFilePath = node.SceneFilePath;

    }
    public override void _LoadData(Window window){
        Node2D parentNode = null;
        Node2D sceneNode = null;

        if(parentNodePath != null){
            // 获取父节点
            parentNode = window.GetNodeOrNull(parentNodePath) as Node2D;
        }

        if(nodePath != null){
            // 加载场景资源
            var sceneFileResource = ResourceLoader.Load<PackedScene>(sceneFilePath);
            sceneNode = sceneFileResource.Instantiate() as Node2D;
        }

        if((parentNode != null) && (sceneNode != null)){
            // 父节点和场景资源都存在，则将场景资源添加到父节点下
            sceneNode.GlobalPosition = globalPosition;
            parentNode.AddChild(sceneNode);
        }
                        
        

    }

}
