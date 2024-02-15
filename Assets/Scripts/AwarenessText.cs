using UnityEngine;
using TMPro;
using System.Collections;

public class AwarenessText : MonoBehaviour
{
    public TMP_Text awarenessText;

    public void ChangeText(float fadeDuration, Color color, string changeAmount)
    {
        StartCoroutine(FadeText(color, changeAmount, fadeDuration));
    }

    private IEnumerator FadeText(Color color, string changeAmount, float fadeDuration)
    {
        awarenessText.color = color;
        awarenessText.text = changeAmount;
        awarenessText.alpha = 0f; // Start with zero opacity

        // Fade in
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration / 2f)
        {
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / (fadeDuration / 2f));
            awarenessText.alpha = alpha;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Wait for half of the fade duration
        yield return new WaitForSeconds(fadeDuration / 2f);

        // Set the text fully visible for 0.5 seconds
        awarenessText.alpha = 1f;
        yield return new WaitForSeconds(0.5f);

        // Fade out
        elapsedTime = 0f;
        while (elapsedTime < fadeDuration / 2f)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / (fadeDuration / 2f));
            awarenessText.alpha = alpha;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        awarenessText.alpha = 0f;
    }
}
