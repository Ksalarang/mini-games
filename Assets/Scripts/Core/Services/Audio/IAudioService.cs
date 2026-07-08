using UnityEngine;

namespace Core.Services.Audio
{
    public interface IAudioService
    {
        void PlaySound(AudioClip clip, float volume = 1f);
        void PlayMusic(AudioClip clip, float volume = 1f, bool loop = true);
        void StopMusic();
        void SetMasterVolume(float volume);
    }
}
