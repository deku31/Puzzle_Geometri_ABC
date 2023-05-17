using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private List<AudioClip> sfx;
    public AudioSource audio;
    public AudioSource bgm;
    public List<AudioClip> alfabet;
    // Start is called before the first frame update
    public void _sfx(int id)
    {
        audio.PlayOneShot(sfx[id]);
    }
    public void _alfabet(int id)
    {
        audio.PlayOneShot(alfabet[id]);
    }
}
