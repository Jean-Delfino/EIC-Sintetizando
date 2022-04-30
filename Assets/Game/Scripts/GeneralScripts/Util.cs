using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Reflection;


using UnityEngine;
using UnityEngine.Events;

public static class Util{
    
    //Coroutines
    public static IEnumerator WaitForFrames(int frameCount){
        while (frameCount > 0){
            frameCount--;
            yield return null; //returning 0 or null will make it wait 1 frame
        }
    }

    //Array work
    public static void SequencialFeed(int[] arrayN, int tam){  
        int i;

        for(i = 0; i < tam; i++){
            arrayN[i] = i;
        }
    }

    public static void ShuffleArray(int[] arrayN){
        System.Random random = new System.Random();
        arrayN = arrayN.OrderBy(x => random.Next()).ToArray();
    }

    public static void RandomVectorFill(int[] arrayN, int startIndex, int min,int max){
        int i;

        for(i = startIndex; i < arrayN.Length ; i++){
            //Range(int minInclusive, int maxExclusive);
            arrayN[i] = UnityEngine.Random.Range(min , max); //This actually helps us being exclusive
        }
    }

    public static void PrintVector(int[] arrayN){
        int i;

        for(i = 0 ; i < arrayN.Length; i++){
            Debug.Log(i + ": " + arrayN[i]);
        }
    }

    public static bool FindOcorrence(string origin, string[] search, int lenghtOfSearch){
        int i, j;
        string hold;

        for(i = 0; i < origin.Length; i+= lenghtOfSearch){
            
            hold = origin.Substring(i , lenghtOfSearch);
            
            for(j = 0; j < search.Length; j++){

                if(hold.Equals(search[j])){
                    return true;
                }
            }
        }

        return false;
    }

    public static string RandomSubString(string origin, int lenghtCUT, int min, int max){
        int position = UnityEngine.Random.Range(0, max);
        //return DNAString.Substring(position, quantity - (2 *  AMNManager.GetSizeAMN()));
        return origin.Substring(position, lenghtCUT);
    }

    //Unity Events

    public static void UnityEventInvokeAllListenersTheSame(UnityEvent m_MyEvent, object[] parameter, Type[] argumentType){
        int i;

        for(i = 0; i < m_MyEvent.GetPersistentEventCount(); i++){
            UnityEventInvokeListenerByIndex(m_MyEvent, i, parameter, argumentType);
        } 
    }

    public static void UnityEventInvokeListenerByIndex(UnityEvent m_myEvent, int eventIndex, object[] parameter, Type[] argumentType){
        object myObj;

        myObj = m_myEvent.GetPersistentTarget(eventIndex);
        MethodInfo another = UnityEventGetMethodInfo(m_myEvent, eventIndex, parameter, argumentType, myObj);
        
        another.Invoke(myObj, parameter);
    }

    public static void UnityEventInvokeListenerByIndexObj(UnityEvent m_myEvent, int eventIndex, object[] parameter, Type[] argumentType, object obj){
        MethodInfo another = UnityEventGetMethodInfo(m_myEvent, eventIndex, parameter, argumentType, obj);
        
        another.Invoke(obj, parameter);
    }

    public static MethodInfo UnityEventGetMethodInfo(UnityEvent m_myEvent, int eventIndex, object[] parameter, Type[] argumentType, object obj){
        return UnityEvent.GetValidMethodInfo(obj, 
            m_myEvent.GetPersistentMethodName(eventIndex), argumentType);
    }
}
