using Services;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managements
{
    public class ServiceManager : MonoBehaviour
    {
        void Awake()
        {
            _ = new ServiceInitializer();
        }
    }
}