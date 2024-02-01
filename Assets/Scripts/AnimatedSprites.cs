using UnityEngine;

public class AnimatedSprite : MonoBehaviour
{
    public Sprite[] sprites;          // Массив спрайтов для анимации
    private SpriteRenderer spriteRenderer;  // Ссылка на компонент SpriteRenderer для управления спрайтом
    private int frame;                 // Текущий кадр анимации

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();  // Получаем компонент SpriteRenderer при старте объекта
    }

    private void OnEnable()
    {
        // Вызываем метод Animate с задержкой 0 секунд для запуска анимации при включении объекта
        Invoke(nameof(Animate), 0f);
    }

    private void OnDisable()
    {
        // Отменяем все вызовы метода Animate при отключении объекта
        CancelInvoke();
    }

    private void Animate()
    {
        frame++;  // Увеличиваем номер текущего кадра

        // Проверяем, если номер текущего кадра превышает количество спрайтов в массиве, сбрасываем его
        if (frame >= sprites.Length)
        {
            frame = 0;
        }

        // Проверяем, находится ли номер текущего кадра в допустимом диапазоне массива спрайтов
        if (frame >= 0 && frame < sprites.Length)
        {
            // Устанавливаем спрайт текущего кадра
            spriteRenderer.sprite = sprites[frame];
        }

        // Запускаем метод Animate снова с интервалом, зависящим от скорости игры из GameManager
        Invoke(nameof(Animate), 1f / GameManager.Instance.gameSpeed);
    }
}