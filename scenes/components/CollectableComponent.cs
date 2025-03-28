using Godot;
using System;

public partial class CollectableComponent : Area2D
{
    [Export]
    public string CollectableName;

    public void onBodyEntered(Node body){
        if(body is Player){
            // 收集物品到背包中
            InventoryManagement.AddCollectable(CollectableName);
            // 物体被收集后，销毁节点
            this.GetParent().QueueFree();

        }
    }

}
