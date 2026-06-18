using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.Events;

public class TypingEffect : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    private float typingSpeed = 0.06f;
    public UnityEvent onTypingDone;  // UnityEvent untuk di-invoke saat mengetik selesai
    public UnityEvent ScriptAudio;  // AudioSource untuk mengetik
    public AudioSource NPCSound;

    private string fullText;

    private void Start()
    {
        // Ambil teks dari TextMeshPro dan assign ke fullText
        fullText = textMeshPro.text;
        textMeshPro.text = "";
        StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        // Mulai memutar audio saat mengetik dimulai
        if (ScriptAudio != null)
        {
            ScriptAudio.Invoke();
        }

        foreach (char letter in fullText.ToCharArray())
        {
            textMeshPro.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        // Hentikan audio saat mengetik selesai
        if (NPCSound != null)
        {
            NPCSound.Stop();
        }

        // Invoke UnityEvent ketika mengetik selesai
        onTypingDone?.Invoke();
    }

}
