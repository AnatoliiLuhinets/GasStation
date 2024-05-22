using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Environment
{
    public class CarAIController : BaseComponent
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private List<GameObject> _wheels;
        [SerializeField] private float _proximityThreshold = 0.5f;
        [field: SerializeField] public bool IsHere { get; private set; }

        private Coroutine _moveCoroutine;

        public bool MoveTo(Vector3 destination)
        {
            if (_moveCoroutine != null)
            {
                StopCoroutine(_moveCoroutine);
                _moveCoroutine = null;
            }

            _moveCoroutine = StartCoroutine(MoveToDestination(destination));
            return IsHere;
        }

        public void TeleportTo(Vector3 position)
        {
            _agent.Warp(position);
        }

        private IEnumerator MoveToDestination(Vector3 destination)
        {
            _agent.SetDestination(destination);
            IsHere = false;

            while (_agent.pathPending || _agent.remainingDistance > _proximityThreshold)
            {
                SpinWheels();
                yield return null;
            }
            IsHere = true;
        }

        private void SpinWheels()
        {
            foreach (var wheel in _wheels)
            {
                wheel.transform.Rotate(_agent.velocity.magnitude * Time.deltaTime * 360, 0, 0);
            }
        }
    }
}