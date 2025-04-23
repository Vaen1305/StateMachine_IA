using UnityEngine;
using UnityEngine.AI;

public class ToySpawner : MonoBehaviour
{
    public GameObject toyPrefab;
    public Transform jugarPoint;
    public float spawnRadius = 3f;

    private GameObject currentToy;

    public GameObject SpawnToy()
    {
        if (toyPrefab == null || jugarPoint == null) return null;

        Vector3 randomPos = jugarPoint.position + Random.insideUnitSphere * spawnRadius;
        randomPos.y = jugarPoint.position.y;

        Vector3 spawnPos = NavMeshUtils.SamplePosition(randomPos, spawnRadius);

        currentToy = Instantiate(toyPrefab, spawnPos, Quaternion.identity);
        return currentToy;
    }

    public void DestroyToy()
    {
        if (currentToy != null)
        {
            Destroy(currentToy);
            currentToy = null;
        }
    }

    public GameObject GetCurrentToy()
    {
        return currentToy;
    }
}
