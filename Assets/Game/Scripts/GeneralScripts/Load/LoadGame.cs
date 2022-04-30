using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Is in UI primarly because it won't be used in others parts of the game
//But it can be reused of course, joining in the GeneralScripts

namespace GameSceneManagement{
    public class LoadGame : MonoBehaviour{
        public void LoadGameplay(){
            Loader.Load(Loader.Scene.Gameplay);
        }
    }
}
