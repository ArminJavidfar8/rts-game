using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managements {
    public class GameManager : MonoBehaviour
    {
        void Awake()
        {
            _ = ServiceHolder.ServiceProvider;
        }
    }
}