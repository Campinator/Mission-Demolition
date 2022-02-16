/**
 * Date Created: Feb 16. 2022
 * Created By: Camp Steiner
 * 
 * Date Modified: Feb 16, 2022
 * Modified by: Camp Steiner
 * 
 * Description: Lines for projectile trajectory
 * 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLine : MonoBehaviour
{
    public static ProjectileLine S; //singleton line

    [Header("Set in Inspector")]
    public float minDistance = 0.1f;

    private LineRenderer line;
    private GameObject _poi;
    private List<Vector3> points;

    private void Awake()
    {
        S = this;
        line = GetComponent<LineRenderer>(); //reference to line, by default disabled
        line.enabled = false;
        points = new List<Vector3>();
    }

    public GameObject poi
    {
        get { return _poi; }
        set {
            _poi = value;
            if(_poi != null)
            {
                line.enabled = false;
                points = new List<Vector3>();
                AddPoints();
            }
        }
    }


}
