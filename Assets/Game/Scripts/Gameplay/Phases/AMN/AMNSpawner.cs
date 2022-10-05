using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using GameUserInterface.Text;

namespace PhasePart.AMN{
    public class AMNSpawner : MonoBehaviour{
        [SerializeField] float animationsTime = 1f;
 
        [SerializeField] Letter letterPrefab = default;
        [SerializeField] Transform letterSpawn = default; //Could be this.transform though
        
        [SerializeField] float AMNPrefabWidth = 65f; 
        private float spaceBetween;

        
        private void Start() {
            spaceBetween = letterSpawn.GetComponent<HorizontalLayoutGroup>().spacing;
        }

        public void SpawnAMN(string RNAString){
            int i;
            Letter hold;

            for(i = 0; i < RNAString.Length; i++){
                hold = Instantiate<Letter>(letterPrefab, letterSpawn);
                hold.Setup(RNAString[i].ToString());        
            }

        }

        public float NextAMN(bool lastOne){
            RectTransform rT = letterSpawn.GetComponent<RectTransform>();
            float moveDistance = (AMNPrefabWidth + spaceBetween) * 3;

            if(lastOne){
                moveDistance -= AMNPrefabWidth / 2;
            }

            moveDistance = rT.anchoredPosition.x - moveDistance;

            LeanTween.moveX(rT, moveDistance, animationsTime);
            
            return animationsTime;
        }

        private IEnumerator MoveTowardsPosition(Vector3 target){

            yield return null;
        }
    }
}
