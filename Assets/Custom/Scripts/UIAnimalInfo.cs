using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIAnimalInfo : MonoBehaviour {

    public Text AnimalName, Description;
	
    public void AnimalInfoUpdate(AnimalInfo newInfo){
        AnimalName.text = newInfo.Name;
        Description.text = newInfo.Description;
    }
}
