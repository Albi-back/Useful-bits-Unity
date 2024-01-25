using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grapple : MonoBehaviour
{
    public GameObject Player;
    private Vector3 dest;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                Debug.Log(hit.transform.name);
                Debug.Log("hit");
                dest = ray.GetPoint(100);
                Player.transform.position += transform.TransformDirection(dest) * Time.deltaTime * 0.5f;

            }
        } 
    }
}
