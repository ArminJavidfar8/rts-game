using Services.Abstraction;
using UnityEngine;

namespace Services.Core.ResourceSystem
{
    public class ResourceService : IResourceService
    {
        public T GetResource<T>(string name) where T : ScriptableObject
        {
            return Resources.Load<T>(name);
        }
    }
}