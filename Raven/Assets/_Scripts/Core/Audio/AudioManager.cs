using System;
using System.Linq;
using UnityEngine;

namespace Raven.Core
{
    public enum AudioNames { Idle, Inpact, Charge, Shoot, Explosion, Dead, Walk, Dash, FireDash }

    public class AudioManager
    {
        private AudioReferences _references;

        public AudioManager(AudioReferences p_references)
        {
            _references = p_references;
        }

        public void PlaySound(AudioClipConditions p_clipOptions, AudioSource p_audioSource)
        {
            p_audioSource.Stop();
            p_audioSource.loop = p_clipOptions.Loop;
            p_audioSource.clip = p_clipOptions.AudioClip;
            p_audioSource.volume = p_clipOptions.Volume;
            p_audioSource.Play();
        }

        public AudioClipConditions GetCurrenAudioClipConditions(AudioClipConditions[] p_audioClips, AudioNames p_clipName)
        {
            return p_audioClips.First(x => x.AudioName == p_clipName);
        }
    }

    [Serializable]
    public class AudioClipConditions
    {
        public AudioNames AudioName;
        public AudioClip AudioClip;
        [Range(0, 1)] public float Volume = 1;
        public bool Loop;
    }
}
