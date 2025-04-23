// Comer.cs
using UnityEngine;

public class Comer : Humano
{
    private void Awake()
    {
        typestate = TypeState.Comer;
        LocadComponent();
    }

    public override void Execute()
    {
        if (_StateMachine.HasReachedDestination())
        {
            if (_DataAgent.Energy.value > _DataAgent.Energy.valueMax)
                _StateMachine.ChangeState(TypeState.Jugar);
            else
            {
                if (_DataAgent.Energy.timeFrameRate > _DataAgent.Energy.timeRate)
                {
                    _DataAgent.Energy.timeFrameRate = 0;
                    _DataAgent.Energy.value += 0.03f;
                }
                _DataAgent.Energy.timeFrameRate += Time.deltaTime;
            }
        }
        base.Execute();
    }
}
