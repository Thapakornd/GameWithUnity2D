using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartController : MonoBehaviour
{
    private Animator ami;

    private void Awake()
    {
        ami = GetComponent<Animator>();
    }

    public void OnClick() 
    {
        ami.SetTrigger("IsClick");
        StartCoroutine(delayToScene());
    }

    IEnumerator delayToScene()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Gameplay");
    }

}
