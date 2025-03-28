using Godot;
using System;

public partial class AudioPlayTimeComponent : Timer
{
    [Export]
    AudioStreamPlayer2D audioPlayerSteamPlayer2D;

    public void OnTimeOut(){
        audioPlayerSteamPlayer2D.Play();
    }

}
