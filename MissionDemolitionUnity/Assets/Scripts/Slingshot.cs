/**
 * Date Created: Feb 9. 2022
 * Created By: Camp Steiner
 * 
 * Date Modified: Feb 14, 2022
 * Modified by: Camp Steiner
 * 
 * Description: Controls the action of the slingshot
 * 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    [Header("Drop Prefab Here")]
    public GameObject projPrefab;
    [Header("Change Values Around")]
    public float velocityMultiplier = 10f;

    [Header("Don't Touch")]
    static private Slingshot S;  
    public GameObject launchPoint;
    public GameObject projectile; //instance of projectile
    public Vector3 launchPos; //position of projectile
    public bool aimingMode; //is player aiming
    public Rigidbody projRB; //useful for later

    static public Vector3 LAUNCH_POS {                                       
            get {
                if (S == null ) return Vector3.zero;
                return S.launchPos;
            }
        }

    private void Awake()
    {
        S = this;    
        Transform launchPointTrans = transform.Find("LaunchPoint");
        launchPoint = launchPointTrans.gameObject; //find child obj called LaunchPoint
        launchPoint.SetActive(false); //disable element
        launchPos = launchPointTrans.position;
    }

    private void Update()
    {
        if (!aimingMode) return; //do nothing if not aiming

        Vector3 mousePos2D = Input.mousePosition; //grab mouse position
        mousePos2D.z = -Camera.main.transform.position.z; //fix z coordinate
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);

        Vector3 mouseDelta = mousePos3D - launchPos; //pixels between mouse position and launch position

        //limit to slingshot collider radius
        float maxMagnitude = this.GetComponent<SphereCollider>().radius;
        if(mouseDelta.magnitude > maxMagnitude) //if too big, scale to radius of launcher
        {
            mouseDelta.Normalize();
            mouseDelta *= maxMagnitude;
        }

        projectile.transform.position = launchPos + mouseDelta;

        if (Input.GetMouseButtonUp(0)) //on mouse up
        {
            aimingMode = false;
            projRB.isKinematic = false;
            projRB.velocity = -mouseDelta * velocityMultiplier; 
            FollowCam.POI = projectile; //set point of interest for follow cam
            projectile = null;
            MissionDemolition.ShotFired();
            ProjectileLine.S.poi = projectile;
        }
    }
    private void OnMouseEnter()
    {
        launchPoint.SetActive(true); //turn on halo
    }

    private void OnMouseExit()
    {
        launchPoint.SetActive(false); //turn off halo
    }

    private void OnMouseDown()
    {
        aimingMode = true;
        projectile = Instantiate(projPrefab) as GameObject; //create projectile at launch point
        projectile.transform.position = launchPos;

        projRB = projectile.GetComponent<Rigidbody>(); 
        projRB.isKinematic = true; //give Rigidbody kinematics
    }
}
