using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighScoreUI : MonoBehaviour
{
    [SerializeField] GameObject highScoreUIElement;
    [SerializeField] Transform elementWrapper;

    List<GameObject> uiElements = new List<GameObject>();

    private void OnEnable()
    {
        InputHandler.OnListChanged += UpdateUI;
    }

    private void OnDisable()
    {
        InputHandler.OnListChanged -= UpdateUI;
    }

    private void UpdateUI(List<InputEntry> list)
    {
        for(int i = 0; i < list.Count; i++)
        {
            InputEntry temp = list[i];

            if(temp.highScore > 0) 
            {
                if(i >= uiElements.Count)
                {
                    var inst = Instantiate(highScoreUIElement, Vector3.zero, Quaternion.identity);
                    inst.transform.SetParent(elementWrapper, false);

                    uiElements.Add(inst);
                }

                var texts = uiElements[i].GetComponentsInChildren<TextMeshProUGUI>();
                texts[0].text = temp.inputName;
                texts[1].text = temp.highScore.ToString();
            }
        }
    }
}
