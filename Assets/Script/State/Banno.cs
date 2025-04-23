// Banno.cs
using UnityEngine;

public class Banno : Humano
{
    private void Awake()
    {
        typestate = TypeState.Banno;
        LocadComponent();
    }

    public override void Execute()
    {
        if (_StateMachine.HasReachedDestination())
        {
            if (_DataAgent.WC.value > _DataAgent.WC.valueMax)
                _StateMachine.ChangeState(TypeState.Jugar);
            else
            {
                if (_DataAgent.WC.timeFrameRate > _DataAgent.WC.timeRate)
                {
                    _DataAgent.WC.timeFrameRate = 0;
                    _DataAgent.WC.value += 0.03f;
                }
                _DataAgent.WC.timeFrameRate += Time.deltaTime;
            }
        }
        base.Execute();
    }
}
