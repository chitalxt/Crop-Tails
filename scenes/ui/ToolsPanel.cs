using Godot;
using System;

public partial class ToolsPanel : PanelContainer
{
    private Button ToolAxe;
    private Button ToolTilling;
    private Button ToolWateringCan;
    private Button ToolCorn;
    private Button ToolTomato;

    public override void _Ready() 
    {
        ToolManager.Instance.Connect("EnableTool", new Callable(this, nameof(OnEnableToolButton)));

        ToolAxe = GetNode<Button>("MarginContainer/HBoxContainer/ToolAxe");
        ToolTilling = GetNode<Button>("MarginContainer/HBoxContainer/ToolTilling");
        ToolWateringCan = GetNode<Button>("MarginContainer/HBoxContainer/ToolWateringCan");
        ToolCorn = GetNode<Button>("MarginContainer/HBoxContainer/ToolCorn");
        ToolTomato = GetNode<Button>("MarginContainer/HBoxContainer/ToolTomato");

        // 游戏一开始禁用工具
        ToolTilling.Disabled = true;  
        ToolTilling.FocusMode = Control.FocusModeEnum.None;

        ToolWateringCan.Disabled = true;
        ToolWateringCan.FocusMode = Control.FocusModeEnum.None;

        ToolCorn.Disabled = true;
        ToolCorn.FocusMode = Control.FocusModeEnum.None;

        ToolTomato.Disabled = true;
        ToolTomato .FocusMode = Control.FocusModeEnum.None;

    }


    public void OnToolAxePressed(){
        // 按下按钮后，通知工具管理器
        ToolManager.SelectTool(DataTypes.Tools.AxeWood);
    }
    
    public void OnToolTillingPressed(){
        // 按下按钮后，通知工具管理器
        ToolManager.SelectTool(DataTypes.Tools.TillGround);
    }

    public void OnToolWateringCanPressed(){
        // 按下按钮后，通知工具管理器
        ToolManager.SelectTool(DataTypes.Tools.WaterCrops);
    }

    public void OnToolToolCornPressed(){
        // 按下按钮后，通知工具管理器
        ToolManager.SelectTool(DataTypes.Tools.PlantCorn);
    }
    public void OnToolToolTomatoPressed(){
        // 按下按钮后，通知工具管理器
        ToolManager.SelectTool(DataTypes.Tools.Plantomato);
    }

    public override void _UnhandledInput(InputEvent @event){
        if(@event.IsActionPressed("release_tool")){
            ToolManager.SelectTool(DataTypes.Tools.None);
            ToolAxe.ReleaseFocus();
            ToolTilling.ReleaseFocus();
            ToolWateringCan.ReleaseFocus();
            ToolCorn.ReleaseFocus();
            ToolTomato.ReleaseFocus();
        }
    }

    public void OnEnableToolButton(DataTypes.Tools tool){
        // 工具管理器通知工具按钮可用
        if(tool == DataTypes.Tools.TillGround){
            ToolTilling.Disabled = false;
            ToolTilling.FocusMode = Control.FocusModeEnum.All;
        }
        else if(tool == DataTypes.Tools.WaterCrops){
            ToolWateringCan.Disabled = false; 
            ToolWateringCan.FocusMode = Control.FocusModeEnum.All;
        }
        else if(tool == DataTypes.Tools.PlantCorn){
            ToolCorn.Disabled = false;
            ToolCorn.FocusMode = Control.FocusModeEnum.All;
        }
        else if(tool == DataTypes.Tools.Plantomato){
            ToolTomato.Disabled = false;
            ToolTomato.FocusMode = Control.FocusModeEnum.All;
        }
    }

}
