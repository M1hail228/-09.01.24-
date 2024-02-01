using UnityEngine;

public class Ground : MonoBehaviour
{
    private MeshRenderer meshRenderer;  // Ссылка на компонент MeshRenderer объекта

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();  // Получаем компонент MeshRenderer при старте объекта
    }

    private void Update()
    {
        // Получаем текущую скорость игры из GameManager и применяем к скорости текстуры земли
        float speed = GameManager.Instance.gameSpeed / transform.localScale.x;

        // Обновляем смещение текстуры по оси X с учетом скорости и времени
        meshRenderer.material.mainTextureOffset += speed * Time.deltaTime * Vector2.right;
    }
}

