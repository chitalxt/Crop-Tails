using Godot;
using System;

public partial class SaveDataComponent : Node
{
    /* 节点数据保存组件 */
    [Export]
    NodeDataResource saveDataResource;
    private Node2D parentNode;

    public override void _Ready(){
        parentNode = GetParent<Node2D>();

        // 添加到全局组中
        AddToGroup("save_data_component");
    } 

    public NodeDataResource SaveData(){
        if(parentNode == null){
            return null;
        }

        if(saveDataResource == null){
            GD.PushError("saveDataResource: ", saveDataResource, parentNode.Name);
            return null;
        }
        saveDataResource._SaveData(parentNode);

        return saveDataResource as NodeDataResource;
    }
}
