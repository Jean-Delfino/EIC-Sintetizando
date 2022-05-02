using UnityEngine;

/*Name is self explanatory*/

using GameGeneralScripts.Reflection;

namespace GameGeneralScripts.Player{
    public class PlayerInfo : GeneralGetter{
        /*
        private string actualProtein{
            get{
                if(Instance == this){
                    return actualProtein;
                } 
                return Instance.actualProtein;
            }
            set{
                if(Instance == this){
                    actualProtein = value;
                    return;
                }
                SetProteinName(value);
            }      
        }
        private string namePlayer{
            get{ 
                if(Instance == this){
                    return namePlayer;
                } 

                return Instance.namePlayer;
            }
            set{
                if(Instance == this){
                    namePlayer = value;
                    return;
                }
                SetNamePlayer(value);
            }      
        }
        */
        private string actualProtein;
        private string namePlayer;
        private float maxScore;
        private float lastScore;
        private float actualScore;

        //private string textTeste = "Al";

        //private float maxTime;
        //private float actualBestTime;
        public static PlayerInfo Instance;

        private void Awake() { 
        // If there is an instance, do nothing 
            if (Instance == null) { 
                print("ENTROU");
                Instance = this;  
                DontDestroyOnLoad(Instance);
            }

            print(this.gameObject.name);
            print(Instance.gameObject.name);

            print("1 : " + GetNamePlayer());
            print("2 : " + GetActualProtein());

            //this = Instance
            //Destroy(Instance.gameObject);
        }

        public void SetNamePlayer(string namePlayer){this.namePlayer = namePlayer; print("BOOM " + Instance.namePlayer);}
        public void SetProteinName(string nameProtein){this.actualProtein = nameProtein; print("BOOM2 " + Instance.actualProtein);}
        public string GetNamePlayer(){return namePlayer;}
        public string GetActualProtein(){return actualProtein;}
    }
}
