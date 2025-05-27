using System.Collections;
using UnityEngine;

public class SpriteFade : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private float duration = 0.3f;
    public Player player;

    private Coroutine fadeCoroutine;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        Color initialColor = spriteRenderer.color;
        initialColor.a = 1f;
        spriteRenderer.color = initialColor;
    }

    private void Update()
    {
        if (player.hunger <= 60 && fadeCoroutine == null)
        {
            fadeCoroutine = StartCoroutine(FadeLoop());
        }
        else if (player.hunger > 60 && fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
            fadeCoroutine = null;

            Color clearColor = spriteRenderer.color;
            clearColor.a = 1f;
            spriteRenderer.color = clearColor;
        }
    }

    private IEnumerator FadeLoop()
    {
        while (player.hunger <= 60)
        {
            yield return StartCoroutine(FadeIcon());
        }

        fadeCoroutine = null;
    }

    private IEnumerator FadeIcon()
    {
        float halfDuration = duration / 2f;
        float time = 0f;

        // Fade in
        while (time < halfDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, time / halfDuration);
            Color newColor = spriteRenderer.color;
            newColor.a = alpha;
            spriteRenderer.color = newColor;

            time += Time.deltaTime;
            yield return null;
        }

        // Hold fully visible
        Color fullColor = spriteRenderer.color;
        fullColor.a = 1f;
        spriteRenderer.color = fullColor;

        time = 0f;

        // Fade out
        while (time < halfDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, time / halfDuration);
            Color newColor = spriteRenderer.color;
            newColor.a = alpha;
            spriteRenderer.color = newColor;

            time += Time.deltaTime;
            yield return null;
        }

        // Ensure fully transparent
        Color clearColor = spriteRenderer.color;
        clearColor.a = 0f;
        spriteRenderer.color = clearColor;
    }
}
