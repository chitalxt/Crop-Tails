using Godot;


public partial class ToolManager : Node
{
    // 单例模式
    public static ToolManager Instance { get; private set; }

    [Signal]
    public delegate void ToolSelectedEventHandler(DataTypes.Tools tool);
    [Signal]
    public delegate void EnableToolEventHandler(DataTypes.Tools tool);

    public DataTypes.Tools selectedTool = DataTypes.Tools.None;

    public override void _Ready()
    {
        Instance = this;
    }

    public static void SelectTool(DataTypes.Tools tool){
        // 将 DataTypes.Tools 转换为 Godot.Variant
        Variant toolVariant = (int)tool;
        Instance.EmitSignal("ToolSelected", toolVariant);
        Instance.selectedTool = tool;
    }

    public static void EnableToolButton(DataTypes.Tools tool){
        Variant toolVariant = (int)tool;
        Instance.EmitSignal("EnableTool", toolVariant);
    }
        

}
