using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RectTransformExtensions
{

    /// <param name="rectTransform">The RectTransform UI Element</param>
    /// <param name="deltaSize"> The Size of the RectTransform by Pixels</param>
    /// <param name="pos">Position Relative to the Screen Center</param>
    public static void SetDeltaSize_At_ScreenPos(this RectTransform rectTransform,Vector2 deltaSize,Vector2 pos)
    {
        rectTransform.sizeDelta = deltaSize;
        float width = Camera.main.pixelWidth;
        float height = Camera.main.pixelHeight;
        float x = width / 2.0f;
        float y = height / 2.0f;
        pos.x += x;
        pos.y += y;
        rectTransform.position = pos;
    }

    public static void Set_Anchor_Left_Top(this RectTransform rectTransform)
    {
        rectTransform.anchorMin = new Vector2(0, 1);
        rectTransform.anchorMax = new Vector2(0, 1);
    }

    public static void Set_Anchor_Left_Top(this RectTransform rectTransform, Vector2 pos)
    {
        rectTransform.anchorMin = new Vector2(0, 1);
        rectTransform.anchorMax = new Vector2(0, 1);
        float height = Camera.main.pixelHeight;
        float rectwidth = rectTransform.sizeDelta.x;
        float rectHeight = rectTransform.sizeDelta.y;
        pos.x += rectwidth / 2.0f;
        pos.y = height - pos.y - rectHeight / 2.0f;
        rectTransform.position = pos;
    }

    public static void Set_Anchor_Left_Top(this RectTransform rectTransform,Vector2 deltaSize, Vector2 pos)
    {
        rectTransform.sizeDelta = deltaSize;
        rectTransform.anchorMin = new Vector2(0, 1);
        rectTransform.anchorMax = new Vector2(0, 1);
        float height = Camera.main.pixelHeight;
        pos.x += deltaSize.x / 2.0f;
        pos.y = height - pos.y - deltaSize.y /2.0f;
        rectTransform.position = pos;
    }
}
