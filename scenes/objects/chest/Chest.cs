using Godot;
using Godot.Collections;
using DialogueManagerRuntime;

public partial class Chest : Node2D
{
    [Export]
    string dialogueStartCommand; // 对话脚本中执行的命令
    [Export]
    int foodDropHight = 40 ; // 食物掉落高度
    [Export]
    int rewardOutputRadius =20; // 奖励输出半径
    [Export]
    Array<PackedScene> outputRewardScenes = new(){};     // 奖励场景

    PackedScene balloonScene = ResourceLoader.Load<PackedScene>("res://dialogue/GameDialogueBalloon.tscn");
    PackedScene cornHarvestScene = ResourceLoader.Load<PackedScene>("res://scenes/objects/plants/CornHarvest.tscn");
    PackedScene tomatoHarvestScene = ResourceLoader.Load<PackedScene>("res://scenes/objects/plants/TomatoHarvest.tscn");

    private InteractableComponent interactableComponent;
    private AnimatedSprite2D animatedSprite2D;
    private FeedComponent feedComponent;
    private Marker2D rewardMarker;
    private Control interactableLabelComponent;

    bool inRanage;
    bool isChestOpen;


    public override void _Ready(){
        interactableComponent = GetNode<InteractableComponent>("InteractableComponent");
        animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        feedComponent = GetNode<FeedComponent>("FeedComponent");
        rewardMarker = GetNode<Marker2D>("RewardMarker");
        interactableLabelComponent = GetNode<Control>("InteractableLabelComponent");

        interactableComponent.Connect("InteractableActivated", new Callable(this, nameof(OnInteractableActivated)));
        interactableComponent.Connect("InteractableDeActivated", new Callable(this, nameof(OnInteractableDeActivated)));

        interactableLabelComponent.Hide();

        // 通过与箱子的对话选择喂养动物的选项，触发FeedTheAnimals事件
        GameDialogueManager.Instance.Connect("FeedTheAnimals", new Callable(this, nameof(OnFeedTheAnimals)));
        // 喂养的食物投放至箱子后触发事件
        feedComponent.Connect("FoodReceived", new Callable(this, nameof(OnFoodReceived)));

    }

    public void OnInteractableActivated(){
        interactableLabelComponent.Show();
        inRanage = true;
    }

    public void OnInteractableDeActivated(){
        if(isChestOpen){
            animatedSprite2D.Play("chest_close");

        }
        isChestOpen = false;
        interactableLabelComponent.Hide();
        inRanage = false;
    }

    public override void _UnhandledInput(InputEvent @event)
    { 
        if(inRanage){
            if(@event.IsActionPressed("show_dialogue")){
                interactableLabelComponent.Hide();
                animatedSprite2D.Play("chest_open");
                isChestOpen = true;

                // 创建对话
                BaseGameDialogueBalloon balloon = balloonScene.Instantiate() as BaseGameDialogueBalloon;

                GetTree().Root.AddChild(balloon);    
                // 开始对话，加载对话脚本
                balloon.Start(GD.Load<Resource>("res://dialogue/conversations/chest.dialogue"), dialogueStartCommand);

            }
        }
    }

    public void OnFeedTheAnimals(){
        if(inRanage){
            TiggerFeedHarvest("corn", cornHarvestScene);
            TiggerFeedHarvest("tomato", tomatoHarvestScene);
        }
    }

    public void TiggerFeedHarvest(string inventoryItem, PackedScene scene){
        var inventory = InventoryManagement.Instance.Inventory;

        // 判断仓库中是否存在该物品
        if(!inventory.ContainsKey(inventoryItem)){
            return;
        }

        var inventoryCount = inventory[inventoryItem];
        
        for(int index = 1;index <= inventoryCount; index++){
            var harvestScene = scene.Instantiate() as Node2D;
            harvestScene.GlobalPosition = new Vector2(GlobalPosition.X, GlobalPosition.Y - foodDropHight);
            // 设置碰撞体为不可用(因为食物掉落时，依然可以与玩家产生碰撞，这就会导致仓库中食物数量会增加)
            var collisionShape2D = harvestScene.GetNode<CollisionShape2D>("CollectableComponent/CollisionShape2D");
            collisionShape2D.Disabled = true;

            GetTree().Root.AddChild(harvestScene);
            
            var targetPosition = GlobalPosition;
            var timeDelay = GD.RandRange(0.5, 2.0);
            GetTree().CreateTimer(timeDelay).Timeout += () => {
                // 轻量级的动画对象
                var tween = GetTree().CreateTween();
                // 设置动画对象位置
                tween.TweenProperty(harvestScene, "position", targetPosition, 1.0);
                // 设置动画对象缩放
                tween.TweenProperty(harvestScene, "scale", new Vector2(0.5f, 0.5f), 1.0);
                // 设置动画对象销毁
                tween.TweenCallback(Callable.From(harvestScene.QueueFree));
            };
            // 移除仓库中的物品
            InventoryManagement.RemoveCollectable(inventoryItem);

        }
    }

    public void OnFoodReceived(){
        CallDeferred(nameof(AddRewardScene));
    }
    
    public void AddRewardScene(){
        // 加载奖励场景
        foreach(var scene in outputRewardScenes){
            var rewardScene = scene.Instantiate() as Node2D;
            var rewardPosition = GetRandomPositionInCircle(rewardMarker.GlobalPosition, rewardOutputRadius);

            rewardScene.GlobalPosition = rewardPosition;
            GetTree().Root.AddChild(rewardScene);

        }
    }

    public Vector2I GetRandomPositionInCircle(Vector2 center, int radius){
        // 随机角度
        var angle = GD.Randf() * Mathf.Tau;
        // 随机距离
        var distanceFromCenter = Mathf.Sqrt(GD.Randf()) * radius;
        // 计算坐标
        var x = center.X + Mathf.Cos(angle) * distanceFromCenter;
        var y = center.Y + Mathf.Sin(angle) * distanceFromCenter;
        
        return new Vector2I((int)x, (int)y);

    }

}
