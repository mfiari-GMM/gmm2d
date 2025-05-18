using UnityEngine;

public class AudioManager : MonoBehaviour {

    public AudioSource[] sfx;
    public AudioSource[] bgm;

    public static AudioManager instance;

    private float soundVolume = 1.0f;
    private int musicPlaying = 0;

    // Use this for initialization
    void Start () {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        } else
        {
            Destroy(this.gameObject);
        }
	}

    public void ChangeVolume(float volume)
    {
        soundVolume = volume;
        bgm[musicPlaying].volume = soundVolume;
    }

    public void PlaySFX(int soundToPlay)
    {
        if (soundToPlay < sfx.Length)
        {
            sfx[soundToPlay].Play();
            sfx[soundToPlay].volume = soundVolume;
        }
    }

    public void PlayBGM(int musicToPlay)
    {
        if (!bgm[musicToPlay].isPlaying)
        {
            StopMusic();
            this.musicPlaying = musicToPlay;

            if (musicToPlay < bgm.Length)
            {
                bgm[musicToPlay].Play();
                bgm[musicToPlay].volume = soundVolume;
            }
        }
    }

    public void StopMusic()
    {
        for(int i = 0; i < bgm.Length; i++)
        {
            bgm[i].Stop();
        }
    }
}
