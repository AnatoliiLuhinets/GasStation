using System.Collections.Generic;
using Common;
using Environment;
using UnityEngine;
using Zenject;
using Cysharp.Threading.Tasks;

namespace Managers
{
    public class CarService : MonoBehaviour
    {
        [SerializeField] private Transform _startPosition;
        [SerializeField] private Transform _exitPoint;
        [SerializeField] private CarAIController _carPrefab;

        private SpawnService<BaseComponent> _spawnService;
        private GasStation _gasStation;
        private SignalBus _signalBus;
        
        private List<CarAIController> _cars = new List<CarAIController>();

        private const int _maxActiveCars = 8;
        private float SpawnDelay => Random.Range(7f, 12f);
        private float ServiceDelay => Random.Range(3f, 6f);

        [Inject]
        private void Construct(GasStation station, SpawnService<BaseComponent> spawnService, SignalBus signalBus)
        {
            _gasStation = station;
            _spawnService = spawnService;
            _signalBus = signalBus;
            
            SpawnCars().Forget();
        }
        
        private async UniTask SpawnCars()
        {
            for (int i = 0; i < _maxActiveCars; i++)
            {
                var baseComponent = await _spawnService.SpawnPrefab(_carPrefab, _startPosition.position, _startPosition.rotation);
                
                if (baseComponent is CarAIController car)
                {
                    _cars.Add(car);
                    car.gameObject.SetActive(false);
                }
            }

            while (true)
            {
                ActivateNextCar();
                await UniTask.Delay(System.TimeSpan.FromSeconds(SpawnDelay));
            }
        }

        private void ActivateNextCar()
        {
            foreach (var car in _cars)
            {
                if (!car.gameObject.activeSelf)
                {
                    ServiceCar(car).Forget();
                    return;
                }
            }
        }

        private async UniTask ServiceCar(CarAIController car)
        {
            car.gameObject.SetActive(true);
            var freePoint = _gasStation.GetPoint();
            
            await UniTask.WaitWhile(() => !car.MoveTo(freePoint.Transform.position));
            
            CompleteService(car, freePoint).Forget();
        }
        
        private async UniTask CompleteService(CarAIController car, GasPoint gasPoint)
        {
            await UniTask.Delay(System.TimeSpan.FromSeconds(ServiceDelay));
            
            _gasStation.EndService(gasPoint);
            _signalBus.Fire(new EnvironmentSignals.OnServiceEnd());

            await UniTask.WaitWhile(() => !car.MoveTo(_exitPoint.position));
            
            car.TeleportTo(_startPosition.position);
            car.gameObject.SetActive(false); 
        }
    }
}
