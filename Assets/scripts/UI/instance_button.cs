using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public abstract class instance_button : MonoBehaviour
    , IPointerEnterHandler
    , IPointerExitHandler
{
    
    public Text priceText;
    public GameObject infoWindow;
    public Text nameText;
    public Text typeText;
    public Text descText;
    public GameObject prefab;
    public string name;
    public string desciption = "";
    public int amount;
    public float radius = 4;
    public int price = 0;
    protected InstanceGenerator generator;
    protected Resources_Controller resControl;

    Color defaultColor;
    public Color selectedColor;

    // Use this for initialization
    protected void Start()
    {
        generator = GameObject.Find("InstanceGenerator").GetComponent<InstanceGenerator>();
        resControl = GameObject.Find("MapController").GetComponent<Resources_Controller>();
        priceText.text = price.ToString();

        defaultColor = GetComponent<Button>().colors.normalColor;
    }

    // Update is called once per frame
    protected void Update()
    {
        Button button = GetComponent<Button>();
        if (price > resControl.getBiomass())
        {
            button.interactable = false;
        }
        else
        {
            button.interactable = true;
        }
    }

    public virtual void select_instance()
    {
        changeColor(selectedColor);
        generator.select(prefab, amount, radius, price, 0, this);
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        infoWindow.SetActive(true);
        nameText.text = name;
        this.setText();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        infoWindow.SetActive(false);

    }

    public void Deselect()
    {
        changeColor(defaultColor);
    }

    private void changeColor(Color color)
    {
        ColorBlock colors = GetComponent<Button>().colors;
        colors.normalColor = color;
        GetComponent<Button>().colors = colors;
    }

    public abstract void setText();
}
