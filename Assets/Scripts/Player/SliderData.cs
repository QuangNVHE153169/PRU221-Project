using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]

public class SliderData
{
    public float value;
    public float minValue;
    public float maxValue;
    public string name;
    public string tag;
    public SliderData(Slider slider)
    {
        if (slider == null)
            return;
        value = slider.value;
        minValue = slider.minValue;
        maxValue = slider.maxValue;
        name = slider.name;
        tag = slider.tag;
    }

    internal void Slider(Slider slider)
    {
        slider.value = this.value;
        slider.minValue = this.minValue;
        slider.maxValue = this.maxValue;
        slider.name = this.name;
        slider.tag = this.tag;
    }
}
