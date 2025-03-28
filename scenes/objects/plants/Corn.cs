using Godot;
using System;

public partial class Corn : Node2D
{
    PackedScene CornHarvestSceneLoader = ResourceLoader.Load<PackedScene>("res://scenes/objects/plants/CornHarvest.tscn");

    private Sprite2D sprite2D;
    private GpuParticles2D waterParticles;
    private GpuParticles2D floweringParticles;
    private GrowthCycleComponent growthCycleComponent;
    private HurtComponent hurtComponent;

    DataTypes.GrowthStates growthState = DataTypes.GrowthStates.Seed; 

    public override void _Ready(){
        sprite2D = GetNode<Sprite2D>("Sprite2D");
        waterParticles = GetNode<GpuParticles2D>("WateringParticles");
        floweringParticles = GetNode<GpuParticles2D>("FloweringParticles");
        growthCycleComponent = GetNode<GrowthCycleComponent>("GrowthCycleComponent");
        hurtComponent = GetNode<HurtComponent>("HurtComponent");

        hurtComponent.Connect("Hurt", new Callable(this, nameof(onHurt)));
        growthCycleComponent.Connect("CropMaturity", new Callable(this, nameof(onCropMaturity)));
        growthCycleComponent.Connect("CropHarvesting", new Callable(this, nameof(onCorpHarvesting)));

        waterParticles.Emitting = false;
        floweringParticles.Emitting = false;
    
    }

    public override void _Process(double delta){
        growthState = growthCycleComponent.getCurrentGrowthState();
        // 根据不同的状态，设置不同的动画帧
        sprite2D.Frame = (int)growthState;

        if(growthState == DataTypes.GrowthStates.Maturity){
            floweringParticles.Emitting = true;
        }

    }

    public void onHurt(int damage){
        // 如果作物未浇水
        if(!growthCycleComponent.isWatered){
            waterParticles.Emitting = true; 
            GetTree().CreateTimer(5).Timeout += resetParticles;

        }
    }

    public void resetParticles(){
        // 重置水粒子发射状态
        waterParticles.Emitting = false;
        // 设置作物为已浇水
        growthCycleComponent.isWatered = true;
    }

    public void  onCropMaturity(){
        // 作物成熟后，发射开花粒子
        floweringParticles.Emitting = true;
    }


    public void onCorpHarvesting(){
        // 作物收割完成后，加载收割场景
        var CornHarvestScene = CornHarvestSceneLoader.Instantiate() as Node2D;
        
        // 设置节点位置
        CornHarvestScene.GlobalPosition = this.GlobalPosition;
        
        this.GetParent().AddChild(CornHarvestScene);

        this.QueueFree();
    }
}
