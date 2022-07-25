using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tt : MonoBehaviour
{



    public void ModelChange(){

        if(GameObject.Find("GameObject").GetComponent<tuya>().isActiveAndEnabled){
            
            GameObject.Find("GameObject").GetComponent<tuya>().enabled = false;
            GameObject.Find("GameObject").GetComponent<View>().enabled = true;
        }else{
            GameObject.Find("GameObject").GetComponent<tuya>().enabled = true;
            GameObject.Find("GameObject").GetComponent<View>().enabled = false;
        }
        
    }


}
