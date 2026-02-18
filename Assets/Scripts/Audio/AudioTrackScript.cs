using UnityEngine;

public class AudioTrackScript : MonoBehaviour
{
    public void PlaySurf()
    {
        AudioScript.Instance.SwitchTracks((int)AudioScript.Tracks.Surf);
    }
    public void PlayRetroFunk()
    {
        AudioScript.Instance.SwitchTracks((int)AudioScript.Tracks.RetroFunk);
    }
}
