using Common;
using Data.Unit;
using Extensions;
using Managements.Unit;
using Microsoft.Extensions.DependencyInjection;
using Services.Abstraction;
using Services.Abstraction.CoroutineSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managements.AI
{
    public class EnemyManager
    {
        private WaitForSeconds _spawnDelay;

        private List<BaseUnit> _enemies;
        private IResourceService _resourceService;
        private IUnitService _unitService;
        private ICoroutineService _coroutineService;

        public EnemyManager(IResourceService resourceService, IUnitService unitService, ICoroutineService coroutineService)
        {
            _resourceService = resourceService;
            _unitService = unitService;
            _coroutineService = coroutineService;

            _enemies = new List<BaseUnit>();
            _spawnDelay = new WaitForSeconds(5);
            _coroutineService.StartCoroutine(GenerateEnemies());
        }

        private IEnumerator GenerateEnemies()
        {
            yield return _spawnDelay;
            IEnumerable<IBaseUnitData> units = _resourceService.GetResource<UnitsCollection>(Constants.Paths.UNITS_COLLECTION).Units;
            int spawnRange = 40;
            while (true)
            {
                BaseUnit enemyUnit = _unitService.SpawUnit(units.GetRandomItem<IBaseUnitData>(), Vector3Extension.GetRandomPositionOnGround(spawnRange), Constants.Tags.ENEMY);
                if (enemyUnit is SimpleUnit)
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