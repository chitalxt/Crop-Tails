using Godot;


public partial class TillingState : NodeState
{   

	[Export]
	Player player;
	[Export]
	AnimatedSprite2D animatedSprite2d;

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
			animatedSprite2d.Play("tilling_back");
		}
		else if(player.playerDirection == Vector2.Down){
			animatedSprite2d.Play("tilling_front");
		}
		else if(player.playerDirection == Vector2.Left){
			animatedSprite2d.Play("tilling_left");
		}
		else if(player.playerDirection == Vector2.Right){
			animatedSprite2d.Play("tilling_right");
		}
        else{
			animatedSprite2d.Play("tilling_front");
		}

    }

    public override void _ExitTree(){
        animatedSprite2d.Stop();
        player.isUsingTool = false;
    }
}
