using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    See GeneralGetter
*/

namespace GameGeneralScripts.Reflection{
    public abstract class AbstractGetter : MonoBehaviour{
        public abstract void SetReceptorField(object receptor, string nameFieldReceptor);
        public abstract void SetReceptorProperty(object receptor, string namePropertyReceptor);
    }
}
