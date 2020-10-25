using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controller : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject Verb;
    public GameObject Subject;
    public GameObject Object;

    void Start()
    {

        Verb = Resources.Load("Verb") as GameObject;
        Subject = Resources.Load("Subject") as GameObject;
        Object = Resources.Load("Object") as GameObject;



    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Instantiate(Verb,new Vector3(0,0,0),Quaternion.identity);
            Instantiate(Subject, new Vector3(1, 0, 0), Quaternion.identity);
            Instantiate(Object, new Vector3(2, 0, 0), Quaternion.identity);
        }

        
    }
}
