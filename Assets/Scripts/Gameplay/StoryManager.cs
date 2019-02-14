using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoryManager : MonoBehaviour
{
    [SerializeField] private GameObject StoryText;

    private TextMeshProUGUI Text;
    private Image Background;

    // Start is called before the first frame update
    void Start()
    {
        Text = StoryText.GetComponentInChildren<TextMeshProUGUI>();
        Background = StoryText.GetComponent<Image>();
        SetText("What is that..? Something is trying to get to me! I need to get out!", true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetText(string text, bool state)
    {
        Text.text = text;
        Text.enabled = state;
        Background.enabled = state;
    }
}
