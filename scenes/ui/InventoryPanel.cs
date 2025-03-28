 using Godot;
using System;

public partial class InventoryPanel : PanelContainer
{
    private Label LogLabel;
    private Label StoneLabel;
    private Label CornLabel;
    private Label TomatoLabel;
    private Label EggLabel;
    private Label MilkLabel;

    public override void _Ready(){
        LogLabel = GetNode<Label>("MarginContainer/VBoxContainer/Logs/LogLabel");
        StoneLabel = GetNode<Label>("MarginContainer/VBoxContainer/Stone/StoneLabel");
        CornLabel = GetNode<Label>("MarginContainer/VBoxContainer/Corn/CornLabel");
        TomatoLabel = GetNode<Label>("MarginContainer/VBoxContainer/Tomato/TomatoLabel");
        EggLabel = GetNode<Label>("MarginContainer/VBoxContainer/Egg/EggLabel");
        MilkLabel = GetNode<Label>("MarginContainer/VBoxContainer/Milk/MilkLabel");

        InventoryManagement.Instance.Connect("InventoryChanged", new Callable(this, nameof(OnInventoryChanged)));
    }

    public void OnInventoryChanged(){
        var inventory  = InventoryManagement.Instance.Inventory;

        if(inventory.ContainsKey("log")){
            LogLabel.Text = inventory["log"].ToString();
        }
        if(inventory.ContainsKey("stone")){
            StoneLabel.Text = inventory["stone"].ToString();
        }
        if(inventory.ContainsKey("corn")){
            CornLabel.Text = inventory["corn"].ToString();
        }
        if(inventory.ContainsKey("tomato")){
            TomatoLabel.Text = inventory["tomato"].ToString();
        }
        if(inventory.ContainsKey("egg")){
            EggLabel.Text = inventory["egg"].ToString();
        }
        if(inventory.ContainsKey("milk")){
            MilkLabel.Text = inventory["milk"].ToString();
        }

    }
}
