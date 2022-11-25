using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float speed;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb =GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            rb.velocity = Vector3.up * speed;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            rb.velocity = Vector3.down * speed;

        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.velocity = Vector3.forward * speed;

        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.velocity = -Vector3.forward * speed;

        }
        else if (Input.GetKey(KeyCode.S))
        {
            rb.velocity = Vector3.left * speed;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            rb.velocity = Vector3.right * speed;
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }
}
