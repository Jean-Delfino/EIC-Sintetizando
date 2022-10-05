using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Reflection;
using System.Threading.Tasks;


using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public static class Util{
    //Coroutines for all classes
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

    public static bool FindOcorrence(string origin, string[] search){ //Better version if you not the same size
        int i;
        bool index = false;

        for(i = 0 ; i < search.Length; i++){
            if(origin.IndexOf(search[i]) != -1){
                index = true;
                break;
            }
        }


        return index;
    }

    //String
    public static string RandomSubString(string origin, int lenghtCUT, int min, int max){
        int position = UnityEngine.Random.Range(0, max);
        //return DNAString.Substring(position, quantity - (2 *  AMNManager.GetSizeAMN()));
        return origin.Substring(position, lenghtCUT);
    }
    
    public static string ConvertToString(string[] phrase){
        string res = ""; 
        int i;
        
        for(i = 0; i < phrase.Length ; i++){
            res += phrase[i];
        }

        return res;
    }

    public static bool FindOcorrence(string origin, string[] search, int lenghtOfSearch){ //See above if needed
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

    //Color
    public static Color RandomSolidColor(){
        int count = 1;
        int i = 0;
        float[] tableTruthFillet = new float[3];
        System.Random rand = new System.Random();

        tableTruthFillet[rand.Next(0,3)] = 1f;

        while(i < 3 && count < 2){
            if(tableTruthFillet[i] != 0){
                i++;
                continue;
            }
            
            tableTruthFillet[i] = (float) rand.NextDouble(); 

            if(tableTruthFillet[i] != 0){
                count++;
            }

            i++;
        }

        return new Color(tableTruthFillet[0], tableTruthFillet[1], tableTruthFillet[2]);
    }

    public static Color CreateNewDifferentColor(Color actualColor){
        Color newColor;

        do{
            newColor = RandomSolidColor();
        }while(newColor == actualColor);//Generates a new color

        return newColor;
    }


    //Tasks

    public static int ConvertToMili(double seconds){
        return (int) TimeSpan.FromSeconds(seconds).TotalMilliseconds;
    }

    //Animations
    public static float ChangeScaleAnimation(RectTransform rt, Vector3 finalScale, float time){
        if(rt.localScale == finalScale){
            return 0f;
        } 

        LeanTween.scale(rt, finalScale, time);
        return time;
    }

    public static float ChangeAlphaImageAnimation(RectTransform rt, float finalAlpha, float time){
        LeanTween.alpha(rt, finalAlpha, time); 
        return time;
    }

    public static float ChangeAlphaCanvasImageAnimation(CanvasGroup rt, float finalAlpha, float time){
        LeanTween.alphaCanvas(rt, finalAlpha, time); 
        return time;
    }

    //Rect Transform 

    public static void CopyRectTransform(RectTransform destiny, RectTransform origin){
        destiny.anchorMin = origin.anchorMin;
        destiny.anchorMax = origin.anchorMax;
        destiny.anchoredPosition = origin.anchoredPosition;
        destiny.rotation = origin.rotation;
        destiny.sizeDelta = origin.sizeDelta;
        destiny.localScale = origin.localScale;
    }
}
