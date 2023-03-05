using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetFX : MonoBehaviour
{
    public AudioMixer _mixer;
    public Slider _slider;

    private void Start()
    {
        _slider.value = PlayerPrefs.GetFloat("FXparameters", 0.75f);
    }

    public void SetLevel(float sliderValue)
    {
        _mixer.SetFloat("FXparameters", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("FXparameters", sliderValue);
    }
}
