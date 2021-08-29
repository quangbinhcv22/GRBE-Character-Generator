using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Random = UnityEngine.Random;

[System.Serializable]
public class InputFieldsBodyPartID
{
    public TMP_InputField inputField;
    public bool block;
}

public class InformationBodyPartID : MonoBehaviour
{
    public const int SumElement = 5;
    public const int SumBodyPart = 6;
    public const int SumRare = 1; //6;
    public const int SumSkin = 1; //9;

    [Header("General")] public BodyPartIndex bodyPartID;
    public TextMeshProUGUI idBodyPartText;
    public TextMeshProUGUI descriptionText;
    public Image bodyPartImage;

    private bool blockRandom;

    [Header("Input Field")] public List<InputFieldsBodyPartID> inputFieldsBodyPartID;

    void Start()
    {
        descriptionText.text = $"{bodyPartID.ToString()} ID";
        idBodyPartText.text = ((int) bodyPartID).ToString();
    }

    private void Update()
    {
        SetBodyPartImage();
    }

    public void RandomBodyPartID()
    {
        if (blockRandom == false)
        {
            int RandomElement = Random.Range(1, SumElement + 1);
            int RandomRare = Random.Range(1, SumRare + 1);
            int RandomSkin = Random.Range(1, SumSkin + 1);

            SetInputFieldText(RandomElement.ToString(), RandomRare.ToString(), RandomSkin.ToString());
        }
    }

    public void ResetBodyPartID()
    {
        if (blockRandom == false)
        {
            SetInputFieldText("5", "1", "1");
        }
    }

    public string GetIDFromInputFields()
    {
        return
            $"{inputFieldsBodyPartID[0].inputField.text}{(int) bodyPartID}" +
            $"{inputFieldsBodyPartID[1].inputField.text}{inputFieldsBodyPartID[2].inputField.text}";
    }

    public void BlockOrUnBlockRandome(Image buttonBlockRandomImage)
    {
        blockRandom = !blockRandom;
        buttonBlockRandomImage.GetComponent<Image>().sprite =
            blockRandom ? Resources.Load<Sprite>("UI/Block") : Resources.Load<Sprite>("UI/UnBlock");
    }

    public void SetBodyPartImage()
    {
        bodyPartImage.sprite = Resources.Load<Sprite>($"Body Part/{GetIDFromInputFields()}");
    }

    public void SetInputFieldText(string IDElement, string IDRare, string IDSkin)
    {
        inputFieldsBodyPartID[0].inputField.text = IDElement;
        inputFieldsBodyPartID[1].inputField.text = IDRare;
        inputFieldsBodyPartID[2].inputField.text = IDSkin;
    }

    public void SetInputFieldText(string IDBodyPart)
    {
        inputFieldsBodyPartID[0].inputField.text = IDBodyPart.Substring(0, 1);
        inputFieldsBodyPartID[1].inputField.text = IDBodyPart.Substring(2, 1);
        inputFieldsBodyPartID[2].inputField.text = IDBodyPart.Substring(3, 1);
    }
}