using Common;
using Data.Unit;
using Extensions;
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

        private IResourceService _resourceService;
        private IUnitService _unitService;
        
        private void Start()
        {
            SetDependencies();
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
            BaseUnitData[] units = _resourceService.GetResource<UnitsCollection>("Collections/UnitsCollection").Units;
            int spawnRange = 50;
            for (int i = 0; i < 5; i++)
            {
                _unitService.SpawUnit(units[i % units.Length], Vector3Extension.GetRandomPositionOnGround(spawnRange), Constants.Tags.ENEMY);

            }
            yield break;
            while (true)
            {
                _unitService.SpawUnit(units.GetRandomItem(), Vector3Extension.GetRandomPositionOnGround(spawnRange), Constants.Tags.ENEMY);
                yield return _spawnDelay;
            }
        }

    }
}