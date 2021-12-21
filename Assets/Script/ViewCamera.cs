using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewCamera : MonoBehaviour
{

    [SerializeField] private Camera debugCam;
    [SerializeField] private Camera mainCam;
    [SerializeField] private Camera menuCam;

    // Call this function to disable FPS camera,
    // and enable overhead camera.
    public void ShowMainCamView()
    {
        menuCam.enabled = false;
        debugCam.enabled = false;
        mainCam.enabled = true;
    }

    // Call this function to enable FPS camera,
    // and disable overhead camera.
    public void ShowDebugCamView()
    {
        mainCam.enabled = false;
        menuCam.enabled = false;
        debugCam.enabled = true;
    }

    public void ShowMenuCamView()
    {
        debugCam.enabled = false;
        mainCam.enabled = false;
        menuCam.enabled = true;        
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        //Debug
        if (Input.GetKeyDown(KeyCode.P))
            ShowDebugCamView();
#endif
        //Main
        if (Input.GetKeyDown(KeyCode.M))
            ShowMainCamView();

        //Menu
        if (Input.GetKeyDown(KeyCode.Escape))
            ShowMenuCamView();
    }
}
