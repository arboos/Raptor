using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SliderChange : MonoBehaviour
{
    public AudioMixer mixer;
    private Slider slider;
    [SerializeField] private string nameParameter;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    public void OnSliderValueChanged()
    {
        float va = slider.value * 50f - 40f;
        if (slider.value <= 0.05f) va = -80f;
        mixer.SetFloat(nameParameter, va);
    }
}
