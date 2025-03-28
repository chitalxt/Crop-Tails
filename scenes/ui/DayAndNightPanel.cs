using Godot;
using System;

public partial class DayAndNightPanel : Control
{
    [Export]
    int NormalSpeed = 5;
    [Export]
    int FastSpeed = 100;
    [Export]
    int CheetahSpeed = 200;

    private Label DayLabel;
    private Label TimeLabel;

    public override void _Ready()
    {
        DayLabel = GetNode<Label>("DayPanel/MarginContainer/DayLabel");
        TimeLabel = GetNode<Label>("TimePanel/MarginContainer/TimeLabel");

        DayAndNightCycleManager.Instance.Connect("TimeTick", new Callable(this, "OnTimeTick"));
    }

     public void OnTimeTick(int day, int hour, int minute){
        DayLabel.Text = $"Day {day}";
        TimeLabel.Text = $"{hour:D2}:{minute:D2}";

    }

    public void OnNormalSpeedButtonPressed(){
        DayAndNightCycleManager.Instance.gameSpeed = NormalSpeed;
    }
    public void OnFastSpeedButtonPressed(){
        DayAndNightCycleManager.Instance.gameSpeed = FastSpeed;
    }

    public void OnCheetahSpeedButtonPressed(){
        DayAndNightCycleManager.Instance.gameSpeed = CheetahSpeed;
    } 

}
