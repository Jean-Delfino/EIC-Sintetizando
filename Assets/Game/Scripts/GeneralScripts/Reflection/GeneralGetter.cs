using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Reflection;

/*
    Class for some useful reflection
    Basically, any object can call this to get some information from a object that they don't know the type
    And call a method to execute one operation

    TextSend gets a UnityEvent then gets its object an call a FieldName via Invoke()
    This is useful to set a Text, because i can use any object an get a field of this AbstractGetter object
    Avoiding GetComponent<> or FindObject<>
*/

namespace GameGeneralScripts.Reflection{
    public class GeneralGetter : AbstractGetter{
        private string nameField;
        private string nameProperty;

        public void PropertyName(string name) { nameProperty = name; }
        public void FieldName(string name){ nameField = name; } //Just to be used in the inspector

        public override void SetReceptorField(object receptor, string nameFieldReceptor){
            Type typeThisClass = this.GetType();
            Type typeReceptor = receptor.GetType();

            FieldInfo thisField = typeThisClass.GetField(nameField, BindingFlags.NonPublic | BindingFlags.Instance);
            FieldInfo receptorField = typeReceptor.GetField(nameFieldReceptor, BindingFlags.NonPublic | BindingFlags.Instance);

            receptorField.SetValue(receptor, thisField.GetValue(this));
        }

        public override void SetReceptorProperty(object receptor, string namePropertyReceptor){
            Type typeThisClass = this.GetType();
            Type typeReceptor = receptor.GetType();

            PropertyInfo thisProperty = typeThisClass.GetProperty(nameProperty);
            PropertyInfo receptorProperty = typeReceptor.GetProperty(namePropertyReceptor);

            receptorProperty.SetValue(receptor, thisProperty.GetValue(this, null));
        }
    }
}
