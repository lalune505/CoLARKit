using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GenerateImageAnchorCustom : MonoBehaviour
{
    //imageAnchorGO = Instantiate<GameObject>(prefabToGenerate, position + prefabToGenerate.transform.position,
    // prefabToGenerate.transform.rotation);

    public GameObject QRPanel;

    public List<GameObject> prefabsToGenerate;

    public List<ARReferenceImage> referenceImages;

    [HideInInspector]
    public string refImageName = "";

    public GameObject Button;

    public GameObject TakePhotoButton;

    public GameObject QRButton;

    public bool prefabNotGenerated = true;


    public GameObject imageAnchorGO;

    // Use this for initialization
    void Start()
    {
        UnityARSessionNativeInterface.ARImageAnchorAddedEvent += AddImageAnchor;
        UnityARSessionNativeInterface.ARImageAnchorRemovedEvent += RemoveImageAnchor;

        Debug.Log("Camera position: " + Camera.main.transform.position);

    }

    void AddImageAnchor(ARImageAnchor arImageAnchor)
    {
        Debug.Log("image anchor added");
        if (prefabsToGenerate.Exists(e => e.name.Equals(arImageAnchor.referenceImageName)) && prefabNotGenerated)
        {
            Vector3 position = UnityARMatrixOps.GetPosition(arImageAnchor.transform);
            Quaternion rotation = UnityARMatrixOps.GetRotation(arImageAnchor.transform);

            refImageName = arImageAnchor.referenceImageName;


            GameObject prefabToGenerate = getPrefab(prefabsToGenerate, refImageName);

            imageAnchorGO = Instantiate<GameObject>(prefabToGenerate, position + prefabToGenerate.transform.position,
                                                    prefabToGenerate.transform.rotation);

            QRPanel.SetActive(false);

            Button.SetActive(true);

            TakePhotoButton.SetActive(true);

            QRButton.SetActive(true);
            
            prefabNotGenerated = false;
        }
    }

  

    void RemoveImageAnchor(ARImageAnchor arImageAnchor)
    {
        Debug.Log("image anchor removed");
        if (imageAnchorGO)
        {
            GameObject.Destroy(imageAnchorGO);
        }

    }

    void OnDestroy()
    {
        UnityARSessionNativeInterface.ARImageAnchorAddedEvent -= AddImageAnchor;
        UnityARSessionNativeInterface.ARImageAnchorRemovedEvent -= RemoveImageAnchor;

    }

    // Update is called once per frame
    void Update()
    {
    }

    private GameObject getPrefab(List<GameObject> prefabs,string refName)
    {
        return prefabs.Find(x => x.name.Equals(refName));
    }

    public void LoadMenuAndResetTracking()
    {
        GameObject.Destroy(imageAnchorGO);
        SceneManager.LoadScene("Menu");

    }
}
