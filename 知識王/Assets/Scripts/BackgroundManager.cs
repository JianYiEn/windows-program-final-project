using UnityEngine;
using UnityEngine.UI;

public class BackgroundManager : MonoBehaviour
{
    public Sprite dayBackground;  // Day 主題的背景圖片
    public Sprite nightBackground; // Night 主題的背景圖片
    private Image backgroundImage;

    void Start()
    {
        backgroundImage = GetComponent<Image>();
        ApplyBackgroundTheme();
    }

    public void ApplyBackgroundTheme()
    {
        string theme = PlayerPrefs.GetString("BackgroundTheme", "Day");

        if (backgroundImage != null)
        {
            backgroundImage.sprite = (theme == "Day") ? dayBackground : nightBackground;
        }
    }
}
