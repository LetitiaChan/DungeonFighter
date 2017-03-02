/***
 * 
 *  插件： 音频管理类
 * 
 *  功能： 项目中音频剪辑统一管理。
 *
 *  作者： 刘国柱
 *
 *  Version： 1.0
 * 
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;                                              //泛型集合命名空间

namespace Kernal
{
    public class AudioManager : MonoBehaviour
    {
        public AudioClip[] AudioClipArray;                                     //剪辑数组
        public static float AudioBackgroundVolumns = 1F;                       //背景音量
        public static float AudioEffectVolumns = 1F;                           //音效音量

        private static Dictionary<string, AudioClip> _DicAudioClipLib;         //音频库
        private static AudioSource[] _AudioSourceArray;                        //音频源数组
        private static AudioSource _AudioSource_BackgroundAudio;               //背景音乐
        private static AudioSource _AudioSource_AudioEffectA;                  //音效源A
        private static AudioSource _AudioSource_AudioEffectB;                  //音效源B

        /// <summary>
        /// 音效库资源加载
        /// </summary>
        void Awake()
        {
            //音频库加载
            _DicAudioClipLib = new Dictionary<string, AudioClip>();
            foreach (AudioClip audioClip in AudioClipArray)
            {
                _DicAudioClipLib.Add(audioClip.name, audioClip);
            }
            //处理音频源
            _AudioSourceArray = this.GetComponents<AudioSource>();
            _AudioSource_BackgroundAudio = _AudioSourceArray[0];
            _AudioSource_AudioEffectA = _AudioSourceArray[1];
            _AudioSource_AudioEffectB = _AudioSourceArray[2];

            //从数据持久化中得到音量数值
            if (PlayerPrefs.GetFloat("AudioBackgroundVolumns") >= 0)
            {
                AudioBackgroundVolumns = PlayerPrefs.GetFloat("AudioBackgroundVolumns");
                _AudioSource_BackgroundAudio.volume = AudioBackgroundVolumns;
            }
            if (PlayerPrefs.GetFloat("AudioEffectVolumns") >= 0)
            {
                AudioEffectVolumns = PlayerPrefs.GetFloat("AudioEffectVolumns");
                _AudioSource_AudioEffectA.volume = AudioEffectVolumns;
                _AudioSource_AudioEffectB.volume = AudioEffectVolumns;
            }
        }

        /// <summary>
        /// 播放背景音乐
        /// </summary>
        /// <param name="audioClip">音频剪辑</param>
        public static void PlayBackground(AudioClip audioClip)
        {
            //防止背景音乐的重复播放。
            if (_AudioSource_BackgroundAudio.clip == audioClip)
            {
                return;
            }
            //处理全局背景音乐音量
            _AudioSource_BackgroundAudio.volume = AudioBackgroundVolumns;
            if (audioClip)
            {
                _AudioSource_BackgroundAudio.loop = true;
                _AudioSource_BackgroundAudio.clip = audioClip;
                _AudioSource_BackgroundAudio.Play();
            }
            else
            {
                Debug.LogWarning("[AudioManager.cs/PlayBackground()] audioClip==null !");
            }
        }

        /// <summary>
        /// 播放背景音乐
        /// </summary>
        /// <param name="strAudioName"></param>
        public static void PlayBackground(string strAudioName)
        {
            if (!string.IsNullOrEmpty(strAudioName))
            {
                PlayBackground(_DicAudioClipLib[strAudioName]);
            }
            else
            {
                Debug.LogWarning("[AudioManager.cs/PlayBackground()] strAudioName==null !");
            }
        }

        /// <summary>
        /// 播放音效_音频源A
        /// </summary>
        /// <param name="audioClip">音频剪辑</param>
        public static void PlayAudioEffectA(AudioClip audioClip)
        {
            //处理全局音效音量
            _AudioSource_AudioEffectA.volume = AudioEffectVolumns;

            if (audioClip)
            {
                _AudioSource_AudioEffectA.clip = audioClip;
                _AudioSource_AudioEffectA.Play();
            }
            else
            {
                Debug.LogWarning("[AudioManager.cs/PlayAudioEffectA()] audioClip==null ! Please Check! ");
            }
        }

        /// <summary>
        /// 播放音效_音频源B
        /// </summary>
        /// <param name="audioClip">音频剪辑</param>
        public static void PlayAudioEffectB(AudioClip audioClip)
        {
            //处理全局音效音量
            _AudioSource_AudioEffectB.volume = AudioEffectVolumns;

            if (audioClip)
            {
                _AudioSource_AudioEffectB.clip = audioClip;
                _AudioSource_AudioEffectB.Play();
            }
            else
            {
                Debug.LogWarning("[AudioManager.cs/PlayAudioEffectB()] audioClip==null ! Please Check! ");
            }
        }

        /// <summary>
        /// 播放音效_音频源A
        /// </summary>
        /// <param name="strAudioEffctName">音效名称</param>
        public static void PlayAudioEffectA(string strAudioEffctName)
        {
            if (!string.IsNullOrEmpty(strAudioEffctName))
            {
                PlayAudioEffectA(_DicAudioClipLib[strAudioEffctName]);
            }
            else
            {
                Debug.LogWarning("[AudioManager.cs/PlayAudioEffectA()] strAudioEffctName==null ! Please Check! ");
            }
        }

        /// <summary>
        /// 播放音效_音频源B
        /// </summary>
        /// <param name="strAudioEffctName">音效名称</param>
        public static void PlayAudioEffectB(string strAudioEffctName)
        {
            if (!string.IsNullOrEmpty(strAudioEffctName))
            {
                PlayAudioEffectB(_DicAudioClipLib[strAudioEffctName]);
            }
            else
            {
                Debug.LogWarning("[AudioManager.cs/PlayAudioEffectB()] strAudioEffctName==null ! Please Check! ");
            }
        }

        /// <summary>
        /// 改变背景音乐音量
        /// </summary>
        /// <param name="floAudioBGVolumns"></param>
        public static void SetAudioBackgroundVolumns(float floAudioBGVolumns)
        {
            _AudioSource_BackgroundAudio.volume = floAudioBGVolumns;
            AudioBackgroundVolumns = floAudioBGVolumns;
            //数据持久化
            PlayerPrefs.SetFloat("AudioBackgroundVolumns", floAudioBGVolumns);
        }

        /// <summary>
        /// 改变音效音量
        /// </summary>
        /// <param name="floAudioEffectVolumns"></param>
        public static void SetAudioEffectVolumns(float floAudioEffectVolumns)
        {
            _AudioSource_AudioEffectA.volume = floAudioEffectVolumns;
            _AudioSource_AudioEffectB.volume = floAudioEffectVolumns;
            AudioEffectVolumns = floAudioEffectVolumns;
            //数据持久化
            PlayerPrefs.SetFloat("AudioEffectVolumns", floAudioEffectVolumns);
        }
    }
}