using Godot;
using Godot.Collections;

public partial class SaveGameDataResource : Resource
{
    /* saveDataNodes 用于保存所有需要保存的节点数据 */
    [Export]
    public Array<NodeDataResource> saveDataNodes = new();
     
}
