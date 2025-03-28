using Godot;
using System;

public partial class GrowthCycleComponent : Node
{
    [Export]
    DataTypes.GrowthStates CurrentGrowthState = DataTypes.GrowthStates.Germination;

    [Export(PropertyHint.Range, "5, 365")]
    public int DayUntilHarvest = 7;

    [Signal]
    public delegate void CropMaturityEventHandler();

    [Signal]
    public delegate void CropHarvestingEventHandler();

    public bool isWatered;
    int startingDay;
    int currentDay;
    

    public override void _Ready(){
        DayAndNightCycleManager.Instance.Connect("TimeTickDay", new Callable(this, nameof(onTimeTickDay)));

    }

    public void onTimeTickDay(int day){
        if(isWatered){
            if(startingDay == 0){
                startingDay = day;
            }

            GrowthStates(startingDay, day);
            HarvestStates(startingDay, day);
        }
    }

    public void GrowthStates(int startingDay, int currentDay){
        // 成熟 或 收获阶段不检测
        if((CurrentGrowthState == DataTypes.GrowthStates.Maturity) || (CurrentGrowthState == DataTypes.GrowthStates.Harvesting)){
            return;
        }

        var numStates = 5;

        var growthDayPassed = (currentDay - startingDay) % numStates;
        var stateIndex = (growthDayPassed % numStates) + 1;

        CurrentGrowthState = (DataTypes.GrowthStates)stateIndex;

        if(CurrentGrowthState == DataTypes.GrowthStates.Maturity){
            EmitSignal("CropMaturity");
        }

    }

    public void HarvestStates(int startingDay, int currentDay){
        // 收获
        if(CurrentGrowthState == DataTypes.GrowthStates.Harvesting){
            return;
        }

        var dayPassed = (currentDay - startingDay) % DayUntilHarvest;

        if(dayPassed == DayUntilHarvest - 1){
            CurrentGrowthState = DataTypes.GrowthStates.Harvesting;
            EmitSignal("CropHarvesting");
        }

    }

    public DataTypes.GrowthStates getCurrentGrowthState(){
        return CurrentGrowthState;
    }
      
}
