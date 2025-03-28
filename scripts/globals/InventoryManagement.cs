using Godot;
using System;
using System.Collections.Generic;

public partial class InventoryManagement : Node
{
    /* 仓库管理器 */
    // 单例模式
    public static InventoryManagement Instance { get; private set; }

    [Signal]
    public delegate void InventoryChangedEventHandler();

    public Dictionary<string, int> Inventory = new Dictionary<string, int>();

    public override void _Ready()
    {
        Instance = this;
    }

    public static void AddCollectable(string collectableName){
        if(Instance.Inventory.ContainsKey(collectableName)){
            Instance.Inventory[collectableName]++;
        }
        else{
            Instance.Inventory.Add(collectableName, 1);
        }
        GD.Print("addCollectable " + collectableName);
        Instance.EmitSignal("InventoryChanged"); 
    }

    public static void RemoveCollectable(string collectableName){
        if(!Instance.Inventory.ContainsKey(collectableName)){
            Instance.Inventory[collectableName] = 0;
        }
        else{
            if(Instance.Inventory[collectableName] > 0){
                Instance.Inventory[collectableName]--;
            }
        }
        GD.Print("RemoveCollectable " + collectableName);
        Instance.EmitSignal("InventoryChanged"); 
    }
}
