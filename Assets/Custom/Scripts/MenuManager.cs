using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowPanel(GameObject Panel){
        Panel.SetActive(true);
    }

    public void HidePanel(GameObject Panel){
        Panel.SetActive(false);
    }


    public void OpenFBPage(){
        Application.OpenURL("https://www.facebook.com/haptic.team/");
    }

    public void OpenWebPage(){
        Application.OpenURL("https://haptic.team"); 
    }

    public void OpenInstPage(){
        Application.OpenURL("https://www.instagram.com/haptic.team/"); 
    }

   
}
