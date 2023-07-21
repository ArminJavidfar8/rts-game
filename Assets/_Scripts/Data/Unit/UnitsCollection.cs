using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data.Unit
{
    [CreateAssetMenu(fileName = "UnitsCollection", menuName = "rts-game/Collections/UnitsCollection")]
    public class UnitsCollection : ScriptableObject
    {
        [SerializeField] private BaseUnitData[] _units;

        public IEnumerable<BaseUnitData> Units => _units;
    }
}