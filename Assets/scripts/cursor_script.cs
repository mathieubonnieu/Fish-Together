using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;


public class cursor_script : MonoBehaviour
{
    public float rotationSpeed;
    private Image circle;
    private int circle_radius = 85;
    private int little_circle_radius = 15;
    int life_fish;
    PhotonView view;
    private fish_script fish_script;
    private GameObject Le_fish;
    private float timer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        transform.name = "cursor";
        transform.parent = GameObject.Find("mini_game").transform.Find("Canvas");
        Le_fish = transform.parent.GetChild(0).Find("fish").gameObject;
        fish_script = transform.parent.GetChild(0).Find("fish").GetComponent<fish_script>();
        circle = transform.parent.GetChild(0).Find("water").GetComponent<Image>();
        view = GetComponent<PhotonView>();
        
    }

    // Update is called once per frame
    void Update()
    {
        try {
            timer += Time.deltaTime;

            if (timer >= 0.1f)
            {
                timer = 0f;
                if (ImagesTouching(Le_fish.GetComponents<RectTransform>()[0], transform.GetComponents<RectTransform>()[0]))
                {
                    fish_script.life = fish_script.life + 1;
                }
                else
                {
                    fish_script.life = fish_script.life - 1;
                }
            }

            //Vector3 mousePos = Input.mousePosition;
            if (view.IsMine)
            {
                RectTransform canvasRect = circle.canvas.GetComponent<RectTransform>();
                Vector2 mousePos;   
                RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, Input.mousePosition, circle.canvas.worldCamera, out mousePos);


                Vector2 circleCenter = circle.rectTransform.anchoredPosition;
                Vector2 direction = mousePos - circleCenter;
                transform.Rotate(new Vector3(0, 0, 1), rotationSpeed * Time.deltaTime);
                float distance = direction.magnitude;
                if (distance <= (circle_radius - little_circle_radius))
                {
                    transform.position = circle.rectTransform.TransformPoint(mousePos);

                }
                else
                {
                    float angle = Mathf.Atan2(direction.y, direction.x);
                    Vector2 newPosition = new Vector2(circleCenter.x + Mathf.Cos(angle) * (circle_radius - little_circle_radius),
                        circleCenter.y + Mathf.Sin(angle) * (circle_radius - little_circle_radius));
                    transform.position = circle.rectTransform.TransformPoint(newPosition);
                }
            }
        } catch {

        }

        bool ImagesTouching(RectTransform img1, RectTransform img2)
        {
            Vector2 img1_scale = new Vector2(img1.rect.width, img1.rect.height);
            Vector2 img2_scale = new Vector2(img2.rect.width, img2.rect.height);


            float img1Min_x = img1.anchoredPosition.x - (img1_scale.x / 2);
            float img1Max_x = img1.anchoredPosition.x + (img1_scale.x / 2);

            float img1Min_y = img1.anchoredPosition.y - (img1_scale.y / 2);
            float img1Max_y = img1.anchoredPosition.y + (img1_scale.y / 2);

            float img2Min_x = img2.anchoredPosition.x - (img2_scale.x / 2);
            float img2Max_x = img2.anchoredPosition.x + (img2_scale.x / 2);

            float img2Min_y = img2.anchoredPosition.y - (img2_scale.y / 2);
            float img2Max_y = img2.anchoredPosition.y + (img2_scale.y / 2);

            bool touchingOnX = !(img1Max_x < img2Min_x || img1Min_x > img2Max_x);
            bool touchingOnY = !(img1Max_y < img2Min_y || img1Min_y > img2Max_y);

            return touchingOnX && touchingOnY;

        }
    }
}
