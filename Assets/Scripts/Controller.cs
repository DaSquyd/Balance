using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    public TMPro.TextMeshProUGUI titleText;
    public float textShakeIntensity;

    public ParticleSystem particleSystem;

    public Rigidbody2D hotdogRB;

    public Text countdown;
    public Image clock;

    public float speed;

    private float count = 2f;

    private int state = 0;

    void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }

    // Start is called before the first frame update
    void Start()
    {
        titleText.text = "BALANCE!";
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x <= -10f)
            transform.position = new Vector2(9.5f, -1f);
        if (transform.position.x >= 10f)
            transform.position = new Vector2(-9.5f, -1f);

        if (state == 0)
        {
            titleText.rectTransform.localPosition = Random.insideUnitCircle * textShakeIntensity;

            if (count <= 0)
            {
                titleText.enabled = false;
                count = 10f;
                hotdogRB.gravityScale = 0.5f;
                state = 1;
                Input.ResetInputAxes();
            }
        }
        else if (state == 1)
        {
            transform.Translate(Input.GetAxis("Horizontal") * speed, 0f, 0f, Space.World);
            transform.Rotate(0f, 0f, Input.GetAxis("Horizontal") * -10, Space.World);

            countdown.text = Mathf.CeilToInt(count).ToString();
            clock.fillAmount = count / 10f;
            clock.color = Color.HSVToRGB(count / 30f, 1f, 1f);

            if (count <= 0f)
            {
                titleText.text = "SUCCESS!";
                titleText.colorGradient = new TMPro.VertexGradient(Color.cyan, Color.cyan, Color.green, Color.green);
                hotdogRB.AddForce(Vector2.up * 100f);

                titleText.enabled = true;
                state = 2;
                count = 2;
            }
            else if (hotdogRB == null)
            {
                titleText.text = "FAILURE!";
                titleText.colorGradient = new TMPro.VertexGradient(Color.red, Color.red, Color.gray, Color.gray);

                titleText.enabled = true;
                state = 2;
                count = 2;
            }
        }
        else if (state == 2)
        {
            particleSystem.Stop(false, ParticleSystemStopBehavior.StopEmitting);
            titleText.rectTransform.localPosition = Random.insideUnitCircle * textShakeIntensity;

            if (hotdogRB != null)
                hotdogRB.gravityScale = 0f;

            if (count <= 0f)
            {
#if !UNITY_EDITOR
                Application.Quit();
#else
                UnityEditor.EditorApplication.isPlaying = false;
#endif
            }
        }



        if (Input.GetKeyDown(KeyCode.Escape))
#if !UNITY_EDITOR
            Application.Quit();
#else
            UnityEditor.EditorApplication.isPlaying = false;
#endif

        count -= Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        particleSystem.transform.position = new Vector3(collision.GetContact(0).point.x, collision.GetContact(0).point.y, -1);
        if (state == 1)
            particleSystem.Play();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        particleSystem.transform.position = new Vector3(collision.GetContact(0).point.x, collision.GetContact(0).point.y, -1);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        particleSystem.Stop(false, ParticleSystemStopBehavior.StopEmitting);
    }
}
