using UnityEngine;
using UnityEngine.SceneManagement;

public class selectTest : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip buttonClickSound;
    public GameObject testPanel; // 對測試 Panel 的引用

    // 載入指定的場景
    public void LoadScene(string sceneName)
    {
        audioSource.PlayOneShot(buttonClickSound);
        SceneManager.LoadScene(sceneName);
    }

    // 關閉測試 Panel
    public void ClosePanel()
    {
        testPanel.SetActive(false);
    }
}
