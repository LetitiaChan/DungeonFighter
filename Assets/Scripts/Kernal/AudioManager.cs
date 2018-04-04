using UnityEngine;
using System.Collections.Generic;

namespace Kernal
{
    public class AudioManager : MonoBehaviour
    {
        #region Unity Inspector Fields
        public AudioClip[] AudioClipArray;
        #endregion

        private static float _AudioBackgroundVolumn = 1f;
        private static float _AudioEffectVolumn = 1f;
        private static Dictionary<string, AudioClip> _AudioClipLib;
        private static AudioSource[] _AudioSourceArray;
        private static AudioSource _AudioSource_Background;
        private static AudioSource _AudioSource_EffectA;
        private static AudioSource _AudioSource_EffectB;

        public static float AudioBackgroundVolumn
        {
            get
            {
                return _AudioBackgroundVolumn;
            }

            set
            {
                _AudioBackgroundVolumn = Mathf.Clamp01(value);
                if (_AudioSource_Background != null)
                    _AudioSource_Background.volume = _AudioBackgroundVolumn;
                PlayerPrefs.SetFloat("AudioBackgroundVolumn", _AudioBackgroundVolumn);
            }
        }

        public static float AudioEffectVolumn
        {
            get
            {
                return _AudioEffectVolumn;
            }

            set
            {
                _AudioEffectVolumn = Mathf.Clamp01(value);
                if (_AudioSource_EffectA != null)
                    _AudioSource_EffectA.volume = _AudioEffectVolumn;
                if (_AudioSource_EffectB != null)
                    _AudioSource_EffectB.volume = _AudioEffectVolumn;
                PlayerPrefs.SetFloat("AudioEffectVolumn", _AudioEffectVolumn);
            }
        }

        void Awake()
        {
            _AudioClipLib = new Dictionary<string, AudioClip>();
            foreach (AudioClip audioClip in AudioClipArray)
            {
                _AudioClipLib.Add(audioClip.name, audioClip);
            }

            _AudioSourceArray = this.GetComponents<AudioSource>();
            _AudioSource_Background = _AudioSourceArray[0];
            _AudioSource_EffectA = _AudioSourceArray[1];
            _AudioSource_EffectB = _AudioSourceArray[2];

            if (PlayerPrefs.GetFloat("AudioBackgroundVolumn") >= 0)
            {
                AudioBackgroundVolumn = PlayerPrefs.GetFloat("AudioBackgroundVolumn");
            }
            if (PlayerPrefs.GetFloat("AudioEffectVolumn") >= 0)
            {
                AudioEffectVolumn = PlayerPrefs.GetFloat("AudioEffectVolumn");
            }
        }

        public static void PlayBackground(AudioClip audioClip)
        {
            if (_AudioSource_Background == null) return;

            if (_AudioSource_Background.clip == audioClip)
            {
                return;
            }

            _AudioSource_Background.volume = AudioBackgroundVolumn;
            if (audioClip)
            {
                _AudioSource_Background.loop = true;
                _AudioSource_Background.clip = audioClip;
                _AudioSource_Background.Play();
            }
            else
            {
                Debug.LogWarning("[AudioManager.cs/PlayBackground()] audioClip==null !");
            }
        }

        public static void PlayBackground(string strAudioName)
        {
            if (!string.IsNullOrEmpty(strAudioName))
            {
                PlayBackground(_AudioClipLib[strAudioName]);
            }
            else
            {
                Debug.LogWarning("[AudioManager.cs/PlayBackground()] strAudioName==null !");
            }
        }

        public static void PlayAudioEffectA(AudioClip audioClip)
        {
            if (_AudioSource_EffectA == null) return;

            _AudioSource_EffectA.volume = AudioEffectVolumn;

            if (audioClip)
            {
                _AudioSource_EffectA.clip = audioClip;
                _AudioSource_EffectA.Play();
            }
            else
            {
                Debug.LogWarning("[AudioManager.cs/PlayAudioEffectA()] audioClip==null ! Please Check! ");
            }
        }

        public static void PlayAudioEffectB(AudioClip audioClip)
        {
            if (_AudioSource_EffectB == null) return;

            _AudioSource_EffectB.volume = AudioEffectVolumn;

            if (audioClip)
            {
                _AudioSource_EffectB.clip = audioClip;
                _AudioSource_EffectB.Play();
            }
            else
            {
                Debug.LogWarning("[AudioManager.cs/PlayAudioEffectB()] audioClip==null ! Please Check! ");
            }
        }

        public static void PlayAudioEffectA(string strAudioEffctName)
        {
            if (!string.IsNullOrEmpty(strAudioEffctName))
            {
                PlayAudioEffectA(_AudioClipLib[strAudioEffctName]);
            }
            else
            {
                Debug.LogWarning("[AudioManager.cs/PlayAudioEffectA()] strAudioEffctName==null ! Please Check! ");
            }
        }

        public static void PlayAudioEffectB(string strAudioEffctName)
        {
            if (!string.IsNullOrEmpty(strAudioEffctName))
            {
                PlayAudioEffectB(_AudioClipLib[strAudioEffctName]);
            }
            else
            {
                Debug.LogWarning("[AudioManager.cs/PlayAudioEffectB()] strAudioEffctName==null ! Please Check! ");
            }
        }

    }
}

