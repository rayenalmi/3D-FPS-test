using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility 
{
    public static T[] ShufulleArray<T>(T[] array ,int seed)
    {
        System.Random prng = new System.Random(seed);

        for (int i =0; i< array.Length - 1; i++)
        {
            int randomindex = prng.Next(i, array.Length);

            T tempItm = array[randomindex];
            array[randomindex] = array[i];
            array[i] = tempItm;
        }
        return array; 
    }



   
}
