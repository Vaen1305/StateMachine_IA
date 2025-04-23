using UnityEngine;

public class Jugar : Humano
{
    private GameObject toyTarget;

    private void Awake()
    {
        typestate = TypeState.Jugar;
        LocadComponent();
    }

    public override void Enter()
    {
        base.Enter();
        var spawner = _StateMachine.GetToySpawner();
        toyTarget = spawner?.GetCurrentToy();
    }

    public override void Execute()
    {
        if (toyTarget == null)
        {
            var spawner = _StateMachine.GetToySpawner();
            toyTarget = spawner?.GetCurrentToy();

            if (toyTarget != null)
            {
                var agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
                NavMeshUtils.MoveToTargetPosition(agent, toyTarget.transform.position);
            }
        }

        if (_StateMachine.HasReachedDestination())
        {
            _DataAgent.DiscountEnergy();
            _DataAgent.DiscountSleep();
            _DataAgent.DiscountWC();
        }

        base.Execute();
    }

    public override void Exit()
    {
        toyTarget = null;
        base.Exit();
    }
}
