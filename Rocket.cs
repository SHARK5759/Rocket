using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    [SerializeField] float rcsThrust = 100f;//在unity中显示该选项，方便调节
    [SerializeField] float mainThrust = 50f;
    [SerializeField] float levelLoadDelay = 2f;

    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip deathExposion;
    [SerializeField] AudioClip Success;

    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem deathExposionParticle;
    [SerializeField] ParticleSystem SuccessParticle;

    Rigidbody rigidbody;
    AudioSource audiosource;

    enum State { Alive,Dying,Transcending}
    State state = State.Alive;

    bool collisionDisabled = true;
    //public object R { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>(); //访问游戏中的火箭的Rigidbody，并进入Rigidbody
        audiosource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (state == State.Alive)
        {
            RespondToThrustInput();//上升
            RespondToRotateInput();//旋转
        }
        if (Debug.isDebugBuild)
        { 
            RespondDebugKey();
        }            
    }
    private void RespondToThrustInput()
    {
        if (Input.GetKey(KeyCode.Space))   //如果按空格      
        {
            ApplyThrust();
        }
        else  //不按空格的时候，声音停止
        {
            audiosource.Stop();
        }
    }
    private void ApplyThrust()
    {
        rigidbody.AddRelativeForce(Vector3.up * mainThrust);//火箭上升
        if (!audiosource.isPlaying)// 保证按空格时声音不会重叠
        {
            audiosource.PlayOneShot(mainEngine);
        }
        mainEngineParticles.Play();
    }
    private void RespondToRotateInput()
    {
        rigidbody.freezeRotation = true;
        float rotateThisFrame = rcsThrust * Time.deltaTime;//调整转速
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotateThisFrame);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotateThisFrame);
        }
        rigidbody.freezeRotation = false;
    }

    private void RespondDebugKey()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextScene();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            //停止碰撞
            collisionDisabled = !collisionDisabled;//切换碰撞
        }
    }


    void OnCollisionEnter(Collision collision)//每当发生碰撞
    {
        if (state != State.Alive|| !collisionDisabled)//ignore collision when dead
        {
            return;
        }
        switch (collision.gameObject.tag)//如果和unity中某一物体的标签发生碰撞
        {
            case "Friendly"://如果标签是Friendly       
               break;
            case "Finish":
                SuccessSequence();
                break;
            default:
                DeathSequence();
                break;
        }            
    }
    private void SuccessSequence()
    {
        state = State.Transcending;
        audiosource.Stop();
        audiosource.PlayOneShot(Success);
        SuccessParticle.Play();
        Invoke("LoadNextScene", levelLoadDelay);
    }
    private void DeathSequence()
    {
        state = State.Dying;
        audiosource.Stop();
        audiosource.PlayOneShot(deathExposion);
        deathExposionParticle.Play();
        Invoke("LoadCurrentScene", levelLoadDelay);
    }
    void LoadNextScene()//加载下一个关卡
    {
        //todo allow for more than 2 levels
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)//我们处在七个场景中的任何一个场景时
        {
            nextSceneIndex = 0; //回到第一个场景，循环
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
    void LoadCurrentScene()
    {
        //todo
        SceneManager.LoadScene(0);
    }
  
 
}
