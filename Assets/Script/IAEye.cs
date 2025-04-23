using UnityEngine;

public class IAEye : MonoBehaviour
{
    public DataView dataView = new DataView();
    public Health detectedTarget;

    private float frameRate = 0f;
    private int index = 0;
    private float[] arrayRate = new float[100];

    public float MinRate = 0.1f;
    public float MaxRate = 0.3f;

    void Start()
    {
        dataView.Owner = transform;
        dataView.CreateMesh();

        for (int i = 0; i < arrayRate.Length; i++)
            arrayRate[i] = Random.Range(MinRate, MaxRate);
    }

    void Update()
    {
        if (frameRate > arrayRate[index])
        {
            frameRate = 0;
            Scan();
            index = (index + 1) % arrayRate.Length;
        }

        frameRate += Time.deltaTime;
    }

    void Scan()
    {
        detectedTarget = null;

        Collider[] hits = Physics.OverlapSphere(transform.position, dataView.distance, dataView.Scanlayers);
        float minDist = float.MaxValue;

        foreach (var hit in hits)
        {
            var health = hit.GetComponent<Health>();
            if (health != null && health.AimOffSet != null && dataView.IsInSight(health.AimOffSet))
            {
                float dist = Vector3.Distance(transform.position, health.transform.position);
                if (dist < minDist)
                {
                    detectedTarget = health;
                    minDist = dist;
                }
            }
        }

        dataView.InSight = detectedTarget != null;
    }

    private void OnValidate()
    {
        dataView.CreateMesh();
    }

    private void OnDrawGizmos()
    {
        dataView.OnDrawGizmos();
    }
}
