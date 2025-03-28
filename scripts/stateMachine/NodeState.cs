using Godot;

// 所有状态节点的基类
public partial class NodeState : Node
{   

    // 自定义信号，发送状态改变相关信息
    [Signal]
    public delegate void TransitionEventHandler();

    public override void _Process(double delta){

    }

    public override void _PhysicsProcess(double delta){
        
    }

    public virtual void OnNextTransitions(){

    }
    
    public override void _EnterTree(){
        
    }

    public override void _ExitTree(){

    }
}
