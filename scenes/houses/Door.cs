using Godot;
using System;
using System.Security.Cryptography.X509Certificates;

public partial class Door : StaticBody2D
{
    [Export]
    private AnimatedSprite2D animatedSprite2D;
    [Export]
    private CollisionShape2D collisionShape2D;
    [Export]
    private InteractableComponent interactableComponent;

    public override void _Ready()
    {
        // 连接信号
        interactableComponent.Connect("InteractableActivated", new Callable(this, nameof(onInteractableActivated)));
        interactableComponent.Connect("InteractableDeActivated", new Callable(this, nameof(onInteractableDeActivated)));

        this.CollisionLayer = 1; // 设定碰撞层为1，用于与玩家碰撞
    }

    public void onInteractableActivated()
    {
        animatedSprite2D.Play("open_door");
        this.CollisionLayer = 2;    // 设定碰撞层为2，与玩家处于同一层
    }

    public void onInteractableDeActivated()
    {
        animatedSprite2D.Play("close_door");
        this.CollisionLayer = 1; // 设定碰撞层为1，用于与玩家碰撞
    }
	
}
