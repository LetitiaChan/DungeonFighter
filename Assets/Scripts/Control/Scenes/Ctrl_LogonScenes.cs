using UnityEngine;
using Global;
using Kernal;

namespace Control
{
    public class Ctrl_LogonScenes : BaseControl
    {
        public static Ctrl_LogonScenes Instance;
        public AudioClip aucBackgroundMusic;

        void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            AudioManager.AudioBackgroundVolumn = 0.5f;
            AudioManager.AudioEffectVolumn = 1f;
            AudioManager.PlayBackground(aucBackgroundMusic);
        }


        public void PlayAudioEffectBySword()
        {
            AudioManager.PlayAudioEffectA("SwordHero_MagicA");
        }

        public void PlayAudioEffectByMagic()
        {
            AudioManager.PlayAudioEffectA("2_FireBallEffect_MagicHero");
        }

        public void EnterNextScenes()
        {
            base.EnterNextScenes(Scenes.LevelOne);
        }

    }
}
