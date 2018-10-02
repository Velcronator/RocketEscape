using UnityEngine;

public class CompleteCameraController : MonoBehaviour
{
    public GameObject player;       //Public variable to store a reference to the player game object
    public GameObject wpFollower;   //reference to the waypoint follower, to retrieve the status of the intro completion bool

    private Transform target;       //reference to the camera's target (player or waypoint follower)
    private Vector3 offset;         //Private variable to store the offset distance between the player and camera

    void Awake()
    {
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        offset = transform.position - player.transform.position;
    }

    private void Update()
    {
        //as long as the intro isn't complete, camera should focus on waypoint follower.
        //once intro is complete, camera should follow player

        //a reference to the transform of the camera target

        switch (GameManager.Instance.introIsComplete)
        {
            case true:
                target = player.transform;
                break;
            case false:
                target = wpFollower.transform;
                break;

            default:
                target = player.transform;
                break;
        }

        Debug.Log(target);
    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        transform.position = target.position + offset;

    }
}