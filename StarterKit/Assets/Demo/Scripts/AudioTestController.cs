using UnityEngine;
using UnityEngine.Audio;

public class AudioTestController : MonoBehaviour
{
    [SerializeField]
    private AudioClip _introLine = null;

    [SerializeField]
    private AudioClip _test = null;

    private AudioSource _audioSource = null;

    private bool _startTriggered = false;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        if(_audioSource != null)
        {
            _audioSource.loop = false;
            _audioSource.playOnAwake = false;
            _audioSource.Stop();
        }
    }

    void Update()
    {
        if(_audioSource != null)
        {
            if (Mox.Sync.Input.KeyDown(Mox.Sync.Input.KEYCODE.T)
            && _audioSource.isPlaying == false
            && _startTriggered == false)
            {
                _audioSource.generator = _introLine;
                _audioSource.Play();
                _startTriggered = true;
            }
            else if (_startTriggered == true && _audioSource.isPlaying == false)
            {
                _startTriggered = false;
                _audioSource.generator = _test;
                _audioSource.Play();
            }
        }
    }
}
