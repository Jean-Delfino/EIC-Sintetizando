using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using TMPro;
using GameGeneralScripts.Reflection;
/*
    See GeneralGetter for some info, basically you pass actualText, and set a TMPro component with 
    actualText, but you don't know which object you getting the text, just that the text it is in
    some of its fields
*/

public class TextSend : MonoBehaviour{
    [SerializeField] List<TextEvent> textLis;
    
    [System.Serializable]
    private class TextEvent{
        public UnityEvent myEvent;

        public TextMeshProUGUI textField;
    }

    private string actualText;

    void Start(){
        SendAllText();
    }

    
    public void SendAllText(){
        int i;
        object myGetter;
        AbstractGetter gg;

        UnityEvent m_MyEvent;

        for(i = 0; i < textLis.Count; i++){
            m_MyEvent = textLis[i].myEvent; //Always just one event
            myGetter = m_MyEvent.GetPersistentTarget(0);
            m_MyEvent.Invoke();
            
            gg = (AbstractGetter) myGetter;
            gg.SetReceptorField(this, "actualText");   

            print(actualText);

            textLis[i].textField.text = textLis[i].textField.text + actualText;
        }
    }

}

    /*
    public static MyDelegate GetMyDelegateFromStringReflection(string methodName){
        MyDelegate function = MyFunctionsClass.CallMethodOne;

        Type inf = typeof(MyFunctionsClass);
        foreach (var method in inf.GetMethods())
        {
            if (method.Name == methodName)
            {
                function = (MyDelegate)Delegate.CreateDelegate(typeof(MyDelegate), method);
            }
        }

        return function;
    }*/

