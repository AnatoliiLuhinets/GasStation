using System.Collections;
using System.Collections.Generic;
using Environment;
using UnityEngine;

namespace Managers
{
    public class CarService : MonoBehaviour
    {
        [SerializeField] private GasStation _gasStation;
        [SerializeField] private Transform _startPosition;
        [SerializeField] private Transform _exitPoint;
        [SerializeField] private CarAIController _carPrefab;

        private List<CarAIController> _cars = new List<CarAIController>();
        private int _maxActiveCars = 4;
        private float _spawnDelay => Random.Range(8f, 15f);

        private void Start()
        {
            StartCoroutine(SpawnCars());
        }

        private IEnumerator SpawnCars()
        {
            for (int i = 0; i < _maxActiveCars; i++)
            {
                var car = Instantiate(_carPrefab, _startPosition.position, _startPosition.rotation);
                _cars.Add(car);
                car.gameObject.SetActive(false);
            }

            while (true)
            {
                ActivateNextCar();
                yield return new WaitForSeconds(_spawnDelay);
            }
        }

        private void ActivateNextCar()
        {
            foreach (var car in _cars)
            {
                if (!car.gameObject.activeSelf)
                {
                    StartCoroutine(ServiceCar(car));
                    return;
                }
            }
        }

        private IEnumerator ServiceCar(CarAIController car)
        {
            car.gameObject.SetActive(true);
            var freePoint = _gasStation.GetPoint();

            yield return new WaitWhile(() => !car.MoveTo(freePoint.Transform.position));
            
            StartCoroutine(CompleteService(car, freePoint));
        }
        
        private IEnumerator CompleteService(CarAIController car, GasPoint gasPoint)
        {
            yield return new WaitForSeconds(Random.Range(5f, 8f));
            
            _gasStation.EndService(gasPoint);

            yield return new WaitWhile(() => !car.MoveTo(_exitPoint.position));
            
            car.TeleportTo(_startPosition.position);
            car.gameObject.SetActive(false); 
            
            yield return null;
        }
    }
}