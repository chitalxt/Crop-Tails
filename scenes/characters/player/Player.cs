using Godot;

public partial class Player : CharacterBody2D
{
    [Export]
    public DataTypes.Tools currentTool = DataTypes.Tools.None;

    public Vector2 playerDirection;
 
    public bool isMoving = false;

    public bool isUsingTool = false;


    private HitComponent hitComponent;

    public override void _Ready(){
        ToolManager.Instance.Connect("ToolSelected", new Callable(this, nameof(OnToolSelected)));
        hitComponent = GetNode<HitComponent>("HitComponent");
    }

    public void OnToolSelected(DataTypes.Tools tool){ 
        // 选择工具后，会发信号通知玩家脚本，更新玩家当前工具
        currentTool = tool;
        // 更新击打组件当前工具 
        hitComponent.CurrentTool = tool;

    }

}
