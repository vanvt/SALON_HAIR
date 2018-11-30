using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SALON_HAIR_API.Measure
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class MeasureController:Attribute
    {
       public MeasureController(string controller)
        {
            Console.WriteLine("MeasureController");
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine(controller);
        }

    }
}



