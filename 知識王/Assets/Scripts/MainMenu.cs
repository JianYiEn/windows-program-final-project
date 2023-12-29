using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioSource audioSource; // 參考到場景中的 AudioSource

    public AudioClip startGameClip; // 開始遊戲的音效
    public AudioClip settingClip;   // 設置的音效
    // 其他音效...

    public void StartGame()
    {
        StartCoroutine(PlaySoundThenLoadScene(startGameClip, "SelectMenu"));
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Setting()
    {
        StartCoroutine(PlaySoundThenLoadScene(settingClip, "SettingMenu"));
    }

    private IEnumerator PlaySoundThenLoadScene(AudioClip clip, string sceneName)
    {
        audioSource.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        SceneManager.LoadScene(sceneName);
    }
}
