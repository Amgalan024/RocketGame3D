﻿using System;
using Rocket.Components.Enemy;
using UnityEngine;
using Utils;

namespace Spawners
{
    public class EnemySpawner : MonoBehaviour, ISpawnerGeneric<EnemyInitializer>
    {
        public event Action<EnemyInitializer> OnSpawned;

        [SerializeField] private Transform _root;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private EnemyInitializer _enemyPrefab;
        [SerializeField] private float _spawnFrequency;
        [SerializeField] private int _maxEnemiesCount;

        public Pool<EnemyInitializer> Pool { get; set; }
        public float SpawnTimer { get; set; }

        private int _currentEnemiesCount;

        private void Start()
        {
            SpawnTimer = _spawnFrequency;
        }

        private void Update()
        {
            Spawn();
        }

        public void Initialize()
        {
            Pool = new Pool<EnemyInitializer>(_enemyPrefab, _maxEnemiesCount, _root);

            Pool.AutoExpand = true;
        }

        public void Spawn()
        {
            if ((SpawnTimer <= 0) && (_currentEnemiesCount < _maxEnemiesCount))
            {
                var spawnedEnemy = Pool.GetFreeElement();

                spawnedEnemy.transform.position = _spawnPoint.position;

                OnSpawned?.Invoke(spawnedEnemy);

                SpawnTimer = _spawnFrequency;
                _currentEnemiesCount++;
            }

            CountDown();
        }

        public void CountDown()
        {
            if (SpawnTimer >= 0)
            {
                SpawnTimer -= Time.deltaTime;
            }
        }

        public void DecreaseEnemyCount()
        {
            SpawnTimer = _spawnFrequency;
            _currentEnemiesCount -= 1;
        }
    }
}