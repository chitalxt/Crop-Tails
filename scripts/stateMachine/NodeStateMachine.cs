using Godot;
using Godot.Collections;

public partial class NodeStateMachine : Node
{
	// 初始状态
    [Export] 
	public NodeState initialNodeState;

	// 存储所有状态
    private Dictionary<string, NodeState> nodeStates = new Dictionary<string, NodeState>();

	// 当前状态
    private NodeState _currentNodeState;
    private string _currentNodeStateName;

    private string _parentNodeStateName;

    public override void _Ready()
    {
        _parentNodeStateName = this.GetParent().Name;
		// 遍历子节点
        foreach (NodeState child in GetChildren()){
            if(child is NodeState){
                // 添加到集合中
                nodeStates[child.Name.ToString().ToLower()] = child;
                // 连接信号与事件
                child.Connect("Transition", new Callable(this, nameof(TransitionTo)));
                
            }
        }

        if (initialNodeState != null){
            // 进入初始状态
            initialNodeState._EnterTree();
            _currentNodeState = initialNodeState;
            _currentNodeStateName = initialNodeState.Name.ToString().ToLower();
        }
    }

    public override void _Process(double delta){
        if (_currentNodeState != null){
            _currentNodeState._Process(delta);
        }
    }

    public override void _PhysicsProcess(double delta){
        if (_currentNodeState != null){
            _currentNodeState._PhysicsProcess(delta);
            // 处理状态转换
            _currentNodeState.OnNextTransitions();
        }
    }

    private void TransitionTo(string nodeStateName){
        // 要转变的状态名称
        string lowerName = nodeStateName.ToLower();

        if (lowerName == _currentNodeStateName)
            return;

        if (nodeStates.TryGetValue(lowerName, out NodeState newState)){
            if (_currentNodeState != null){
                // 结束当前状态
                _currentNodeState._ExitTree();
            }
            // 设置新状态
            newState._EnterTree();
            _currentNodeState = newState;
            _currentNodeStateName = newState.Name.ToString().ToLower();
            
            // GD.Print(_parentNodeStateName , $" Current State: {_currentNodeStateName}");

        }
    }
}