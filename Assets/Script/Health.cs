using UnityEngine;

public enum Entity { Child, Toy }

public class Health : MonoBehaviour
{
    public Entity entity;
    public Transform AimOffSet;

    void Start()
    {
        if (AimOffSet == null)
        {
            AimOffSet = this.transform;
        }
    }
}
