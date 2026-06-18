using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectWithBaskets : MonoBehaviour
{
    [Header("Wadah GameObjects")]
    public GameObject basket1;
    public GameObject basket2;
    public GameObject basket3;

    [Header("Objek GameObjects")]
    public GameObject[] objectsToStore; // Objek yang dapat dimasukkan ke keranjang

    [Header("Events")]
    public UnityEngine.Events.UnityEvent WhenGone; // Event yang dipanggil setelah objek dihancurkan
    public UnityEngine.Events.UnityEvent WhenGoneBasket1; // Event untuk basket 1
    public UnityEngine.Events.UnityEvent WhenGoneBasket2; // Event untuk basket 2
    public UnityEngine.Events.UnityEvent WhenGoneBasket3; // Event untuk basket 3
    public UnityEngine.Events.UnityEvent WhenTouchBasket1; // Event khusus untuk basket 1
    public UnityEngine.Events.UnityEvent WhenTouchBasket2; // Event khusus untuk basket 2
    public UnityEngine.Events.UnityEvent WhenTouchBasket3; // Event khusus untuk basket 3

    // Menyimpan objek yang sudah dimasukkan ke dalam setiap keranjang
    private List<GameObject> basket1Contents = new List<GameObject>();
    private List<GameObject> basket2Contents = new List<GameObject>();
    private List<GameObject> basket3Contents = new List<GameObject>();

    void Start()
    {
        // Pastikan jumlah objek yang akan disimpan sesuai
        if (objectsToStore.Length == 0)
        {
            Debug.LogError("Tidak ada objek untuk disimpan!");
        }
    }

    void Update()
    {
        // Cek apakah semua objek sudah dihancurkan
        if (AreAllObjectsGone() && AreAllBasketsGone())
        {
            WhenGone.Invoke(); // Memanggil event WhenGone jika semua objek dan keranjang hilang
        }
    }

    // Fungsi untuk menangani ketika objek masuk ke dalam keranjang menggunakan trigger
    private void OnTriggerEnter(Collider other)
    {
        // Cek apakah objek yang menyentuh collider adalah objek yang ada dalam array objectsToStore
        for (int i = 0; i < objectsToStore.Length; i++)
        {
            if (other.gameObject == objectsToStore[i])
            {
                // Menangani objek berdasarkan keranjang yang tersentuh
                if (basket1.GetComponent<Collider>().bounds.Contains(other.transform.position))
                {
                    Destroy(other.gameObject); // Hancurkan objek asli
                    WhenTouchBasket1.Invoke(); // Panggil event hanya untuk basket 1
                    basket1Contents.Add(other.gameObject); // Menambahkan objek ke keranjang 1
                    CheckBasketFull(basket1, basket1Contents.Count, 3); // Cek apakah keranjang 1 sudah penuh
                }
                else if (basket2.GetComponent<Collider>().bounds.Contains(other.transform.position))
                {
                    Destroy(other.gameObject); // Hancurkan objek asli
                    WhenTouchBasket2.Invoke(); // Panggil event hanya untuk basket 2
                    basket2Contents.Add(other.gameObject); // Menambahkan objek ke keranjang 2
                    CheckBasketFull(basket2, basket2Contents.Count, 4); // Cek apakah keranjang 2 sudah penuh
                }
                else if (basket3.GetComponent<Collider>().bounds.Contains(other.transform.position))
                {
                    Destroy(other.gameObject); // Hancurkan objek asli
                    WhenTouchBasket3.Invoke(); // Panggil event hanya untuk basket 3
                    basket3Contents.Add(other.gameObject); // Menambahkan objek ke keranjang 3
                    CheckBasketFull(basket3, basket3Contents.Count, 2); // Cek apakah keranjang 3 sudah penuh
                }
            }
        }
    }

    // Fungsi untuk memeriksa apakah semua objek sudah hilang (tidak aktif)
    private bool AreAllObjectsGone()
    {
        foreach (GameObject obj in objectsToStore)
        {
            if (obj != null && obj.activeInHierarchy)
            {
                return false; // Ada objek yang masih aktif
            }
        }
        return true; // Semua objek sudah hilang
    }

    // Fungsi untuk memeriksa apakah semua keranjang sudah dihancurkan
    private bool AreAllBasketsGone()
    {
        return basket1 == null && basket2 == null && basket3 == null;
    }

    // Fungsi untuk mengecek apakah kapasitas keranjang sudah penuh
    private void CheckBasketFull(GameObject basket, int currentCount, int maxCapacity)
    {
        if (currentCount >= maxCapacity)
        {
            // Memanggil event WhenGone untuk keranjang yang penuh
            if (basket == basket1)
            {
                WhenGoneBasket1.Invoke(); // Memanggil event untuk basket 1
                StartCoroutine(DestroyBasketAfterDelay(basket, 1f)); // Menunggu 1 detik lalu menghancurkan keranjang 1
            }
            else if (basket == basket2)
            {
                WhenGoneBasket2.Invoke(); // Memanggil event untuk basket 2
                StartCoroutine(DestroyBasketAfterDelay(basket, 1f)); // Menunggu 1 detik lalu menghancurkan keranjang 2
            }
            else if (basket == basket3)
            {
                WhenGoneBasket3.Invoke(); // Memanggil event untuk basket 3
                StartCoroutine(DestroyBasketAfterDelay(basket, 1f)); // Menunggu 1 detik lalu menghancurkan keranjang 3
            }
        }
    }

    // Coroutine untuk menunggu 1 detik dan kemudian menghancurkan keranjang
    private IEnumerator DestroyBasketAfterDelay(GameObject basket, float delay)
    {
        yield return new WaitForSeconds(delay); // Tunggu selama delay
        Destroy(basket); // Menghancurkan gameObject keranjang setelah delay
    }
}
