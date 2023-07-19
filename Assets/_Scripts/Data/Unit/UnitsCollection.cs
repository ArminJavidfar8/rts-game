using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data.Unit
{
    [CreateAssetMenu(fileName = "UnitsCollection", menuName = "rts-game/UnitsCollection")]
    public class UnitsCollection : ScriptableObject
    {
        [SerializeField] private BaseUnitData[] _units;

        public BaseUnitData[] Units => _units;
    }
}