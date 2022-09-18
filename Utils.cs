using System;
using System.Collections;
using System.Collections.Generic;
namespace MeindosMod
{
    public static class Utils
    {
        public static IList createList(Type myType)
        {
            Type genericListType = typeof(List<>).MakeGenericType(myType);
            return (IList)Activator.CreateInstance(genericListType);
        }

    }
}
