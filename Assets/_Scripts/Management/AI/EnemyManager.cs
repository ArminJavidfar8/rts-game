using Common;
using Data.Unit;
using Extensions;
using Managements.Unit;
using Services.Abstraction;
using Services.Core.ResourceSystem;
using Services.Core.Unit;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managements.AI
{
    public class EnemyManager : MonoBehaviour, IServiceUser
    {
        private WaitForSeconds _spawnDelay;

        private List<BaseUnit> _enemies;
        private IResourceService _resourceService;
        private IUnitService _unitService;
        
        private void Start()
        {
            SetDependencies();
            _enemies = new List<BaseUnit>();
            _spawnDelay = new WaitForSeconds(5);
            StartCoroutine(GenerateEnemies());
        }

        public void SetDependencies()
        {
            _resourceService = ResourceService.Instance;
            _unitService = UnitService.Instance;
        }

        private IEnumerator GenerateEnemies()
        {
            IEnumerable<BaseUnitData> units = _resourceService.GetResource<UnitsCollection>(Constants.Paths.UNITS_COLLECTION).Units;
            int spawnRange = 40;
            while (true)
            {
                BaseUnit enemyUnit = _unitService.SpawUnit(units.GetRandomItem<BaseUnitData>(), Vector3Extension.GetRandomPositionOnGround(spawnRange), Constants.Tags.ENEMY);
                if (enemyUnit is SimpleSoldier)
                {
                    BaseUnitAI unitAI = enemyUnit.gameObject.AddComponent<SimpleSoldierAI>();
                    unitAI.Initialize(enemyUnit);
                }
                _enemies.Add(enemyUnit);
                yield return _spawnDelay;
            }
        }
    }
}