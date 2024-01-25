using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mov3d : MonoBehaviour
{
    // Start is called before the first frame update
  
     public Transform Groundcheck;
    public float groundcheckradius = 0.1f;
    private bool isjumping = false;
    public LayerMask Ground;
    public LayerMask player;
    private bool grounded = true;
    public float jumpamount = 5f;
    public Rigidbody rb;
    public Collider[] collectible1;
    public Collider[] shooter;
    private bool haspwr = false;
    public bool shootactive = false;
    float sped;
    float sped2;
    public GameObject bull;
    public Transform bulletspawn;
    public float speedH = 2.0f; 
    private float yaw = 0.0f;
    
    void Start()
    {
        sped = GetComponent<GlobalValues>().sped;
        sped2 = sped * 2;
    }
    // Update is called once per frame
    void Update()
    {
        if (!haspwr)
        {
            if (Input.GetKey("w"))
            {
                Debug.Log("moving");
                transform.position += transform.TransformDirection(Vector3.forward) * Time.deltaTime * sped;
                GetComponent<Animation>().Play("Run");
            }
            else if (Input.GetKey("s"))
            {
                transform.position += transform.TransformDirection(Vector3.back) * Time.deltaTime * sped;
                GetComponent<Animation>().Play("Run_Back");
            }
            if (Input.GetKey("a"))
            {
                transform.position += transform.TransformDirection(Vector3.left) * Time.deltaTime * sped;
                GetComponent<Animation>().Play("Run_Back");
            }
            else if (Input.GetKey("d"))
            {
                transform.position += transform.TransformDirection(Vector3.right) * Time.deltaTime * sped;
                GetComponent<Animation>().Play("Run_Back");
            }
            else if (Input.GetKeyDown("space"))
            {
                jump();
            }
            else if (!Input.anyKey)
            {
                GetComponent<Animation>().Play("Idle");
            }
            if (Input.GetKey("a") && Input.GetKeyDown("space"))
            {
                transform.position += transform.TransformDirection(Vector3.back) * Time.deltaTime * sped;
                jump();

            }
            if (Input.GetKey("d") && Input.GetKeyDown("space"))
            {
                transform.position += transform.TransformDirection(Vector3.forward) * Time.deltaTime * sped;
                jump();
            }
        }
        if (haspwr)
        {
            if (Input.GetKey("d"))
            {
                Debug.Log("moving");
                transform.position += transform.TransformDirection(Vector3.forward) * Time.deltaTime * sped2;

                GetComponent<Animation>().Play("Run");
            }
            else if (Input.GetKey("a"))
            {
                transform.position += transform.TransformDirection(Vector3.back) * Time.deltaTime * sped2;
                GetComponent<Animation>().Play("Run_Back");
            }
            else if (Input.GetKeyDown("space"))
            {
                jump();
            }
            if (Input.GetKey("a") && Input.GetKeyDown("space"))
            {
                transform.position += transform.TransformDirection(Vector3.back) * Time.deltaTime * sped2;
                jump();

            }
            if (Input.GetKey("d") && Input.GetKeyDown("space"))
            {
                transform.position += transform.TransformDirection(Vector3.forward) * Time.deltaTime * sped2;
                jump();
            }
            else if (!Input.anyKey)
            {
                GetComponent<Animation>().Play("Idle");
            }
        }
        if (shootactive)
        {
            if (Input.GetKeyDown("e"))
            {
                shoot();
            }
        }
        yaw += speedH * Input.GetAxis("Mouse X");

        transform.eulerAngles = new Vector3(0.0f, yaw);
    }
    void jump()
    {
        grounded = true;
        if (Physics.CheckSphere(Groundcheck.position, Ground))
        {
            if (grounded)
            {
                rb.AddForce(Vector2.up * sped * 4, ForceMode.Impulse);
                isjumping = true;
                Debug.Log("isjumping");
                grounded = false;
                wait();
            }
            else if (!grounded)
            {
            }
        }
        else
        {
            grounded = false;
        }
    }
    private IEnumerator wait()
    {
        yield return new WaitForSeconds(2);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(Groundcheck.position, groundcheckradius);
    }
    public void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < collectible1.Length || i < shooter.Length; i++)
        {
            if (other == collectible1[i])
            {
                haspwr = true;
                break;
            }
            else if (other == shooter[i])
            {
                shootactive = true;
                break;
            }
        }
    }
    void shoot()
    {
        bull.SetActive(true);
        Instantiate(bull, bulletspawn.position, bulletspawn.rotation);
        Debug.Log("shooted");
    }
}


