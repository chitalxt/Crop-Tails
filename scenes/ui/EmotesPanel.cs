using Godot;


public partial class EmotesPanel : Panel
{   
    private AnimatedSprite2D animatedSprite2D;
    private Timer emoteIdleTimer;

    public Godot.Collections.Array<string> emotes = new(){"emote_1_idle", "emote_2_smile", "emote_3_ear_wave", "emote_4_blink"};

    public override void _Ready(){
        emoteIdleTimer = GetNode<Timer>("EmoteIdleTimer");
        animatedSprite2D = GetNode<AnimatedSprite2D>("Emote/AnimatedSprite2D");
        animatedSprite2D.Play("emote_1_idle");

        InventoryManagement.Instance.Connect("InventoryChanged", new Callable(this, nameof(OnInventoryChanged)));
        GameDialogueManager.Instance.Connect("FeedTheAnimals", new Callable(this, nameof(OnFeedTheAnimals)));
    }

    public void PlayEmote(string animation){
        animatedSprite2D.Play(animation);
    }

    public void OnEmoteIdleTimerTimeout(){
        var index = GD.RandRange(0,3);
        var emote = emotes[index];
        
        animatedSprite2D.Play(emote);
    }

    public void OnInventoryChanged(){
        // NOTE: 这里报错应该是因为用+=绑定事件，但是+=有一个bug，他不会自己释放不用的信号，可以重写dispose方法释放信号
        // 处理方式参考：https://forum.godotengine.org/t/c-event-handlers-triggering-unhandled-exception-system-objectdisposedexception-cannot-access-a-disposed-object/17794/2
        animatedSprite2D.Play("emote_7_excited");
    }

    public void OnFeedTheAnimals(){
        animatedSprite2D.Play("emote_6_love_kiss");
    }
}
