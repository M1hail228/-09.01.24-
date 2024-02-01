using UnityEngine;

public class Spawner : MonoBehaviour
{
    [System.Serializable]
    // ��������� ��� �������� ���������� �� ��������, ������� ����� ���� �������
    public struct SpawnableObject
    {
        // ������ �������
        public GameObject prefab;
        // ����������� ��������� ������� (� �������� �� 0 �� 1)
        [Range(0f, 1f)]
        public float spawnChance;
    }

    // ������ ��������, ������� ����� ���� �������
    public SpawnableObject[] objects;

    // ����������� ������� ��������� ��������
    public float minSpawnRate = 1f;

    // ������������ ������� ��������� ��������
    public float maxSpawnRate = 2f;

    // ���������� ��� ��������� �������
    private void OnEnable()
    {
        // ��������� ����� Spawn ����� ��������� �������� �������
        Invoke(nameof(Spawn), Random.Range(minSpawnRate, maxSpawnRate));
    }

    // ���������� ��� ����������� �������
    private void OnDisable()
    {
        // �������� ��� ������ ������ Spawn
        CancelInvoke();
    }

    // ����� ��� �������� ��������
    private void Spawn()
    {
        // ���������� ��������� �������� ��� ����������� �������� �� ������
        float spawnChance = Random.value;

        // �������� �� ������� ��������
        foreach (var obj in objects)
        {
            // ���� ��������� �������� ������ ����������� ��������� �������
            if (spawnChance < obj.spawnChance)
            {
                // ������� ��������� �������
                GameObject obstacle = Instantiate(obj.prefab);
                // ������������� ������� ������� ������������ ������� ��������
                obstacle.transform.position += transform.position;
                // ��������� ����, ����� ������� ������ ���� ������
                break;
            }

            // ��������� ����������� ��������� �������
            spawnChance -= obj.spawnChance;
        }

        // ��������� ����� Spawn ����� ����� ��������� �������� �������
        Invoke(nameof(Spawn), Random.Range(minSpawnRate, maxSpawnRate));
    }
}
