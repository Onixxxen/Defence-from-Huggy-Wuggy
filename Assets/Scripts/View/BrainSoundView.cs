using UnityEngine;

public class BrainSoundView : MonoBehaviour
{
    [SerializeField] private AudioSource _brainClick;

    public void PlayBrainClickSound()
    {
        _brainClick.Play();
    }
}
