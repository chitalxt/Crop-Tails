using Godot;

public partial class NpcIdleState : NodeState
{   
    [Export]
    Npc npc;
    [Export]
	AnimatedSprite2D animatedSprite2d;
    [Export]
    float IdleStateTimerInterval = 5;

    Timer IdleStateTimer;
    bool IdleStateTimeout = false; 

    public override void _Ready(){
        // 默认站立状态，5秒后切换行走状态
        IdleStateTimer = new Timer();
        IdleStateTimer.WaitTime = IdleStateTimerInterval;
        // NOTE: 定时器加载到场景树中会自动开启，定时器初始化时需设置，否则不会启动
        IdleStateTimer.Autostart = true;
        IdleStateTimer.Connect("timeout", new Callable(this, nameof(OnIdleStateTimeTimeout)));

        // 稍后添加到场景中（因为节点还未完全加载，所以设置延迟添加）
        this.CallDeferred(nameof(addTimer));

    }

    public override void _Process(double delta){
 
    }

    public override void _PhysicsProcess(double delta){
        
    }

    public override void OnNextTransitions(){
        // 超时后切换状态
        if(IdleStateTimeout){
            npc.isMoving = true;
            EmitSignal("Transition", "Walk");
        }
    }

    public override void _EnterTree(){
        animatedSprite2d.Play("idle");

        // 每次进入状态，重置超时状态
        IdleStateTimeout = false;

        // 确保定时器已加载到场景中
        if(IdleStateTimer != null && IdleStateTimer.IsInsideTree()){
            // 启动定时器
            IdleStateTimer.Start();
        }
        
    }

    public override void _ExitTree(){
        animatedSprite2d.Stop();
        // 停止定时器 
        IdleStateTimer.Stop();
    }

    public void addTimer(){
        GetTree().Root.AddChild(IdleStateTimer);
    }

    public void OnIdleStateTimeTimeout(){
         IdleStateTimeout = true;
    }


}
