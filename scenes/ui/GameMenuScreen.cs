using Godot;
using System;
using System.Collections;

public partial class GameMenuScreen : CanvasLayer
{
    Button saveGameButton;

    public override void _Ready(){
        saveGameButton = GetNode<Button>("MarginContainer/VBoxContainer/SaveGameButton");
        saveGameButton.Disabled = !SaveGameManager.Instance.allowSaveGame;

        if(SaveGameManager.Instance.allowSaveGame){
            saveGameButton.FocusMode = Control.FocusModeEnum.All;
        }
        else{
            saveGameButton.FocusMode = Control.FocusModeEnum.None;
        }
    }

    public void OnStartGameButtonPressed(){
        GameManager.StartGame();
        QueueFree(); 
    }
    public void OnSaveGameButtonPressed(){
        SaveGameManager.SaveGame();
    }

    public void OnExitGameButtonPressed(){
         GameManager.ExitGame ();
    }



}
