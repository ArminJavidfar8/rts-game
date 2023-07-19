using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Services.Abstraction.PoolSystem
{
    public interface IPoolable
    {
        string Name { get; }
        void OnGetFromPool();
        void OnReleaseToPool();
    }
}