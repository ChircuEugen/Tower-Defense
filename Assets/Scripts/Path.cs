using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public Transform[] waypoints;


    private void OnDrawGizmos()
    {
        if(waypoints.Length > 0)
        {
            for(int i=0; i<waypoints.Length; i++)
            {
                if(i < waypoints.Length - 1)
                {
                    Gizmos.color = Color.white;
                    Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
                }
            }
        }
    }

    public Vector3 GetPosition(int index)
    {
        return waypoints[index].position;
    }
}
