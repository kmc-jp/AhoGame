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
            textBox.text = text;
            print("SetText");
        }
    }
}