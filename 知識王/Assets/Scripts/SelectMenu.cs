using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SelectMenu : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip buttonClickSound;
    public GameObject selectTestPanel;
    public void HomeButton()
    {
        StartCoroutine(PlaySoundThenLoadScene(buttonClickSound, "MainMenu"));
    }
    public void ReviewButton()
    {
        StartCoroutine(PlaySoundThenLoadScene(buttonClickSound, "ReviewScene"));
    }
    public void TestButton()
    {
        audioSource.PlayOneShot(buttonClickSound);

        // 顯示 selectTest Panel
        selectTestPanel.SetActive(true);
    }
    private IEnumerator PlaySoundThenLoadScene(AudioClip clip, string sceneName)
    {
        audioSource.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        SceneManager.LoadScene(sceneName);
    }
}
