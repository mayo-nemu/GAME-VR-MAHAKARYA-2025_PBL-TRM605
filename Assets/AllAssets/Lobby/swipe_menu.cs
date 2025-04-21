using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class swipe_menu : MonoBehaviour
{
    public GameObject scrollbar;
    float scroll_pos = 0;
    public Button[] Maps; // Array untuk menyimpan game object map
    int selectedMapIndex = 0; // Indeks map yang dipilih
    float[]pos;
    
    //Variable tombol
    int posisi = 0;

    void Start()
    {
        // Mendapatkan semua komponen Button dari anak-anak game object
        Maps = GetComponentsInChildren<Button>();

        // Inisialisasi posisi
        pos = new float[Maps.Length];
        float distance = 1f / (pos.Length - 1f);
        for (int i = 0; i < pos.Length; i++) {
            pos[i] = distance * i;
        }
    }

    //Function Button Slide
    public void next()
    {
        if (posisi < pos.Length - 1)
        {
            posisi += 1;
            scroll_pos = pos[posisi];
            selectedMapIndex = posisi; // Mengupdate indeks map yang dipilih
        }
    }

    public void prev()
    {
        if (posisi > 0)
        {
            posisi -= 1;
            scroll_pos = pos[posisi];
            selectedMapIndex = posisi; // Mengupdate indeks map yang dipilih
        }
    }

    public void SelectMap()
    {
        // Memanggil onClick dari map yang dipilih
        Maps[selectedMapIndex].onClick.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        // Pastikan pos sudah diinisialisasi di Start() dan tidak null
        if (pos == null || pos.Length == 0) return;

        // Hitung ulang jarak antar posisi
        float distance = 1f / (pos.Length - 1f);

        if (Input.GetMouseButton(0)) {
            scroll_pos = scrollbar.GetComponent<Scrollbar>().value;
        } else {
            for (int i = 0; i < pos.Length; i++) {
                if (scroll_pos < pos[i] + (distance / 2) && scroll_pos > pos[i] - (distance / 2)) {
                    scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, pos[i], 0.05f);
                    posisi = i;
                }
            }
        }

        for (int i = 0; i < pos.Length; i++) {
            if (scroll_pos < pos[i] + (distance / 2) && scroll_pos > pos[i] - (distance / 2)) {
                transform.GetChild(i).localScale = Vector2.Lerp(transform.GetChild(i).localScale, new Vector2(1f, 1f), 0.05f);
                for (int a = 0; a < pos.Length; a++) {
                    if (a != i) {
                        transform.GetChild(a).localScale = Vector2.Lerp(transform.GetChild(a).localScale, new Vector2(0.8f, 0.8f), 0.05f);
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) {
            prev();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) {
            next();
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            SelectMap();
        }
        else if (Input.GetKeyDown(KeyCode.Return)) {
            BackToMainMenu();
        }
    }

    public void BackToMainMenu()
    {
        // Kode untuk kembali ke scene MainMenu
        SceneManager.LoadScene("MainMenu"); // Pastikan bahwa Anda sudah mengimpor namespace UnityEngine.SceneManagement
    }
}