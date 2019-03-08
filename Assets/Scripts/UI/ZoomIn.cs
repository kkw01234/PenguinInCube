using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ZoomIn : MonoBehaviour, IPointerDownHandler
{
    public GameObject board;
    public GameObject content;
    public GameObject data;

    void Start()
    {
        board.SetActive(false);
        content.SetActive(false);
    }

    public void OnOffWhiteboard()
    {
        if (!board.activeSelf)
        {
            board.SetActive(true);
            content.SetActive(true);
            if (data.GetComponent<TextMesh>())
                content.GetComponent<Text>().text = data.GetComponent<TextMesh>().text;
            else if (data.GetComponent<SpriteRenderer>())
                content.GetComponent<Image>().sprite = data.GetComponent<SpriteRenderer>().sprite;
        }
        else if (board.activeSelf)
        {
            board.SetActive(false);
            content.SetActive(false);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnOffWhiteboard();
    }
}
