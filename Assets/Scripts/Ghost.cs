using System.Collections;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public int speed = 3;
    public Transform player;
    public Material SourceInvisMat; // Материал, который ПОДДЕРЖИВАЕТ ПРОЗРАЧНОСТЬ
    public float Duration = 2f;

    private Renderer ghostRenderer;
    private Material originalMaterial; // Для хранения исходного материала
    private Material fadeInstanceMaterial; // Для хранения КОПИИ материала прозрачности
    private Coroutine fadeCor;

    void Start()
    {
        ghostRenderer = GetComponent<Renderer>();

        // КЛЮЧЕВОЙ ШАГ 1: Сохраняем исходный материал, который сейчас установлен у призрака.
        // Используем sharedMaterial, чтобы не создавать лишнюю копию в Start.
        originalMaterial = ghostRenderer.sharedMaterial;
    }

    // Этот метод вызывается Игроком, когда происходит касание (например, в OnTriggerStay)
    public void StartTransp()
    {
        if (fadeCor == null) // Проверка, чтобы не запускать несколько раз
        {
            // КЛЮЧЕВОЙ ШАГ 2: Создаем КОПИЮ прозрачного материала и назначаем ее.
            if (SourceInvisMat != null)
            {
                fadeInstanceMaterial = new Material(SourceInvisMat);
                ghostRenderer.material = fadeInstanceMaterial;

                // Запускаем процесс исчезновения
                fadeCor = StartCoroutine(Fading(Duration));
            }
        }
    }


    private IEnumerator Fading(float Duration)
    {
        speed = 0;

        float startTime = Time.time;
        float elapsed = 0f;
        Color startColor = fadeInstanceMaterial.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0f); // Полностью прозрачный

        while (elapsed < Duration)
        {
            elapsed = Time.time - startTime;
            float t = elapsed / Duration;

            // Плавное изменение цвета
            fadeInstanceMaterial.color = Color.Lerp(startColor, endColor, t);

            yield return null;
        }

        // Конечная точка
        fadeInstanceMaterial.color = endColor;

        // Уничтожение объекта после исчезновения
        Destroy(gameObject);
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= 8 && speed > 0)
        {
            // ... (логика движения)
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
            // ... (логика вращения)
        }
    }

    // Очистка памяти при уничтожении объекта
    void OnDestroy()
    {
        // Уничтожаем скопированный материал, чтобы избежать утечек памяти
        if (fadeInstanceMaterial != null)
        {
            Destroy(fadeInstanceMaterial);
        }
    }
}