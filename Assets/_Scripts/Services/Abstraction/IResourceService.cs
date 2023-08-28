using UnityEngine;

namespace Services.Abstraction
{
    public interface IResourceService
    {
        T GetResource<T>(string name) where T : ScriptableObject;
    }
}