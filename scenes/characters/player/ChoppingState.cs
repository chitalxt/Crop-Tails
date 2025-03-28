using Godot;


public partial class ChoppingState : NodeState
{   

	[Export]
	Player player;
	[Export]
	AnimatedSprite2D animatedSprite2d;
    [Export]
    CollisionShape2D hitComponentCollisionShape2D;

    public override void _Ready(){
        // 设置击打组件的碰撞形状为不可用，默认位置为（0,0）
        hitComponentCollisionShape2D.Disabled = true;
        hitComponentCollisionShape2D.Position = new Vector2(0, 0);
    }

    public override void _Process(double delta){

    }

    public override void _PhysicsProcess(double delta){
        
    }

    public override void OnNextTransitions(){
        if(!animatedSprite2d.IsPlaying()){
            EmitSignal("Transition", "Idle");
        }
    }

    public override void _EnterTree(){
        // 进入状态时，播放动画
        if(player.playerDirection == Vector2.Up){
			animatedSprite2d.Play("chopping_back");
            hitComponentCollisionShape2D.Position = new Vector2(0, -18);
		}
		else if(player.playerDirection == Vector2.Down){
			animatedSprite2d.Play("chopping_front");
            hitComponentCollisionShape2D.Position = new Vector2(0, 3);
		}
		else if(player.playerDirection == Vector2.Left){
			animatedSprite2d.Play("chopping_left");
            hitComponentCollisionShape2D.Position = new Vector2(-9, 0);
		}
		else if(player.playerDirection == Vector2.Right){
			animatedSprite2d.Play("chopping_right");
            hitComponentCollisionShape2D.Position = new Vector2(9, 0);
		}
        else{
			animatedSprite2d.Play("chopping_front");
            hitComponentCollisionShape2D.Position = new Vector2(0, 3);
		}

        hitComponentCollisionShape2D.Disabled = false;

    }

    public override void _ExitTree(){
        animatedSprite2d.Stop();
        player.isUsingTool = false;
        hitComponentCollisionShape2D.Disabled = true;
    }
}
