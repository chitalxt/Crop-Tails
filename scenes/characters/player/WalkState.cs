using Godot;


public partial class WalkState : NodeState
{   

	[Export]
	Player player;
	[Export]
	AnimatedSprite2D animatedSprite2d;
	[Export]
	int speed = 50;  

	GameInputEvents gameInputEvents; 

	public override void _Ready(){
		gameInputEvents = new GameInputEvents();
	}

	public override void _Process(double delta){
		
	}

	public override void _PhysicsProcess(double delta){
		
		if(!player.isUsingTool){
			var direction = gameInputEvents.MovementInput();

			// 根据不同的方向，播放不同的动画
			if(direction == Vector2.Up){
				animatedSprite2d.Play("walk_back");
			}
			else if(direction == Vector2.Down){
				animatedSprite2d.Play("walk_front");
			}
			else if(direction == Vector2.Left){
				animatedSprite2d.Play("walk_left");
			}
			else if(direction == Vector2.Right){
				animatedSprite2d.Play("walk_right");
			}

			if(direction != Vector2.Zero){
				player.isMoving = true;
				player.playerDirection = direction;
			}

			// 移动角色
			player.Velocity = direction * speed;
			player.MoveAndSlide();
		}
		
	}

	public override void OnNextTransitions(){
		if(!gameInputEvents.IsMovementInput()){
			EmitSignal("Transition", "Idle");
		}
	}

	public override void _EnterTree(){
	}

	public override void _ExitTree(){
		// 停止动画播放
		animatedSprite2d.Stop();
		player.isMoving = false;
	}
}
