using Godot;
using System;

public partial class HurtComponent : Area2D
{
    [Export]
    public DataTypes.Tools tool = DataTypes.Tools.None;

    [Signal]
    public delegate void HurtEventHandler(int damage);

    public void OnAreaEntered(Area2D area)
    {
        // 碰撞处理
        var hit_component = area as HitComponent;
     
        if(tool == hit_component.CurrentTool){
            EmitSignal("Hurt", hit_component.hitDamage);
        }
    }
    
}
