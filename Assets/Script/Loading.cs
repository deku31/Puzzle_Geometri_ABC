using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    public string tujuan;

    [SerializeField] private float loadTime = 5.0f; // Waktu loading dalam detik
    [SerializeField] private Image loadingBar; // Referensi ke Image yang akan diisi dengan fill amount
    [Range(0,1)]
    [SerializeField] private float progress=0;
    private float currentTime = 0.0f;

    private void Update()
    {
        currentTime += Time.deltaTime;
        progress = Mathf.Clamp01(currentTime / loadTime); // Hitung progres loading dari 0.0f hingga 1.0f

        loadingBar.fillAmount = progress; // Set fill amount pada Image sesuai dengan progres loading

        if (progress == 1.0f) // Jika loading selesai
        {
            SceneManager.LoadScene(tujuan);
        }
    }
}
