using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UICanvas : UIBase
{
    protected override void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        gameObject.SetActive(false);
    }

    public abstract void Init();

    public void ShowCanvas()
    {
        gameObject.SetActive(true);
        Init();
    }

    public void HideCanvas()
    {
        gameObject.SetActive(false);
    }
}