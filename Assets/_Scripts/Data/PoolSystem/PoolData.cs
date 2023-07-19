using Services.Abstraction.PoolSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data.PoolSystem
{
    [CreateAssetMenu(fileName = "PoolData", menuName = "rts-game/PoolSystem/PoolData")]
    public class PoolData : ScriptableObject
    {
        [SerializeField] private GameObject[] _prefabs;

        public GameObject[] Prefabs => _prefabs;
    }
}