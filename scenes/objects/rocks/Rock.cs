using Godot;
using System;

public partial class Rock: Sprite2D
{
    PackedScene StoneSceneLoader = ResourceLoader.Load<PackedScene>("res://scenes/objects/rocks/Stone.tscn");

    private HurtComponent hurtComponent;
    private DamageComponent damageComponent;

    public override void _Ready()
    {
        // 获取组件
        hurtComponent = GetNode<HurtComponent>("HurtComponent");
        damageComponent = GetNode<DamageComponent>("DamageComponent");
        // 连接信号
        hurtComponent.Connect("Hurt", new Callable(this, nameof(onHurt)));
        damageComponent.Connect("MaxDamageReached", new Callable(this, nameof(onMaxDamageReached)));

    }


    public void onHurt(int damage){
        // 处理伤害
        damageComponent.applyDamage(damage);

        // 设定摇晃参数
        ((ShaderMaterial)Material).SetShaderParameter("shake_intensity", 0.3);

        // 1 秒后重置摇晃参数
        GetTree().CreateTimer(0.5).Timeout += resetShakeIntensity;
 
    }
    

    public void onMaxDamageReached(){
        // 稍后加载Log场景
        this.CallDeferred(nameof(addStoneScene));

        // 达到最大伤害时，销毁节点
        QueueFree();
    }

    public void addStoneScene(){
        // 加载Log场景
        var StoneScene = StoneSceneLoader.Instantiate() as Node2D;
        
        // 设置原木节点位置
        StoneScene.GlobalPosition = this.GlobalPosition;
        
        this.GetTree().Root.AddChild(StoneScene);
    }

    public void resetShakeIntensity(){
        // 重置摇晃参数
        ((ShaderMaterial)Material).SetShaderParameter("shake_intensity", 0);
    }

}
