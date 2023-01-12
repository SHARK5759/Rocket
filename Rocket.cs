using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField]float rcsThrust = 100f;//在unity中显示该选项，方便调节
    [SerializeField] float mainThrust = 50f;
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
        Thrust();//上升
        Rotate();//旋转
    }

    private void Rotate()
    {
        rigidbody.freezeRotation = true;        
        float rotateThisFrame = rcsThrust * Time.deltaTime;//调整转速
        if (Input.GetKey(KeyCode.A))
        {           
            transform.Rotate(Vector3.forward * rotateThisFrame);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward *rotateThisFrame);
        }
        rigidbody.freezeRotation = false;
    }

    private void Thrust()
    {         
        if (Input.GetKey(KeyCode.Space))   //如果按空格      
        {           
            rigidbody.AddRelativeForce(Vector3.up * mainThrust);//火箭上升
            if (!audiosource.isPlaying)//保证按空格时声音不会重叠
            {
                audiosource.Play();
            }
        }
        else  //不按空格的时候，声音停止
        {
            audiosource.Stop();
        }       
    }
}
