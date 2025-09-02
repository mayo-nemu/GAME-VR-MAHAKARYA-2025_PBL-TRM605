using System.Collections;
using UnityEngine;

public class DestroyOnShrink : MonoBehaviour
{
    [Header("Shrink Settings")]
    public float shrinkDuration = 0.3f;
    public AnimationCurve shrinkCurve;

    private bool isShrinking = false;

    // Fungsi yang dipanggil dari UnityEvent, bisa langsung dipilih di dropdown
    public void Destroy()
    {
        if (!isShrinking)
        {
            StartCoroutine(ShrinkAndDestroy());
        }
    }

    private IEnumerator ShrinkAndDestroy()
    {
        isShrinking = true;

        Vector3 originalScale = transform.localScale;
        float timer = 0f;

        while (timer < shrinkDuration)
        {
            float t = timer / shrinkDuration;

            float scaleValue = 1f - t;
            if (shrinkCurve != null && shrinkCurve.length > 0)
            {
                scaleValue = shrinkCurve.Evaluate(t);
            }

            transform.localScale = originalScale * scaleValue;
            timer += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }
}