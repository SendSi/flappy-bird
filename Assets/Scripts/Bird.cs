using UnityEngine;

public class Bird : MonoBehaviour
{
    public float timer = 0;
    public int frameNumber = 10;// frame number one seconds
    public int frameCount = 0;//  frame counter

    public bool canJump = false;
    private Rigidbody mBridBody;
    private AudioSource mBridAudio;
    private Material mBridMaterial;

    void Start()
    {
        mBridBody = this.GetComponent<Rigidbody>();
        mBridAudio = this.GetComponent<AudioSource>();
        mBridMaterial = this.GetComponent<Renderer>().material;
        EventCenter.GetInstance().Bind(EventName.EN_getLife, OnEventGetLife);
    }

    void Update()
    {
        if (GameManager._intance.GameState == GameManager.GAMESTATE_PLAYING)
        {
            timer += Time.deltaTime;
            if (timer >= 1.0f / frameNumber)
            {
                frameCount++;
                timer -= 1.0f / frameNumber;
                int frameIndex = frameCount % 3;
                mBridMaterial.SetTextureOffset("_MainTex", new Vector2(0.333333f * frameIndex, 0));
            }
        }

        if (GameManager._intance.GameState == GameManager.GAMESTATE_PLAYING)
        {         
            if (Input.GetMouseButton(0))
            {
                mBridAudio.Play();
                Vector3 vel2 = mBridBody.velocity;
                mBridBody.velocity = new Vector3(vel2.x, 5, vel2.z);
            }
        }
    }

    private void OnEventGetLife()
    {
        mBridBody.useGravity = true;
        mBridBody.velocity = new Vector3(2, 0, 0);
    }


    private void OnDestroy()
    {
        EventCenter.GetInstance().UnBind(EventName.EN_getLife, OnEventGetLife);
    }
}
