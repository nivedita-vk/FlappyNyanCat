using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AudioScript : MonoBehaviour
{
    public static AudioScript Instance { get; private set; }

    private const int MUTED = 0;
    private const int UNMUTED = 1;

    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;

    [SerializeField] AudioClip[] musicTracks = new AudioClip[2];
    [SerializeField] AudioClip buttonSfx;

    private int currentTrackIndex = 0;

    private int sfxMuteValue;
    private int bgmMuteValue;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    private void Start()
    {
        Button[] buttons = FindObjectsByType<Button>(FindObjectsInactive.Include, FindObjectsSortMode.None);

        foreach(Button button in buttons)
        {
            ButtonSoundPlayer soundPlayer = button.AddComponent<ButtonSoundPlayer>(); // attaches ButtonSoundPlayer component to button and returns that component as reference to call intialize method
            soundPlayer.Initialize(sfxSource, buttonSfx);
        }
    }
    public enum Tracks
    {
        Surf = 0, RetroFunk = 1
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip == null || sfxSource == null) return;
        if (!sfxSource.mute) sfxSource.PlayOneShot(clip);
    }
    public void StopBGM() {  musicSource.Stop(); }
    public void PlayBGM() { musicSource.Play();  }
    public void PauseBGM() { musicSource.Pause(); }
    public void ResumeBGM() { musicSource.UnPause(); }
    public void SfxVolumeToggle(bool shouldMute)
    {
        sfxSource.mute = shouldMute;
        sfxMuteValue = MuteValue(sfxSource.mute);
        PlayerPrefs.SetInt("sfxMuted", sfxMuteValue);
        PlayerPrefs.Save();
    }

    public void BgmVolumeToggle(bool shouldMute)
    {
        musicSource.mute = shouldMute;
        bgmMuteValue = MuteValue(musicSource.mute);
        PlayerPrefs.SetInt("bgmMuted", bgmMuteValue);
        PlayerPrefs.Save();
    }

   public void SwitchTracks(int trackIndex)
    {
        if(trackIndex >= 0 && trackIndex < musicTracks.Length)
        {
            currentTrackIndex = trackIndex;
            musicSource.clip = musicTracks[currentTrackIndex];
            musicSource.Play();

            
            PlayerPrefs.SetInt("trackChoice", currentTrackIndex);
            PlayerPrefs.Save();

        } else
        {
            Debug.Log("Pick a valid track choice number!");
        }
    }
    public void SfxOnly() { musicSource.mute = !musicSource.mute; }

    public void ChangeVolume(float volume)
    {
        musicSource.volume = volume;
        sfxSource.volume = volume;
        PlayerPrefs.SetFloat("audioVolume", volume);
        PlayerPrefs.Save();
    }

    public void Save()
    {
        PlayerPrefs.SetInt("sfxMuted", sfxMuteValue); 
        PlayerPrefs.SetInt("bgmMuted", bgmMuteValue);
        PlayerPrefs.SetInt("trackChoice", currentTrackIndex);
        PlayerPrefs.SetFloat("audioVolume", musicSource.volume);

        PlayerPrefs.Save();
    }
    public void Load()
    {
        float savedVolume = PlayerPrefs.GetFloat("audioVolume", 1f);
        musicSource.volume = savedVolume;
        sfxSource.volume = savedVolume;

        currentTrackIndex = PlayerPrefs.GetInt("trackChoice", 0);
        SwitchTracks(currentTrackIndex);

        sfxMuteValue = PlayerPrefs.GetInt("sfxMuted", UNMUTED);
        bgmMuteValue = PlayerPrefs.GetInt("bgmMuted", UNMUTED);

        musicSource.mute = IsItMuted(bgmMuteValue);
        sfxSource.mute = IsItMuted(sfxMuteValue);
    }
    public float GetVolume() {
        if (musicSource != null)
        {
            return musicSource.volume;
        }
        return 0; 
    }
    public bool IsSfxMuted() { return sfxSource != null ? sfxSource.mute : false; }
    public bool IsBgmMuted() { return musicSource != null ? musicSource.mute : false; }
    public int GetCurrentTrack() { return currentTrackIndex; }

    private int MuteValue(bool isMuted)
    {
        return isMuted ? MUTED : UNMUTED;
    }

    private bool IsItMuted(int muteValue)
    {
        return muteValue == MUTED;
    }

    private class ButtonSoundPlayer : MonoBehaviour, IPointerDownHandler
    {
        private AudioSource audioSource;
        private AudioClip audioClip;

        public void Initialize(AudioSource source, AudioClip clip)
        {
            audioSource = source;
            audioClip = clip;
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            if (audioSource != null && audioClip != null)
            {
                audioSource.PlayOneShot(audioClip);
            }
        }
    }
}
