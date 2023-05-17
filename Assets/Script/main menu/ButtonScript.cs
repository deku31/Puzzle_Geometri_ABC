using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    public SoundManager sfx;
    string _scene;
    public  Animator anim;
    bool _changescene;
    private void Start()
    {
        sfx = FindObjectOfType<SoundManager>();
        _changescene = false;
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            keluar();
        }
        if (_changescene==true)
        {
            if (Input.GetMouseButtonUp(0))
            {
                anim.SetTrigger("OntrigerUp");
                anim.SetBool("Click", false);
                sfx._sfx(0);
                StartCoroutine(ChangeScene(0.7f));
            }
        }
    }
    private void OnMouseDown()
    {
       
    }
    public void navigasi(string SceneTujuan)
    {
        anim.SetBool("Click", true);
        anim.SetTrigger("OnTrigerDown");
        _changescene=true;
        _scene = SceneTujuan;

    }

    public void keluar()
    {
        Application.Quit();
    }
    IEnumerator ChangeScene(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(_scene);
        StopAllCoroutines();
    }
}
