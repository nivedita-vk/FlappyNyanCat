using UnityEngine;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Toggle sfxToggle;
    [SerializeField] private Toggle bgmToggle;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private GameObject panelObject;

    private void Start()
    {
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        sfxToggle.onValueChanged.AddListener(OnSfxToggleChanged);
        bgmToggle.onValueChanged.AddListener(OnBgmToggleChanged);

        if(panelObject != null)
        {
            panelObject.SetActive(false);
        }
    }

    public void OpenPanel()
    {
        if(panelObject != null)
        {
            panelObject.SetActive(true);
        }

        UpdateUIFromAudioScript();
    }

    public void ClosePanel()
    {
        AudioScript.Instance.Save();

        if(panelObject != null)
        {
            panelObject.SetActive(false);
        }
    }
    private void UpdateUIFromAudioScript()
    {
        volumeSlider.SetValueWithoutNotify(AudioScript.Instance.GetVolume());
        sfxToggle.SetIsOnWithoutNotify(!AudioScript.Instance.IsSfxMuted());
        bgmToggle.SetIsOnWithoutNotify(!AudioScript.Instance.IsBgmMuted());
    }

    private void OnVolumeChanged(float value)
    {
        AudioScript.Instance.ChangeVolume(value);
    }

    private void OnSfxToggleChanged(bool isOn)
    {
        AudioScript.Instance.SfxVolumeToggle(!isOn);
    }

    private void OnBgmToggleChanged(bool isOn)
    {
        AudioScript.Instance.BgmVolumeToggle(!isOn);
    }
}
