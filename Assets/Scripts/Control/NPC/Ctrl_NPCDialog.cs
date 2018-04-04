using UnityEngine;
using System.Collections;
using Model;

namespace Contrl
{
    public class Ctrl_NPCDialog : MonoBehaviour
    {

        public static Ctrl_NPCDialog Instance;

        void Awake()
        {
            Instance = this;
        }

    }
}