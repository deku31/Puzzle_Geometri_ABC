using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    public Animator anim;
    public float speed=1;
    bool restart = false;
    public float endingTime=30;

    public string NamaSceneTujuan;

    void Start()
    {
        anim.SetFloat("Speed",speed);
        anim.SetBool("skip", false);

    }
    public void Restart()
    {
        anim.SetBool("Restart", true);
    }
    IEnumerator RestartScene()
    {
        yield return new WaitForSeconds(0.5f);
        anim.Play("Entry");
        StopAllCoroutines();
    }
    // Update is called once per frame
    void Update()
    {
        if (restart==true)
        {
            StartCoroutine(RestartScene());
        }
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetBool("skip", true);
            endingTime = 2;
        }
        if (endingTime<=0)
        {
            //keluar scene
            SceneManager.LoadScene(NamaSceneTujuan);
            Debug.Log("Keluar");
        }
        else
        {
            endingTime -= speed * Time.deltaTime;
        }
    }
}
