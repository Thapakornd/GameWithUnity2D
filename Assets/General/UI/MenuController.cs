using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    private Slider vollumeBar;

    [SerializeField] GameObject vollumeMenu;

    public void OnClickOpen() 
    {
        vollumeMenu?.SetActive(true);
    }

    public void OnClose() 
    {
        vollumeMenu?.SetActive(false);
    }
}
