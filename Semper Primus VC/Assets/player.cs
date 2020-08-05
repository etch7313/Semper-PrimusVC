using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject chest;
    public GameObject head;
    public GameObject aimat;
    float rotate = 45f;
    Animator anim;
    GameObject headpivot;

    //recordables
    public Quaternion aimdirection;
    public bool forward;
    public bool backward;
    public bool right;
    public bool left;
    void Start()
    {
        anim = GetComponent<Animator>();
        headpivot = new GameObject();
        aimat.transform.parent = headpivot.transform;
        aimat.transform.localPosition = new Vector3(0, 0, 2.7f);

        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        chest.transform.LookAt(aimat.transform.position);
        chest.transform.Rotate(new Vector3(0, rotate, 0));
        head.transform.LookAt(aimat.transform.position);
    }
    private void Update()
    {
        this.transform.LookAt(aimat.transform);
        this.transform.rotation = Quaternion.Euler(0, this.transform.eulerAngles.y, 0);
        headpivot.transform.position = head.transform.position;
        if (Input.GetKeyDown(KeyCode.W))
        {
            forward = true;
            backward = false;
            movement();

        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            forward = false;
            movement();
        }
        mouserotation();
    }
    void movement()
    {
        if(forward)
        {
            anim.SetBool("moving", true);
            anim.SetBool("forward", true);
        }
        else
        {
            anim.SetBool("moving", false);
            anim.SetBool("forward", false);
        }
    }
    float xRotation = 0;
    void mouserotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * 150f * Time.deltaTime;
        float mouseY = -Input.GetAxis("Mouse Y") * 250f * Time.deltaTime;
        //mouseY = Mathf.Clamp(mouseY, Mathf.pi)

        headpivot.transform.Rotate(Vector3.up * mouseX);
        headpivot.transform.Rotate(Vector3.right * mouseY);

        // e3mel hena y limit el camera angle on its local X axis 

    }
}
