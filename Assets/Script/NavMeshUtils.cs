using UnityEngine;
using UnityEngine.AI;

public static class NavMeshUtils
{
    public static Vector3 SamplePosition(Vector3 targetPos, float range = 2f)
    {
        if (NavMesh.SamplePosition(targetPos, out NavMeshHit hit, range, NavMesh.AllAreas))
        {
            return hit.position;
        }
        return targetPos;
    }

    public static bool MoveToTargetPosition(NavMeshAgent agent, Vector3 targetPos, float range = 2f)
    {
        Vector3 sampledPos = SamplePosition(targetPos, range);
        return agent.SetDestination(sampledPos);
    }

    public static bool CalculatePath(NavMeshAgent agent, Vector3 targetPos, out NavMeshPath path)
    {
        path = new NavMeshPath();
        return agent.CalculatePath(targetPos, path) && path.status == NavMeshPathStatus.PathComplete;
    }

    public static bool FindClosestEdge(Vector3 position, out NavMeshHit hit)
    {
        return NavMesh.FindClosestEdge(position, out hit, NavMesh.AllAreas);
    }

    public static bool RayCast(Vector3 start, Vector3 end, out NavMeshHit hit)
    {
        return NavMesh.Raycast(start, end, out hit, NavMesh.AllAreas);
    }
}
