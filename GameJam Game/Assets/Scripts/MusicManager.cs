using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioSource _currentSong;
    private AudioSource _nextSong = new();
    private float _fadeRate;
    private bool _changingSong = false;

    public AudioSource CurrentSong
    {
        get => _currentSong;
        set => _currentSong = value;
    }

    private void Update()
    {
        if(_currentSong.volume > 0 && _nextSong != null)
        {
            _nextSong.volume += _fadeRate * Time.deltaTime;
            _currentSong.volume -= _fadeRate * Time.deltaTime;

            if(_currentSong.volume <= 0)
            {
                _currentSong.Stop();
                _currentSong = _nextSong;
                _nextSong = null;
            }
        }
    }

    public void ChangeSong(AudioSource song, float fadeTime)
    {
        if (_currentSong == song) return;

        if(_currentSong == null)
        {
            _currentSong = song;
            return;
        }

        _nextSong = song;
        _fadeRate = _currentSong.volume / fadeTime;

        _nextSong.volume = 0;
        _nextSong.Play();
    }


}
