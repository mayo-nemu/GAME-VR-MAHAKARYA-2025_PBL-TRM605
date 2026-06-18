using UnityEngine;
using UnityEngine.UI;

public class ClickForUnityEvent : MonoBehaviour
{
    private Button uiButton; // Referensi ke Button yang ingin dipanggil OnClick-nya

    void Start()
    {
        // Mendapatkan komponen Button dari GameObject yang sama
        uiButton = GetComponent<Button>();
    }

    public void ClickButton()
    {
        uiButton.onClick.Invoke(); // Memanggil OnClick() dari Button
    }
}
