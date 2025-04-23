// Humano.cs
using UnityEngine;

public class Humano : State
{
    public DataAgent _DataAgent;

    public override void LocadComponent()
    {
        base.LocadComponent();
        _DataAgent = GetComponent<DataAgent>();
    }
}
