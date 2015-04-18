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
        bool down = false;

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
                if (down)
                {
                    if (tf.position.y > -5)
                        tf.position -= new Vector3(0, speed * 5 * Time.fixedDeltaTime, 0);
                }
                else if (goingRight)
                {
                    tf.position += new Vector3(speed * Time.fixedDeltaTime, 0, 0);
                    if (endX < tf.position.x) goingRight = false;
                }
                else
                {
                    tf.position = tf.position - new Vector3(speed * Time.fixedDeltaTime, 0, 0);
                    if (tf.position.x < beginX) goingRight = true;
                }
            }
        }

        public void Setup(StageController controller, float y, float beginX, float endX)
        {
            this.stage = controller;
            tf.position = new Vector3(tf.position.x , y + sprite.sprite.texture.height / 200f, 0);
            this.beginX = beginX;
            this.endX = endX;
            down = false;
            Move();
        }

        public void Move()
        {
            sprite.enabled = true;
            tf.position = new Vector3(beginX, tf.position.y, 0);
            moving = true;
        }

        public void Down()
        {
            down = true;
        }

        /// <summary>
        /// ポインタのいる位置が左から何%かを返す
        /// </summary>
        public float PositionToPercent()
        {
            var diff = tf.position.x - beginX;
            var width = endX - beginX;
            return diff / width;
        }

        public float Stop()
        {
            moving = false;
            return 0;
        }
    }
}