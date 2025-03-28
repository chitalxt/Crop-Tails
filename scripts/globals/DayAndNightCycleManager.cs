using Godot;
using System;

public partial class DayAndNightCycleManager  : Control
{
    /* 昼夜循环管理器 */
    // 单例模式
    public static DayAndNightCycleManager  Instance { get; private set; }

    // 时间设置常量
    const int MINUTES_PER_DAY = 24 * 60;
    const int MINUTES_PER_HOUR = 60;
    const float GAME_MINUTE_DURATION = MathF.Tau / MINUTES_PER_DAY;

    // 游戏速度
    public float gameSpeed = 5.0f;

    // 初始时间
    public int InitialDay = 1;
    public int InitialHour = 12;
    public int InitialMinute = 30;

    // 当前时间
    float time = 0.0f;
    int currentMinute = -1;
    int currentDay = 0;

    [Signal]
    public delegate void GameTimeEventHandler(float time);
    [Signal]
    public delegate void TimeTickEventHandler(int day, int hour, int minute);
    [Signal]
    public delegate void TimeTickDayEventHandler(int day);

    public override void _Ready()
    {
        Instance = this;

        setInitialTime();
    }

    public override void _Process(double delta){
        // 计算游戏时间
        time += (float)delta * gameSpeed * GAME_MINUTE_DURATION;

        EmitSignal("GameTime", time);
        RecalulateTime();
    }


    public void setInitialTime(){
        // 设置初始时间
        var initialTotalMinutes = InitialDay * MINUTES_PER_DAY + InitialHour * MINUTES_PER_HOUR + InitialMinute;

        // 转换为游戏时间
        time = initialTotalMinutes * GAME_MINUTE_DURATION;
    }

    public void RecalulateTime(){
        // 重新计算时间 
        var totalMinutes = (int)(time / GAME_MINUTE_DURATION);
        var day = (int)(totalMinutes / MINUTES_PER_DAY);

        // 计算小时和分钟
        var currentDayMinutes = totalMinutes % MINUTES_PER_DAY;

        var hour = (int)(currentDayMinutes / MINUTES_PER_HOUR);
        var minute = (int)(currentDayMinutes % MINUTES_PER_HOUR);

        // 发送信号
        if(currentDay != day){
            currentDay = day;
            EmitSignal("TimeTickDay", currentDay);
        }
        if(currentMinute!= minute){
            currentMinute = minute;
            EmitSignal("TimeTick", currentDay, hour, minute);
        }

    }
}
