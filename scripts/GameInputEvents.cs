using Godot;


public partial class GameInputEvents : Node
{
	public Vector2 direction;

	public Vector2 MovementInput(){
		// 根据输入改变角色的方向
		if(Input.IsActionPressed("walk_up")){
			direction = Vector2.Up;
		}
		else if(Input.IsActionPressed("walk_down")){
			direction = Vector2.Down;
		}
		else if(Input.IsActionPressed("walk_left")){
			direction = Vector2.Left;
		}
		else if(Input.IsActionPressed("walk_right")){
			direction = Vector2.Right;
		}     
		else{
			direction = Vector2.Zero;
		} 

		return direction;
	}

	public bool IsMovementInput(){
		// 判断是否有方向输入
		if(direction == Vector2.Zero){
			return false;
		}
		else{
			return true;
		}

	}

	public bool UsingTool(){
		// 判断是否使用工具
		bool useToolValue = Input.IsActionJustPressed("hit");
		return useToolValue;
	}
}
