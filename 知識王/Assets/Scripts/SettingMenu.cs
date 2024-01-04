using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SettingMenu : MonoBehaviour
{
    public Button soundButton;
    private bool isSoundEnabled = true;
    public AudioSource audioSource;
    public AudioClip buttonClickSound;
    void Start()
    {
        isSoundEnabled = PlayerPrefs.GetInt("SoundEnabled", 1) == 1; // 首先讀取設置

        UpdateButtonVisual(); // 然後更新 UI

        soundButton.onClick.AddListener(ToggleSound); // 添加點擊事件監聽器
    }
    public void Goback()
    {
        StartCoroutine(PlaySoundThenLoadScene(buttonClickSound, "MainMenu"));
    }

    private IEnumerator PlaySoundThenLoadScene(AudioClip clip, string sceneName)
    {
        audioSource.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        SceneManager.LoadScene(sceneName);
    }
    void ToggleSound()
    {
        isSoundEnabled = !isSoundEnabled;
        PlayerPrefs.SetInt("SoundEnabled", isSoundEnabled ? 1 : 0);
        PlayerPrefs.Save();

        UpdateButtonVisual();

        // 找到場景中的所有 AudioManager 並刷新設置
        foreach (var audioManager in FindObjectsOfType<AudioManager>())
        {
            audioManager.RefreshAudioSettings();
        }
    }

    void UpdateButtonVisual()
    {
        TextMeshProUGUI textMesh = soundButton.GetComponentInChildren<TextMeshProUGUI>();
        if (textMesh != null)
        {
            textMesh.text = isSoundEnabled ? "ON" : "OFF";
        }
    }
}

