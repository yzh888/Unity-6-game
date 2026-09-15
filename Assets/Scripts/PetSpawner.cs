using UnityEngine;

public class PetSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] petPrefabs;
    [SerializeField] private int maxPets = 10;
    [SerializeField] private float spawnInterval = 1.5f;
    [SerializeField] private float areaSize = 20f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float spawnHeightOffset = 0.3f;
    private float timer;
    private int currentCount;

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f && currentCount < maxPets)
        {
            SpawnOne();
            timer = spawnInterval;
        }
    }

    private void SpawnOne()
    {
        if (petPrefabs.Length == 0) return;

        GameObject prefab = petPrefabs[Random.Range(0, petPrefabs.Length)];

        float half = areaSize * 0.5f;
        Vector3 pos = transform.position + new Vector3(
            Random.Range(-half, half),
            0f,
            Random.Range(-half, half)
        );

        if (Physics.Raycast(pos + Vector3.up * 50f, Vector3.down, out RaycastHit hit, 100f, groundLayer))
        {
            pos = hit.point + Vector3.up * spawnHeightOffset;
        }

        Instantiate(prefab, pos, Quaternion.identity);
        currentCount++;
    }

    public float SpawnInterval => spawnInterval;

    public void SetSpawnInterval(float value)
    {
        spawnInterval = Mathf.Max(0.05f, value);
    }
}