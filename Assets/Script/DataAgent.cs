using UnityEngine;

[System.Serializable]
public class Data
{
    [Range(0f, 1f)]  
    public float value;
    public float valueMax = 1;
    public float time;
    public float timeRate;
    public float timeFrameRate = 0;

    public Data() { }
}

public class DataAgent : MonoBehaviour
{
    public Data Energy = new Data();
    public Data Sleep = new Data();
    public Data WC = new Data();

    public StateMachine _StateMachine;

    void Start()
    {
        _StateMachine = GetComponent<StateMachine>();
    }

    public void DiscountEnergy()
    {
        if (ShouldConsume())
        {
            if (Energy.timeFrameRate > Energy.timeRate)
            {
                Energy.timeFrameRate = 0;
                Energy.value = Mathf.Clamp(Energy.value - 0.03f, 0, Energy.valueMax);
            }
            Energy.timeFrameRate += Time.deltaTime;
        }
    }

    public void DiscountSleep()
    {
        if (ShouldConsume())
        {
            if (Sleep.timeFrameRate > Sleep.timeRate)
            {
                Sleep.timeFrameRate = 0;
                Sleep.value = Mathf.Clamp(Sleep.value - 0.03f, 0, Sleep.valueMax);
            }
            Sleep.timeFrameRate += Time.deltaTime;
        }
    }

    public void DiscountWC()
    {
        if (ShouldConsume())
        {
            if (WC.timeFrameRate > WC.timeRate)
            {
                WC.timeFrameRate = 0;
                WC.value = Mathf.Clamp(WC.value - 0.03f, 0, WC.valueMax);
            }
            WC.timeFrameRate += Time.deltaTime;
        }
    }

    public void ResetAllTimers()
    {
        Energy.timeFrameRate = 0;
        Sleep.timeFrameRate = 0;
        WC.timeFrameRate = 0;
    }

    private bool ShouldConsume()
    {
        return _StateMachine.currentState.typestate == TypeState.Jugar && 
               _StateMachine.HasReachedDestination();
    }
}