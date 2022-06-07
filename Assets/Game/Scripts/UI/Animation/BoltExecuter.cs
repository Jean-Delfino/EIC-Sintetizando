using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Kinda of the desing pattern of the Strategy
*/

public abstract class BoltExecuter : MonoBehaviour{
    [SerializeField] float gap = 2;
    [SerializeField] GameObject bolt;

    // Start is called before the first frame update
    public abstract void InstantiateBolt(Transform origin);

    protected void SpawnBolt(Transform origin , Vector3 pos , Vector3 rotation){
        GameObject holder = Instantiate<GameObject>(bolt , origin);
            
        holder.GetComponent<RectTransform>().localPosition += pos;
        holder.GetComponent<RectTransform>().Rotate(rotation , Space.Self);
    }

    protected float GetGap(){
        return gap;
    }
}
