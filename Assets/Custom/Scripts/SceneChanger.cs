using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour {

    public Animator animator;

    private string SceneToLoad;
	
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(SceneToLoad);
    }

    public void OnPlayButtonClick()
    {
        FadeToScene("Main");
    }

    public void OnBackButtonClick(){
        FadeToScene("Menu");
    }

    public void FadeToScene(string SceneName)
    {
        SceneToLoad = SceneName;
        animator.SetTrigger("FadeOut");
    }
}
