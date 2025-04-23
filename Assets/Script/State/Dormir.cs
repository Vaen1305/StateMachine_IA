// Dormir.cs
using UnityEngine;

public class Dormir : Humano
{
    private void Awake()
    {
        typestate = TypeState.Dormir;
        LocadComponent();
    }

    public override void Execute()
    {
        if (_StateMachine.HasReachedDestination())
        {
            if (_DataAgent.Sleep.value > _DataAgent.Sleep.valueMax)
                _StateMachine.ChangeState(TypeState.Jugar);
            else
            {
                if (_DataAgent.Sleep.timeFrameRate > _DataAgent.Sleep.timeRate)
                {
                    _DataAgent.Sleep.timeFrameRate = 0;
                    _DataAgent.Sleep.value += 0.03f;
                }
                _DataAgent.Sleep.timeFrameRate += Time.deltaTime;
            }
        }
        base.Execute();
    }
}
