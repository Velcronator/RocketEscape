using UnityEngine;

public class IntroWaypointFollower : MonoBehaviour
{
    public GameObject[] waypoints; //array of waypoints to cycle through during intro
    int currentWP = 0;

    public float introCamSpeed = 15f;     //control camera speed during intro   
    public float introCamAngularSpeed = 4f;  //control turning speed of camera during intro

    private Vector3 direction;          //the vector direction for the next waypoint

    void LateUpdate ()
    {
        if (!GameManager.Instance.introIsComplete)
        {
            //if there are no waypoints, return
            if (waypoints.Length == 0)
            {
                GameManager.Instance.introIsComplete = true;
            }

            if (!GameManager.Instance.introIsComplete)
            {
                direction = waypoints[currentWP].transform.position - this.transform.position;
            }

            if (direction.magnitude < introCamAccuracy) //array still has waypoints to cycle through
            {
                currentWP++;
                if (currentWP >= waypoints.Length)
                {
                    GameManager.Instance.introIsComplete = true;
                }
            }
        }
    }
}   