 using Godot;
using System;

public partial class  NodeDataResource : Resource
{
    /* 保存节点数据资源的基类 */
    [Export]
    public Vector2 globalPosition;
    [Export]
    public NodePath nodePath;
    [Export]
    public NodePath parentNodePath;

    public virtual void _SaveData(Node2D node){
        // 保存节点数据(位置，节点路径，父节点路径)
        globalPosition = node.GlobalPosition;
        nodePath = node.GetPath();

        var parentNode = node.GetParent();
        if(parentNode != null){
            parentNodePath = parentNode.GetPath();
        }

    }

    public virtual void _LoadData(Window window){

    }
}
