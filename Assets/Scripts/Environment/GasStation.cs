using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Environment
{
    [Serializable]
    public class GasPoint
    {
        public Transform Transform;
        public bool IsFree = true;
    }
    public class GasStation : BaseComponent
    {
        
        [SerializeField] private List<GasPoint> _points = new List<GasPoint>();

        public GasPoint GetPoint()
        {
            var point = _points.FirstOrDefault((p)=> p.IsFree);
            point.IsFree = false;
            
            return point;
        }

        public void EndService(GasPoint point)
        {
            point.IsFree = true;
        }
    }
}
