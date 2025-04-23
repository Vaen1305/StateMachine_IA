using UnityEngine;

[System.Serializable]
public class DataView : DataViewBase
{
    public LayerMask occlusionlayers;
    public bool InsideObject = false;
    public bool InSight = false;
    Transform enemy;

    public override bool IsInSight(Transform target)
    {
        InSight = false;
        enemy = null;

        if (target == null || Owner == null) return false;

        Vector3 origin = Owner.position;
        Vector3 dest = target.position;
        Vector3 dir = dest - origin;

        if (dir.magnitude > distance) return false;

        if (dest.y < origin.y - height || dest.y > origin.y + height)
            return false;

        dir.y = 0;
        float deltaAngle = Vector3.Angle(dir.normalized, Owner.forward);
        if (deltaAngle > angle) return false;

        if (InsideObject && Physics.Linecast(origin, dest, occlusionlayers))
            return false;

        enemy = target;
        InSight = true;
        return true;
    }

    public override void OnDrawGizmos()
    {
        if (!IsDrawGizmo || mesh == null || Owner == null) return;

        Gizmos.color = InSight ? meshSightIn : meshSightOut;
        Gizmos.DrawMesh(mesh, Owner.position, Owner.rotation);
    }
}
