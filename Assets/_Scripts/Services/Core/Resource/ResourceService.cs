using Services.Abstraction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Services.Core.ResourceSystem
{
    public class ResourceService : IResourceService
    {
        private static ResourceService _instance;

        public static ResourceService Instance 
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ResourceService();
                }
                return _instance;
            }
        }

        public T GetResource<T>(string name) where T : ScriptableObject
        {
            return Resources.Load<T>(name);
        }
    }
}