using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Ahoge
{
    public class TextManager : MonoBehaviour
    {
        RectTransform textBoxPanelRTF;
        Text textBox;

        public void Awake()
        {
            Transform canvas = GameObject.Find("/Canvas").transform;
            textBoxPanelRTF = canvas.FindChild("TextBoxPanel").transform as RectTransform;
            textBox = textBoxPanelRTF.FindChild("TextBox").GetComponent<Text>();
        }

        public void Start()
        {

        }

        public void Update()
        {
        }

        public void SetText(string text)
        {
            SetVisible(true);
            textBox.text = text;
        }

        public void SetVisible(bool b)
        {
            Vector2 max;
            if(b)
            {
                max = new Vector2(1, 0.3333f);
            }
            else
            {
                max = new Vector2(0, 0f);
            }
            textBoxPanelRTF.anchorMax = max;
        }
    }
}