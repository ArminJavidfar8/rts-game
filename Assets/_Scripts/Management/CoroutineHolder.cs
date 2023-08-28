using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managements
{
    public class CoroutineHolder : MonoBehaviour
    {
        public static CoroutineHolder Instance { get; private set; }
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
        }
    }
}