using Godot;
using System;

public partial class DayAndNightComponent : CanvasModulate
{
    // 私有字段存储实际值
    private int _initialDay = 1;
    // 导出属性（显示在编辑器）
    [Export]
    public int InitialDay{        // 在游戏运行时实时更新数值
        get => _initialDay;
        set{
            // 设置新值
            _initialDay = value;
            // 设置管理器属性
            /* NOTE: 这里的DayAndNightCycleManager.Instance为null， 因为它还没有被实例化
            所以在程序初始化(控制器上)设置值会报错，提示DayAndNightCycleManager.Instance为null */
            DayAndNightCycleManager.Instance.InitialDay = value;
            DayAndNightCycleManager.Instance.setInitialTime();
            
        }
    }
    private int _initialHour = 12;
    [Export]
    public int InitialHour{
        get => _initialHour;
        set{
            _initialHour = value;
            DayAndNightCycleManager.Instance.InitialHour = value;
            DayAndNightCycleManager.Instance.setInitialTime();
        }
    }

    private int _initialMinute = 30;
    [Export]
    public int InitialMinute{
        get => _initialMinute;
        set{
            _initialMinute = value;
            DayAndNightCycleManager.Instance.InitialMinute = value;
            DayAndNightCycleManager.Instance.setInitialTime();
        }
    }
    
    [Export]
    GradientTexture1D DayNightGradientTexture;

    public override void _Ready(){
        DayAndNightCycleManager.Instance.InitialDay = _initialDay;
        DayAndNightCycleManager.Instance.InitialHour = _initialHour;
        DayAndNightCycleManager.Instance.InitialMinute = _initialMinute;
        DayAndNightCycleManager.Instance.setInitialTime();

        DayAndNightCycleManager.Instance.Connect("GameTime", new Callable(this, nameof(OnGameTime)));

    }

    public void OnGameTime(float time){
        var sampleValue = 0.5 * (MathF.Sin((float)(time - MathF.PI * 0.5)) + 1.0);
        Color = DayNightGradientTexture.Gradient.Sample((float)sampleValue);

    }

}
