using UnityEngine;
using System.Collections;

namespace Ahoge
{
    public class PointerController : MonoBehaviour
    {
        Transform tf;

        SpriteRenderer sprite;

        StageController stage;

        public float speed;
        float beginX;
        float endX;

        bool goingRight = true;
        bool moving = false;

        public void Awake()
        {
            tf = this.transform;
            sprite = this.GetComponent<SpriteRenderer>();
            sprite.enabled = false;
        }

        public void Start()
        {

        }

        public void Update()
        {
            //往復運動
            if (moving)
            {
                if (goingRight)
                {
                    tf.position += new Vector3(speed * Time.fixedDeltaTime, 0, 0);
                    if (endX < tf.position.x) goingRight = false;
                }
                else
                {
                    tf.position -= new Vector3(speed * Time.fixedDeltaTime, 0, 0);
                    if (tf.position.x < beginX) goingRight = true;
                }
            }
        }

        public void Setup(StageController controller)
        {
            this.stage = controller;
            Move();
        }

        public void Move()
        {
            sprite.enabled = true;
            tf.position = new Vector3(beginX, tf.position.y, 0);
            moving = true;
        }

        public float Stop()
        {
            moving = false;
            return 0;
        }
    }
}