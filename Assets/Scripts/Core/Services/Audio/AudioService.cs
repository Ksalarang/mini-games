using UnityEngine;

namespace Core.Services.Audio
{
    public class AudioService : IAudioService
    {
        private readonly AudioSource sfxSource;
        private readonly AudioSource musicSource;
        private float masterVolume = 1f;

        public AudioService(AudioSource sfxSource, AudioSource musicSource)
        {
            this.sfxSource = sfxSource;
            this.musicSource = musicSource;
        }

        public void PlaySound(AudioClip clip, float volume = 1f)
        {
            sfxSource.PlayOneShot(clip, volume * masterVolume);
        }

        public void PlayMusic(AudioClip clip, float volume = 1f, bool loop = true)
        {
            musicSource.clip = clip;
            musicSource.loop = loop;
            musicSource.volume = volume * masterVolume;
            musicSource.Play();
        }

        public void StopMusic()
        {
            musicSource.Stop();
        }

        public void SetMasterVolume(float volume)
        {
            masterVolume = Mathf.Clamp01(volume);
            musicSource.volume = masterVolume;
        }
    }
}
