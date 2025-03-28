using Godot;

public partial class NpcWalkState : NodeState
{   
    [Export]
    Npc npc;
    [Export]
	AnimatedSprite2D animatedSprite2d;
    [Export]    
    NavigationAgent2D navigationAgent2D;

    double minSpeed = 5;
    double maxSpeed = 10;
    double nextSpeed;

    public override void _Ready()
    {
        // 避障功能开启时，设置安全速度
        navigationAgent2D.VelocityComputed += OnSafeVelocityComputed;

        this.CallDeferred(nameof(CharacterSetup));
    }

    public async void CharacterSetup()
    {  
        // 等待第一个物理帧执行后，设置小鸡的导航路线
        await ToSignal(GetTree(), SceneTree.SignalName.PhysicsFrame);
        
        SetMovementTarget();
    }

    public void SetMovementTarget(){
        // 在导航区域内随机获取一个位置作为小鸡的目标点
        var targetPosition = NavigationServer2D.MapGetRandomPoint(navigationAgent2D.GetNavigationMap(), navigationAgent2D.NavigationLayers, false);

        navigationAgent2D.TargetPosition = targetPosition;
        // 随机获取速度
        nextSpeed = GD.RandRange(minSpeed, maxSpeed);
    
    }

    public override void _Process(double delta){
 
    }
 
    public override void _PhysicsProcess(double delta){
        if(navigationAgent2D.IsNavigationFinished()){
            npc.currentWalkCircle += 1;
            // 导航完成后，重新设置目标点
            SetMovementTarget();
            return;
        }

        if(npc.isMoving){
            var nextPosition = navigationAgent2D.GetNextPathPosition();
            var nextDirection = npc.GlobalPosition.DirectionTo(nextPosition);
            var velocity = nextDirection * (float)nextSpeed;

            if(navigationAgent2D.AvoidanceEnabled){
                animatedSprite2d.FlipH = velocity.X < 0;
                navigationAgent2D.Velocity = velocity;
            }
            else{
                npc.Velocity = velocity;
                npc.MoveAndSlide();
            }

        }

    }

    public void OnSafeVelocityComputed(Vector2 safeVelocity){
        // 设置安全避障速度
        if(npc.isMoving){
            animatedSprite2d.FlipH = safeVelocity.X < 0;
            npc.Velocity = safeVelocity;
            npc.MoveAndSlide();
        }
    }

    public override void OnNextTransitions(){
        // 如果导航完成，则切换到Idle状态
        if(npc.currentWalkCircle == npc.walkCircles){
            npc.Velocity = Vector2.Zero;
            npc.isMoving  = false;

            EmitSignal("Transition", "Idle");
        }
    }
    
    public override void _EnterTree(){
        animatedSprite2d.Play("walk");
        npc.currentWalkCircle = 0; 
    }

    public override void _ExitTree(){
        animatedSprite2d.Stop();
        npc.isMoving = false;
    }
}
