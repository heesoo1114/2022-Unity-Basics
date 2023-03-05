using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetBGM : MonoBehaviour
{
    public AudioMixer _mixer;
    public Slider _slider;

    private void Start()
    {
        _slider.value = PlayerPrefs.GetFloat("BGMparameters", 0.75f);
    }

    public void SetLevel(float sliderValue)
    {
        _mixer.SetFloat("BGMparameters", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("BGMparameters", sliderValue);
    }
}
