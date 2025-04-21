using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class color : MonoBehaviour
{
    private Image kartColor;

    private void Awake()
    {
        kartColor = GetComponent<Image>();
    }
    void Start()
    {
        float rColor = Random.Range(0, 255);
        float gColor = Random.Range(0, 255);
        float bColor = Random.Range(0, 255);
        Color randomColor = new Color(bColor, rColor, gColor);
        kartColor.color = randomColor;
    }
}
