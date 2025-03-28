using Godot;
using System;

public partial class DamageComponent : Node2D
{
    [Export]
    int MaxDamage = 1;
    [Export]
    int CurrentDamage = 0;

    [Signal]
    public delegate void MaxDamageReachedEventHandler();


    public void applyDamage(int damage){
        // 伤害处理
        if(CurrentDamage < MaxDamage){
            CurrentDamage += damage;
        }

        if(CurrentDamage == MaxDamage){
            EmitSignal("MaxDamageReached");
        }
    }
        

}
