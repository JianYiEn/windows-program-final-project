using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestMenu : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip buttonClickSound;
    public void Goback()
    {
        StartCoroutine(PlaySoundThenLoadScene(buttonClickSound, "SelectMenu"));
    }

    private IEnumerator PlaySoundThenLoadScene(AudioClip clip, string sceneName)
    {
        audioSource.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        SceneManager.LoadScene(sceneName);
    }
}
