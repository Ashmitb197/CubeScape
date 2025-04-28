using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;


public class OptionGraphics : MonoBehaviour
{

    public TMP_Dropdown graphicsDropdown;
    [SerializeField] Slider masterVol;
    [SerializeField] Slider SensitivitySlider;
    public AudioMixer mainAudioMixer;
    public bool isMouseInverted;
    [Range(0.5f,5)]public float Sensitivity;
    public Text sensitivityText;

    public Toggle inverseToggle;

    void Awake()
    {

        //initialize sensitivityslider
        //inverseToggle = this.transform.Find("Toggle").GetComponent<Toggle>();

        // SensitivitySlider = this.transform.Find("Sensitivity_Slider").GetComponent<Slider>();
        // sensitivityText = SensitivitySlider.gameObject.transform.Find("SensitivityText").GetComponent<Text>();

        
    }

    void Start()
    {
        //this.gameObject.SetActive(false);
        //Cursor.visible = true;
    }

    public void ChangeGraphicQuality()
    {
        QualitySettings.SetQualityLevel(graphicsDropdown.value);
    }

    public void ChangeMasterVolume()
    {
        AudioListener.volume = masterVol.value;
    }

    // public void ChangeSensitivity()
    // {
    //     Sensitivity = SensitivitySlider.value;
    // }


    public void InvertMouse()
    {
        isMouseInverted = inverseToggle.isOn;
        //return false;
    }

    void Update()
    {
        InvertMouse();
        Debug.Log(isMouseInverted);
        //sensitivityText.text = (Math.Round(Sensitivity,2)).ToString();
    }
}
