using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OffScreenIndicator : MonoBehaviour
{
    public float arrowPadding;                    //padding to stop arrow leaving screen, held as a decimal because of its usage in screenport calculations
    public float textPadding;                      //padding to stop text leaving screen, held as a decimal because of its usage in screenport calculations

    GameObject endLevel_GO;                //reference to level objective object
    GameObject player_GO;            //reference to player object, do it in editor rather than via script because rocket pieces are all tagged 'player'
    Camera mainCam;                         //reference to Main Camera
    float distanceToTarget;                 //distance from ship to objective
    Transform spriteChild;                  //reference to the child which holds the Arrow Sprite
    Text distanceTextChild;            //reference to the child which holds the distance text

    Vector3 playerPos;                      //player position
    Vector3 endLevelPos;                    //end of level position

    private void Awake ()
    {
        //initialise references to relevant objects
        endLevel_GO = GameObject.FindGameObjectWithTag("Finish");
        mainCam = Camera.main;
        spriteChild = transform.Find("IndicatorSprite");
        distanceTextChild = transform.Find("DistanceIndicator").GetComponent<Text>();
        player_GO = GameObject.FindGameObjectWithTag("Player");
        distanceTextChild.gameObject.SetActive(true);
        spriteChild.gameObject.SetActive(true);

    }

    void Update ()
    {
        UpdateTargetAngleAndDistance();                                 //update the distance from player to endgoal and rotate arrow to point at goal

        UpdateIndicatorPosition();                                      //update the screen position of the arrow based on where the goal is (when offscreen) 
    }

    private void UpdateTargetAngleAndDistance()
    {
        endLevelPos = endLevel_GO.gameObject.transform.position;        //update endlevelpos each frame
        playerPos = player_GO.gameObject.transform.position;            //update playerpos each frame
        distanceToTarget = (int)Vector3.Distance(playerPos, endLevelPos);    //update distance to target based on two above lines
        distanceTextChild.text = distanceToTarget.ToString();           //update text on child to equal distance from endgoal


        Vector3 arrowDirection = endLevelPos - playerPos;               //get direction vector of endlevel from player
        float rotationZ = Mathf.Atan2(arrowDirection.y, arrowDirection.x) * Mathf.Rad2Deg;  //get an angle based on direction vector, converted to degrees
        spriteChild.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ - 90);   //update rotation of arrow
    }

    private void UpdateIndicatorPosition()
    {
        Vector3 viewPortPos = mainCam.WorldToViewportPoint(endLevelPos);    //update screenpoint based on world location of endgoal
        if (viewPortPos.x < 0 || viewPortPos.x > 1 || viewPortPos.y < 0 || viewPortPos.y > 1) //if the endlevel goal is off screen either on X or Y axis
        {
            distanceTextChild.gameObject.SetActive(true);       //turn on distance text child while endgoal is off screen
            spriteChild.gameObject.SetActive(true);           //turn on arrow while endgoal is offscreen
            Vector3 clampedArrowScreenPoint = new Vector3(Mathf.Clamp(viewPortPos.x, 0f + arrowPadding, 1f - arrowPadding), 
                                                            Mathf.Clamp(viewPortPos.y, 0f + arrowPadding, 1f - arrowPadding), 
                                                            0f); //clamp arrow position between 0 and 1 substracting an amount of padding from the side
            Vector3 clampedTextScreenPoint = new Vector3(Mathf.Clamp(viewPortPos.x, 0f + textPadding, 1f - textPadding),
                                                Mathf.Clamp(viewPortPos.y, 0f + textPadding, 1f - textPadding),
                                                0f); //clamp text position between 0 and 1 substracting an amount of padding from the side
            spriteChild.position = mainCam.ViewportToScreenPoint(clampedArrowScreenPoint); //apply clamped vector3 to screen coordinates
            distanceTextChild.gameObject.GetComponent<Transform>().position = mainCam.ViewportToScreenPoint(clampedTextScreenPoint); //apply clamped vector3 to screen coordinates
        }
        else
        {
            distanceTextChild.gameObject.SetActive(false);      //turn off distance text child while endgoal is on screen
            spriteChild.gameObject.SetActive(false);          //turn off arrow while endgoal is on screen
        }
    }
}
