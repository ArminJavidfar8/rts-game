using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Services.Abstraction
{
    public interface IResourceService
    {
        T GetResource<T>(string name) where T : ScriptableObject;
    }
}