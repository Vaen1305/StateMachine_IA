using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NavMeshTester : MonoBehaviour
{
    private NavMeshAgent agent;

    [Header("Destino de prueba")]
    public Transform target;

    [Header("Opciones")]
    public float range = 2f;
    public bool usarSample = true;
    public bool usarPathCheck = false;
    public bool usarRaycast = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (target == null)
        {
            Debug.LogWarning("Falta asignar un target al NavMeshTester.");
            return;
        }

        ProbarMovimiento();
    }

    public void ProbarMovimiento()
    {
        Vector3 destino = target.position;

        if (usarSample)
        {
            destino = NavMeshUtils.SamplePosition(destino, range);
        }

        if (usarRaycast)
        {
            if (NavMeshUtils.RayCast(transform.position, destino, out NavMeshHit hit))
            {
                Debug.Log("Raycast detectó un obstáculo: " + hit.position);
                destino = hit.position;
            }
        }

        if (usarPathCheck)
        {
            if (NavMeshUtils.CalculatePath(agent, destino, out NavMeshPath path))
            {
                Debug.Log("Ruta válida. Moviendo al destino.");
                agent.SetPath(path);
            }
            else
            {
                Debug.LogWarning("No se pudo calcular una ruta completa.");
            }
        }
        else
        {
            NavMeshUtils.MoveToTargetPosition(agent, destino, range);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ProbarMovimiento();
        }
    }
}
