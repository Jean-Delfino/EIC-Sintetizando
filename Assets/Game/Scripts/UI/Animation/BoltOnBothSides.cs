using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Auto explanatory
*/


public class BoltOnBothSides : BoltExecuter{
    public override void InstantiateBolt(Transform origin){
        float width = origin.GetComponent<RectTransform>().rect.width;

        SpawnBolt(origin , new Vector3(-(width + GetGap()), 0 , 0), new Vector3(0 , 0 , 0));
        SpawnBolt(origin , new Vector3((width + GetGap()) , 0 , 0), new Vector3(0 , 180 , 0));
    }
}
