using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.CompilerServices;

using VComponentID = System.UInt64;
using VGroup = System.UInt64;


namespace V.VComponentSystem
{
    static class Internal
    {
        private static VComponentID lastID = 0;

        static VComponentID GetUniqueComponentID()
        {
            return lastID++;
        }
    }

    static class Global
    {
        static VComponentID typeID = 0;

        static VComponentID GetComponentID<T>() where T : VComponent
        {
            //is is broken here, because C# do not support local static variable
            //So it is imporssiable to generate a unique ID for each type of T inside 
            //the GetComponent<T> function.
            return typeID;
        }
    }

    class VComponent
    {

    }

    public class VComponentManager{
        
	
    }
}
