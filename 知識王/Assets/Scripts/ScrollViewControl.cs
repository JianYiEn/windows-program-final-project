using UnityEngine;
using UnityEngine.UI;

public class ScrollViewControl : MonoBehaviour
{
    public ScrollRect scrollRect;

    public void SetScrollPosition(float position)
    {
        scrollRect.horizontalNormalizedPosition = position;
    }
}
