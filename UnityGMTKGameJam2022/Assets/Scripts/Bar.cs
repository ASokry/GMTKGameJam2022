using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    public float GetValue() { return (int)slider.value; }

    public void SetValue(int val)
    {
        //print("set");
        if (val <= slider.maxValue && val >= 0)
        {
            slider.value = val;
        }
    }

    public void SetMaxValue(int maxVal)
    {
        //print("set max");
        slider.maxValue = maxVal;
    }
}
