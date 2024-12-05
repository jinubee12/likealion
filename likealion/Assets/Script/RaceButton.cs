using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RaceButton : MonoBehaviour
{
    public TMP_Text text;
    public RectTransform rect;

    public void UpdatePosition(float newX)
    {
        rect.anchoredPosition = new Vector2(newX, rect.anchoredPosition.y);
    }
}
