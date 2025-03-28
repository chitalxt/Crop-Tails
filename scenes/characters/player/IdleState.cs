using Godot;


public partial class IdleState : NodeState
{   

	[Export]
	Player player;
	[Export]
	AnimatedSprite2D animatedSprite2d;

	GameInputEvents gameInputEvents; 

	public override void _Ready(){
		gameInputEvents = new GameInputEvents();
	}

	public override void _Process(double delta){
		
	}

	public override void _PhysicsProcess(double delta){
		// 判断玩家状态 
		if(!player.isMoving && !player.isUsingTool){
			// 根据不同的方向，播放不同的动画
			if(player.playerDirection == Vector2.Up){
				animatedSprite2d.Play("idle_back");
			}
			else if(player.playerDirection == Vector2.Down){
				animatedSprite2d.Play("idle_front");
			}
			else if(player.playerDirection == Vector2.Left){
				animatedSprite2d.Play("idle_left");
			}
			else if(player.playerDirection == Vector2.Right){
				animatedSprite2d.Play("idle_right");
			}
			else {
				animatedSprite2d.Play("idle_front");
			}
		}
	}

	public override void OnNextTransitions(){

		gameInputEvents.MovementInput();

		if (gameInputEvents.IsMovementInput()){
			EmitSignal("Transition", "Walk"); 
		}
		
		if (player.currentTool == DataTypes.Tools.AxeWood && gameInputEvents.UsingTool()){
			player.isUsingTool = true;
			EmitSignal("Transition", "Chopping");
		}

		if (player.currentTool == DataTypes.Tools.TillGround && gameInputEvents.UsingTool()){
			player.isUsingTool = true;
			EmitSignal("Transition", "Tilling");
		}

		if (player.currentTool == DataTypes.Tools.WaterCrops && gameInputEvents.UsingTool()){
			player.isUsingTool = true;
			EmitSignal("Transition", "Watering");
		}
	}

	public override void _EnterTree(){
	}

	public override void _ExitTree(){
		// 停止动画播放
		animatedSprite2d.Stop();
		
	}
}
