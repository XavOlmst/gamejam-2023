using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreUI : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] GameObject highscoreUIElementPrefab;
    [SerializeField] Transform elementWrapper;

    List<GameObject> uiElements = new List<GameObject>();

    private void OnEnable()
    {
        HighscoreHandler.onHighscoreListChanged += UpdateUI;
    }

    private void OnDisable()
    {
        HighscoreHandler.onHighscoreListChanged -= UpdateUI;
    }

    public void ShowPanel()
    {
        panel.SetActive(true);
    }

    public void ClosePanel()
    {
        panel.SetActive(false);
    }

    private void UpdateUI(List<HighscoreElement> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            HighscoreElement element = list[i];

            if (element != null && element.points > 0)
            {
                if (i >= uiElements.Count)
                {
                    // instantiate new entry
                    var inst = Instantiate(highscoreUIElementPrefab, Vector3.zero, Quaternion.identity);
                    inst.transform.SetParent(elementWrapper, false);

                    uiElements.Add(inst);
                }

                // write or overwrite name & points
                var texts = uiElements[i].GetComponentsInChildren<TMP_Text>();
                texts[0].text = element.playerName;
                texts[1].text = element.points.ToString();
            }
        }
    }
}