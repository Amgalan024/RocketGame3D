﻿using Core.InteractionHandle.Visitors;
using Rocket.Components.InteractionHandlers;
using Rocket.Models;
using UnityEngine;

namespace Rocket.Components.Projectile
{
    public class ProjectileInteractionHandler : MonoBehaviour, IRocketComponent, ICollisionEnterVisitor
    {
        [SerializeField] private GameObject _projectile;

        public RocketModel RocketModel { get; set; }
        public IInteractionVisitor CollisionEnterVisitor { get; set; }

        private void OnCollisionEnter(Collision collision)
        {
            var collisionEnterHandler = collision.gameObject.GetComponent<ICollisionEnterVisitor>();

            collisionEnterHandler?.CollisionEnterVisitor.Visit(this);
        }

        private void OnTriggerEnter(Collider other)
        {
            var triggerEnterHandler = other.gameObject.GetComponent<ITriggerEnterVisitor>();

            triggerEnterHandler?.TriggerEnterVisitor.Visit(this);
        }

        public void InitializeComponent(RocketModel rocketModel)
        {
            RocketModel = rocketModel;
            CollisionEnterVisitor = new ProjectileCollisionEnterVisitor(_projectile);
        }
    }
}