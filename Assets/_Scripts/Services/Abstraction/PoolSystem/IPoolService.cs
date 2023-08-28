using UnityEngine;

namespace Services.Abstraction.PoolSystem
{
    public interface IPoolService
    {
        GameObject GetGameObject(string name);
        void ReleaseGameObject(GameObject gameObject);
    }
}