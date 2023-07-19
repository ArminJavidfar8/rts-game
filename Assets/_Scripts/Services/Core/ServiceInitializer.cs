using Services.Core.Unit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Services
{
    public class ServiceInitializer
    {
        public ServiceInitializer()
        {
            _ = new UnitService();
        }
    }
}