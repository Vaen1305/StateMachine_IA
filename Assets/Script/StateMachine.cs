using UnityEngine;
using UnityEngine.AI;

public class StateMachine : MonoBehaviour
{
    public State[] states;
    public State currentState = null;
    public TypeState Startstate;

    public Transform JugarPoint;
    public Transform ComerPoint;
    public Transform DormirPoint;
    public Transform WCPoint;

    public ToySpawner toySpawner;

    private DataAgent _dataAgent;
    private NavMeshAgent _agent;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        states = GetComponents<State>();
        _dataAgent = GetComponent<DataAgent>();
        ChangeState(Startstate);
    }

    void Update()
    {
        currentState?.Execute();
        CheckNeeds();
    }

    void CheckNeeds()
    {
        if (_dataAgent == null) return;

        if (_dataAgent.Energy.value < 0.1f)
        {
            ChangeState(TypeState.Comer);
            MoveTo(ComerPoint);
        }
        else if (_dataAgent.Sleep.value < 0.1f)
        {
            ChangeState(TypeState.Dormir);
            MoveTo(DormirPoint);
        }
        else if (_dataAgent.WC.value < 0.1f)
        {
            ChangeState(TypeState.Banno);
            MoveTo(WCPoint);
        }
    }

    void MoveTo(Transform target)
    {
        if (target != null && _agent != null)
        {
            NavMeshUtils.MoveToTargetPosition(_agent, target.position);
        }
    }

    public bool HasReachedDestination()
    {
        return !_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance;
    }

    public void ChangeState(TypeState type)
    {
        foreach (var state in states)
        {
            if (state.typestate == type)
            {
                currentState?.Exit();

                currentState = state;
                currentState.Enter();
                state.enabled = true;

                if (type == TypeState.Jugar && toySpawner != null)
                {
                    toySpawner.DestroyToy();
                    GameObject toy = toySpawner.SpawnToy();
                    if (toy != null)
                        NavMeshUtils.MoveToTargetPosition(_agent, toy.transform.position);
                    else
                        MoveTo(JugarPoint);
                }
                else if (toySpawner != null)
                {
                    toySpawner.DestroyToy();
                }
            }
            else
            {
                state.enabled = false;
            }
        }
    }

    public ToySpawner GetToySpawner()
    {
        return toySpawner;
    }
}
