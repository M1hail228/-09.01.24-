using UnityEngine;

public class Spawner : MonoBehaviour
{
    [System.Serializable]
    // Структура для хранения информации об объектах, которые могут быть созданы
    public struct SpawnableObject
    {
        // Префаб объекта
        public GameObject prefab;
        // Вероятность появления объекта (в пределах от 0 до 1)
        [Range(0f, 1f)]
        public float spawnChance;
    }

    // Массив объектов, которые могут быть созданы
    public SpawnableObject[] objects;

    // Минимальная частота появления объектов
    public float minSpawnRate = 1f;

    // Максимальная частота появления объектов
    public float maxSpawnRate = 2f;

    // Вызывается при активации объекта
    private void OnEnable()
    {
        // Запускаем метод Spawn через случайный интервал времени
        Invoke(nameof(Spawn), Random.Range(minSpawnRate, maxSpawnRate));
    }

    // Вызывается при деактивации объекта
    private void OnDisable()
    {
        // Отменяем все вызовы метода Spawn
        CancelInvoke();
    }

    // Метод для создания объектов
    private void Spawn()
    {
        // Генерируем случайное значение для определения появится ли объект
        float spawnChance = Random.value;

        // Проходим по массиву объектов
        foreach (var obj in objects)
        {
            // Если случайное значение меньше вероятности появления объекта
            if (spawnChance < obj.spawnChance)
            {
                // Создаем экземпляр объекта
                GameObject obstacle = Instantiate(obj.prefab);
                // Устанавливаем позицию объекта относительно позиции спаунера
                obstacle.transform.position += transform.position;
                // Прерываем цикл, чтобы создать только один объект
                break;
            }

            // Уменьшаем вероятность появления объекта
            spawnChance -= obj.spawnChance;
        }

        // Запускаем метод Spawn через новый случайный интервал времени
        Invoke(nameof(Spawn), Random.Range(minSpawnRate, maxSpawnRate));
    }
}
