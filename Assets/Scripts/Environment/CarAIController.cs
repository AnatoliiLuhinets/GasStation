using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace Environment
{
    public class CarAIController : BaseComponent
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private List<GameObject> _wheels;
        [SerializeField] private float _proximityThreshold = 0.5f;
        
        private bool IsHere;
        
        public bool MoveTo(Vector3 destination)
        {
            MoveToDestination(destination).Forget();
            return IsHere;
        }

        public void TeleportTo(Vector3 position)
        {
            _agent.Warp(position);
        }

        private async UniTask MoveToDestination(Vector3 destination)
        {
            _agent.SetDestination(destination);
            IsHere = false;

            while (!_agent || (_agent.pathPending || _agent.remainingDistance > _proximityThreshold))
            {
                SpinWheels();
                await UniTask.Yield(PlayerLoopTiming.Update);
            }
            
            IsHere = true;
        }


        private void SpinWheels()
        {
            if (!_agent)
            {
                return;
            }
            
            foreach (var wheel in _wheels)
            {
                wheel.transform.Rotate(_agent.velocity.magnitude * Time.deltaTime * 360, 0, 0);
            }
        }
    }
}