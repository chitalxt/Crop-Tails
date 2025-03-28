using Godot;
using System;

public partial class GameManager : Node
{
    public static GameManager Instance { get; private set; }
    PackedScene gameMenuSceneLoader = ResourceLoader.Load<PackedScene>("res://scenes/ui/GameMenuScreen.tscn");

    public override void _Ready()
    {
        Instance = this;
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        base._UnhandledInput(@event);

        if (@event.IsActionPressed("game_menu")){
            showGameMenuScreen(); 
        }
    }
    public static void StartGame(){
        SceneManager.LoadMainSceneContainer();
        SceneManager.LoadLevel("Level1");
        SaveGameManager.LoadGame();
        SaveGameManager.Instance.allowSaveGame = true;
    }

    public static void ExitGame(){
        Instance.GetTree().Quit();
    }

    public void showGameMenuScreen(){
        var gameMenuScene = gameMenuSceneLoader.Instantiate();
        GetTree().Root.AddChild(gameMenuScene);
    }
}
