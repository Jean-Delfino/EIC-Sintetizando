using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameSceneManagement{
    public static class Loader{
        // Start is called before the first frame update
        //Based on code monkey

        public enum Scene{
            UIBeg,
            Loading,
            Gameplay
        } 

        private static Action onLoaderCallback;
        public static void Load(Scene scene){
            onLoaderCallback = () => {
                SceneManager.LoadScene(scene.ToString());
            };

            SceneManager.LoadScene(Scene.Loading.ToString());
        }

        public static void LoaderCallback(){
            if(onLoaderCallback != null){
                onLoaderCallback();
                onLoaderCallback = null;
            }
        }
    }
}
