using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;
using UnityEngine.SceneManagement;
using System.IO;
using NatShareU;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class Manager : MonoBehaviour
{

    public UnityARCameraManager MAR;

    public GenerateImageAnchorCustom _GenerateImageAnchor;

    public List<AnimalInfo> _AnimalInfo;

    public UIAnimalInfo AnimalInfoContainer;

    public GameObject AnimalInfoPanel;

    public List<GameObject> AnimalInfoList;

    public GameObject ButtonBack;

    public GameObject InfoButton;

    public GameObject QRPanel;

    public GameObject TakePhotoButton;

    public GameObject QRButton;

    public GameObject AnimalInfoButton;

    public Flash _Flash;

    public GameObject InfoPanel;

    private bool notTouched = true;

    [HideInInspector]
    public Texture2D screenImage;

    [HideInInspector]
    public GenerateImageAnchorCustom GIA;
    // Use this for initialization
    void Start()
    {
        GIA = FindObjectOfType<GenerateImageAnchorCustom>();
    }

    // Update is called once per frame
    void Update()
    {
        if (notTouched)
        {
            if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                InfoPanel.SetActive(false);
                AnimalInfoButton.SetActive(false);
                notTouched = false;

            }
        }
    }

    public void LoadMenuAndResetTracking()
    {
        SceneManager.LoadScene("Menu");

    }

    public void ShowPanel(GameObject Panel)
    {
        Panel.SetActive(true);
    }

    public void HidePanel(GameObject Panel)
    {
        Panel.SetActive(false);
    }

    public void ShowAnimalInfo()
    {
        foreach (var item in AnimalInfoList)
        {
            if (item.name == GIA.refImageName)
            {
                item.SetActive(true);
                AnimalInfoPanel.SetActive(true);
            }
        }
    }

    public void CloseAnimalInfo()
    {
        AnimalInfoPanel.SetActive(false);
    }

    public void ResetSceneConfig(){
     

        ARKitWorldTrackingSessionConfiguration config = new ARKitWorldTrackingSessionConfiguration();
        config.planeDetection = MAR.planeDetection;
        config.alignment = MAR.startAlignment;
        config.getPointCloudData = MAR.getPointCloud;
        config.enableLightEstimation = MAR.enableLightEstimation;
        config.enableAutoFocus = MAR.enableAutoFocus;
        if (MAR.detectionImages != null)
        {
            config.arResourceGroupName = MAR.detectionImages.resourceGroupName;
        }

            //m_session.RunWithConfig (config);
        UnityARSessionNativeInterface.GetARSessionNativeInterface().RunWithConfigAndOptions(config, UnityARSessionRunOption.ARSessionRunOptionRemoveExistingAnchors | UnityARSessionRunOption.ARSessionRunOptionResetTracking);
          
    }

    public void LoadScanQR(){
        
        if (_GenerateImageAnchor.imageAnchorGO)
        {
            GameObject.Destroy(_GenerateImageAnchor.imageAnchorGO);
        }
        _GenerateImageAnchor.prefabNotGenerated = true;
        AnimalInfoList.ForEach((item) => item.SetActive(false));
        CloseAnimalInfo();
        ResetSceneConfig();

        QRPanel.SetActive(true);
        InfoButton.SetActive(false);
        TakePhotoButton.SetActive(false);
        QRButton.SetActive(false);
    }

  

    public void TakeScreenShot(){
        _Flash.CameraFlash();
        StartCoroutine(CaptureScreenshot());  

    }


    IEnumerator CaptureScreenshot()
    {
        yield return null;
        GameObject.Find("MainCanvas").GetComponent<Canvas>().enabled = false;
        //Wait for end of frame
        yield return new WaitForEndOfFrame();

        screenImage = new Texture2D(Screen.width, Screen.height);
      
        //Get Image from screen
        screenImage.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenImage.Apply();

        NatShare.SaveToCameraRoll(screenImage);

        GameObject.Find("MainCanvas").GetComponent<Canvas>().enabled = true; 

        Texture2D.Destroy(screenImage);

    }

    public void HideInformationPanel(){
        
    }


}
    [System.Serializable]
    public class AnimalInfo 
    {
        public string IDName;
        public string Name;
        public string Description;
        
    }
        


