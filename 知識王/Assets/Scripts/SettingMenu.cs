using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SettingMenu : MonoBehaviour
{
    public Button soundButton;
    public Button themeButton; // 新增背景主題切換按鈕
    private bool isSoundEnabled = true;
    private bool isDayTheme = true; // 預設主題為 Day
    public AudioSource audioSource;
    public AudioClip buttonClickSound;

    void Start()
    {
        // 讀取音效設置
        isSoundEnabled = PlayerPrefs.GetInt("SoundEnabled", 1) == 1;

        // 讀取主題設置
        isDayTheme = PlayerPrefs.GetString("BackgroundTheme", "Day") == "Day";

        UpdateButtonVisuals(); // 更新所有按鈕的顯示

        // 為按鈕添加點擊事件監聽器
        soundButton.onClick.AddListener(ToggleSound);
        themeButton.onClick.AddListener(ToggleTheme);
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
        UpdateButtonVisuals();

        // 立即更新當前場景中的音效設置
        AudioManager audioManager = FindObjectOfType<AudioManager>();
        if (audioManager != null)
        {
            audioManager.RefreshAudioSettings();
        }
        audioSource.PlayOneShot(buttonClickSound);
    }


    void ToggleTheme()
    {
        isDayTheme = !isDayTheme;
        string theme = isDayTheme ? "Day" : "Night";
        PlayerPrefs.SetString("BackgroundTheme", theme);
        PlayerPrefs.Save();
        UpdateButtonVisuals(); // 更新按鈕顯示

        // 更新背景主題（如果有 BackgroundManager）
        BackgroundManager backgroundManager = FindObjectOfType<BackgroundManager>();
        if (backgroundManager != null)
        {
            backgroundManager.ApplyBackgroundTheme();
        }
        audioSource.PlayOneShot(buttonClickSound);
    }

    void UpdateButtonVisuals()
    {
        // 更新音效按鈕的文字
        TextMeshProUGUI soundTextMesh = soundButton.GetComponentInChildren<TextMeshProUGUI>();
        if (soundTextMesh != null)
        {
            soundTextMesh.text = isSoundEnabled ? "ON" : "OFF";
        }

        // 更新主題按鈕的文字
        TextMeshProUGUI themeTextMesh = themeButton.GetComponentInChildren<TextMeshProUGUI>();
        if (themeTextMesh != null)
        {
            themeTextMesh.text = isDayTheme ? "淺色" : "深色";
        }
    }
}
