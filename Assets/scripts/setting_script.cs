using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class setting_script : MonoBehaviour
{
    [Header("UI Sliders")]
    [SerializeField] private Slider bgmSlider; // 背景音乐音量控制
    [SerializeField] private Slider sfxSlider; // 音效音量控制

    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer audioMixer; // 关联的 AudioMixer

    private void Start()
    {
        // 初始化 Slider 的值
        bgmSlider.value = PlayerPrefs.GetFloat("BGMVolume", 0.75f); // 默认音量 75%
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0.75f);

        // 监听 Slider 的值变化
        bgmSlider.onValueChanged.AddListener(SetBGMVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);

        // 设置初始音量
        SetBGMVolume(bgmSlider.value);
        SetSFXVolume(sfxSlider.value);
    }

    private void SetBGMVolume(float value)
    {
        PlayerPrefs.SetFloat("BGMVolume", value); // 保存音量
        float dB = Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1)) * 20; // 防止 Log10(0) 报错
        audioMixer.SetFloat("BGMVolume", dB);
    }

    private void SetSFXVolume(float value)
    {
        PlayerPrefs.SetFloat("SFXVolume", value); // 保存音量
        float dB = Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1)) * 20; // 防止 Log10(0) 报错
        audioMixer.SetFloat("SFXVolume", dB); // 转换为分贝
    }

    private void OnDestroy()
    {
        // 移除监听器
        bgmSlider.onValueChanged.RemoveListener(SetBGMVolume);
        sfxSlider.onValueChanged.RemoveListener(SetSFXVolume);
    }
}
