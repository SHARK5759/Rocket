using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Rigidbody rigidbody;
    AudioSource audiosource;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>(); //访问游戏中的火箭的Rigidbody，并进入Rigidbody
        audiosource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    private void ProcessInput()
    {
        if (Input.GetKey(KeyCode.Space))   //如果按空格
        {
            rigidbody.AddRelativeForce(Vector3.up);//火箭上升
            if (!audiosource.isPlaying)//保证按空格时声音不会重叠
            {
                audiosource.Play(); 
            }
        }
        else  //不按空格的时候，声音停止
        {
                audiosource.Stop();
        }
        if(Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.back);
        }
    }
}
