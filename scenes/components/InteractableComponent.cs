using Godot;
using System;

public partial class InteractableComponent : Area2D
{
    /* 可交互组件管理器 */ 
    [Signal]
    public delegate void InteractableActivatedEventHandler();
     [Signal]
    public delegate void InteractableDeActivatedEventHandler();


    public void onBodyEntered(Node body)
    {
        // 发生碰撞
        EmitSignal("InteractableActivated");
    }

    public void onBodyExited(Node body)
    {
        // 结束碰撞
        EmitSignal("InteractableDeActivated");
    }

}
