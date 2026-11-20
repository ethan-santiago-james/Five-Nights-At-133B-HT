using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CamScript : MonoBehaviour
{
    public Button livingRoomButton;
    public Button hallwayOneButton;
    public Button hallwayTwoButton;
    public Button kitchenButton;

    public GameObject camMenu;

    public GameObject livingRoomCamObject;
     public GameObject hallwayOneCamObject;
      public GameObject hallwayTwoCamObject;
       public GameObject kitchenCamObject;


    private Camera livingRoomCam;
    private  Camera hallwayOneCam;
    private Camera hallwayTwoCam;
    private Camera kitchenCam;

    private Camera currentlyActiveCamera;
    // Start is called before the first frame update
    void Start()
    {
        camMenu.SetActive(false);
        
        livingRoomCam = livingRoomCamObject.GetComponent<Camera>();
        hallwayOneCam = hallwayOneCamObject.GetComponent<Camera>();
        hallwayTwoCam = hallwayTwoCamObject.GetComponent<Camera>();
        kitchenCam = kitchenCamObject.GetComponent<Camera>();

        livingRoomButton.onClick.AddListener(() => makeCameraAppearAtFront(livingRoomCam));
        hallwayOneButton.onClick.AddListener(() => makeCameraAppearAtFront(hallwayOneCam));
        hallwayTwoButton.onClick.AddListener(() => makeCameraAppearAtFront(hallwayTwoCam));
        kitchenButton.onClick.AddListener(() => makeCameraAppearAtFront(kitchenCam));

    }

    void makeCameraAppearAtFront(Camera cam) {
        Debug.Log(cam.depth);
        if(currentlyActiveCamera != null) {

            currentlyActiveCamera.depth = -1f;
        }
        currentlyActiveCamera = cam;
        cam.depth = 1343434f;
    }

    // Update is called once per frame
    void Update()
    {

        if(PlayerState.Instance.isInCamMenu == true) {

            camMenu.SetActive(true);

        } else {

            kitchenCam.depth = -1f;
            hallwayOneCam.depth = -1f;
            hallwayTwoCam.depth = -1f;
            livingRoomCam.depth = -1f;
            camMenu.SetActive(false);

        }
    }
}
