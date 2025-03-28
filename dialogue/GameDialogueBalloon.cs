using Godot;
using Godot.Collections;
using DialogueManagerRuntime;

public partial class GameDialogueBalloon : BaseGameDialogueBalloon
{
    // private EmotesPanel emotesPanel;

    public override void _Ready(){
        base._Ready();
        // emotesPanel = GetNode<EmotesPanel>("Balloon/Panel/Dialogue/HBoxContainer/EmotesPanel");
    }

    public override void Start(Resource dialogueResource, string title, Array<Variant> extraGameStates = null){
        base.Start(dialogueResource, title, extraGameStates);
        // emotesPanel.PlayEmote("emote_12_talking");
    }
    
    public override void Next(string nextId){
        base.Next(nextId);
        // emotesPanel.PlayEmote("emote_12_talking");
    }
        


}
