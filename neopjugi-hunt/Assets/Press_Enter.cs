using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Press_Enter : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Update");
        changeScene();
    }

    void changeScene()
    {
        Debug.Log("ChangeScene");

        if (Input.GetKey("space"))
        {
            Debug.Log("Thank you very much");
        }
    }
}
