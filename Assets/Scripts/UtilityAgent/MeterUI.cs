using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MeterUI : MonoBehaviour
{
    public TMP_Text text;
    public Slider slider;

    public Vector3 worldPosition
    {
        set
        {
            Vector2 viewportPoint = Camera.main.WorldToViewportPoint(value);
            GetComponent<RectTransform>().anchorMin = viewportPoint;
            GetComponent<RectTransform>().anchorMax = viewportPoint;
        }
    }
}
