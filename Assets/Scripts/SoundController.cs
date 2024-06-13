using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField] private AudioSource jump;
    [SerializeField] private AudioSource win;
    [SerializeField] private AudioSource lose;

    public static SoundController Instance;

    private void Start()
    {
        Instance = this;
    }

    public void SetVolume(float lvl)
    {
        jump.volume = lvl;
        win.volume = lvl;
        lose.volume = lvl;
    }

    public void Jump()
    {
        jump.Play();
    }

    public void Win()
    {
        win.Play();
    }

    public void Lose()
    {
        lose.Play();
    }
}
