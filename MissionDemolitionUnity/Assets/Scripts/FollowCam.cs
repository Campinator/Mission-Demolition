/**
 * Date Created: Feb 14. 2022
 * Created By: Camp Steiner
 * 
 * Date Modified: Feb 14, 2022
 * Modified by: Camp Steiner
 * 
 * Description: Camera to follow projectile
 * 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public static GameObject pointOfInterest; //what to follow

    public float camZ; //z index of camera currently

    [Header("Set in Inspector")]
    public float easing = 0.5f; //smoothing on camera
    public Vector2 minXY = Vector2.zero; //clamping camera
    
    void Awake()
    {
        camZ = this.transform.position.z;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if (pointOfInterest == null) return;
        Vector3 dest = pointOfInterest.transform.position;
        //clamp x and y 
        dest.x = Mathf.Max(minXY.x, dest.x);
        dest.y = Mathf.Max(minXY.y, dest.y);
        //interpolate from current position to target object
        dest = Vector3.Lerp(this.transform.position, dest, easing);
        dest.z = camZ;
        this.transform.position = dest;

        Camera.main.orthographicSize = dest.y + 10;

    }
}
