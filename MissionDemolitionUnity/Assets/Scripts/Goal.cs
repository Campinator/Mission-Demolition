using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public static bool goalMet = false; 
    
    void OnTriggerEnter(Collider other) {
        //when goal hit by something
        if(other.gameObject.tag == "Projectile"){ //if it's a projectile
            Goal.goalMet = true;
            //higher opacity for goal
            Material mat = GetComponent<Renderer>().material;
            Color c = mat.color;
            c.a = 1;
            mat.color = c;
        }
    }
}
