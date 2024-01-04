using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        ApplySoundSettings();
    }

    public void ApplySoundSettings()
    {
        // 檢查 PlayerPrefs 獲取音效設置
        bool isSoundEnabled = PlayerPrefs.GetInt("SoundEnabled", 1) == 1;

        // 根據設置啟用或禁用 AudioSource
        if (audioSource != null)
        {
            audioSource.mute = !isSoundEnabled;
        }
    }
    public void RefreshAudioSettings()
    {
        ApplySoundSettings();
    }
}
