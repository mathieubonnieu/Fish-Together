using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class fish_script : MonoBehaviour
{
    private float speed;
    public Image circle;
    private int circle_radius = 85;
    private int little_circle_radius = 15;
    private Vector2 go_to;
    private Vector2 go_to2;
    private bool isMoving = false;
    private RectTransform canvasRect;
    public int life = 35;
    // Start is called before the first frame update
    void Start()
    {
        canvasRect = circle.canvas.GetComponent<RectTransform>();
        GenerateRandomDestination();
        GetComponent<RectTransform>().anchoredPosition = go_to;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("go_to: " + go_to + ", transform.position: " + go_to2 );
        if (!isMoving)
        {
            GenerateRandomDestination();
            isMoving = true;
        }

        if (isMoving)
        {
            Vector2 direction = go_to - GetComponent<RectTransform>().anchoredPosition;

            if (direction.magnitude < 0.1f)
            {
                isMoving = false;
            }
            else
            {
                float step = speed * Time.deltaTime;
                GetComponent<RectTransform>().anchoredPosition = Vector2.MoveTowards(GetComponent<RectTransform>().anchoredPosition, go_to, step);
            }
        }
    }

    void GenerateRandomDestination()
    {
        float radius = Random.Range(0, (circle_radius - little_circle_radius));
        float angle = Random.Range(0, 360); // Angle en degrés




        // pos actuel pos dest




        float x = radius * Mathf.Cos(angle * Mathf.Deg2Rad);
        float y = radius * Mathf.Sin(angle * Mathf.Deg2Rad);

        go_to = new Vector2(x, y);

        Vector2 my_pos = new Vector2(GetComponent<RectTransform>().anchoredPosition.x, GetComponent<RectTransform>().anchoredPosition.y);
        Vector2 direction = go_to - my_pos;
        float angleInDegrees = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //Debug.Log(angleInDegrees + " " + my_pos + " " + go_to);

        Quaternion newRotation = Quaternion.Euler(0, 0, angleInDegrees - 90);
        GetComponent<RectTransform>().rotation = newRotation;


        speed = Random.Range(70, 130);
    }
}