using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{

    public Text countdown;
    public Image clock;

    public float speed;

    private float count = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Input.GetAxis("Horizontal") * speed, 0f, 0f, Space.World);
        transform.Rotate(0f, 0f, Input.GetAxis("Horizontal") * -10, Space.World);

        countdown.text = Mathf.CeilToInt(10f - Time.time).ToString();
        clock.fillAmount = count / 10f;
        clock.color = Color.Lerp(Color.green, Color.red, count / 10f);

        count -= Time.deltaTime;

        

        if (Input.GetKeyDown(KeyCode.Escape))
#if !UNITY_EDITOR
            Application.Quit();
#else
            UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
