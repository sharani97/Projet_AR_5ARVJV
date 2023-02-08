using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    protected Joybutton joybutton;
    protected Joystick joystick;
    protected bool jump = false;
    public float scale_inter = 2f;
    // Start is called before the first frame update
    void Start()
    {
        joybutton= FindObjectOfType <Joybutton>();
        joystick= FindObjectOfType <Joystick>();
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody rb= GetComponent <Rigidbody>();
        rb.velocity= new Vector3(joystick.Horizontal * scale_inter, rb.velocity.y, joystick.Vertical*scale_inter);
        if (!jump && joybutton.pressed)
        {
            jump = true;
            rb.velocity+= Vector3.up * scale_inter;
        }
        if(jump && !joybutton.pressed)
            jump = false;
        
    }
}
