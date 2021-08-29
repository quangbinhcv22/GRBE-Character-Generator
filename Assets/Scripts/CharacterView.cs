using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterView : MonoBehaviour
{
    public ConfigBodyPartSprite configBodyPartSprite;

    [Header("Images")] public Image faceImage;
    public Image hairImage;
    public Image bodyImage;
    public Image handImage;
    public Image legImage;
    public Image decoImage;

    [Header("Text")] public TextMeshProUGUI characterIDText;

    public void Start()
    {
        configBodyPartSprite = Resources.Load<ConfigBodyPartSprite>("ConfigBodyPartSprite");
    }

    public void UpdateView(string faceID, string hairID, string bodyID, string handID, string legID, string decoID)
    {
        faceImage.sprite = SpriteManager.Instance.GetSpriteBasedOnID(faceID);
        hairImage.sprite = SpriteManager.Instance.GetSpriteBasedOnID(hairID);
        bodyImage.sprite = SpriteManager.Instance.GetSpriteBasedOnID(bodyID);
        handImage.sprite = SpriteManager.Instance.GetSpriteBasedOnID(handID);
        legImage.sprite = SpriteManager.Instance.GetSpriteBasedOnID(legID);
        decoImage.sprite = SpriteManager.Instance.GetSpriteBasedOnID(decoID);

        characterIDText.text = $"{faceID}-{hairID}-{bodyID}-{handID}-{legID}-{decoID}";

        // change index children based on ID
        List<BodyPartPriority> bodyPartPriorities = new List<BodyPartPriority>();

        bodyPartPriorities.Add(new BodyPartPriority(faceID, configBodyPartSprite.GetOrderBodyPartSprite(faceID)));
         bodyPartPriorities.Add(new BodyPartPriority(hairID, configBodyPartSprite.GetOrderBodyPartSprite(hairID)));
         bodyPartPriorities.Add(new BodyPartPriority(bodyID, configBodyPartSprite.GetOrderBodyPartSprite(bodyID)));
         bodyPartPriorities.Add(new BodyPartPriority(handID, configBodyPartSprite.GetOrderBodyPartSprite(handID)));
         bodyPartPriorities.Add(new BodyPartPriority(legID, configBodyPartSprite.GetOrderBodyPartSprite(legID)));
         bodyPartPriorities.Add(new BodyPartPriority(decoID, configBodyPartSprite.GetOrderBodyPartSprite(decoID)));
        
         for (int i = 0; i < bodyPartPriorities.Count; i++)
         {
             for (int j = i + 1; j < bodyPartPriorities.Count; j++)
             {
                 if (bodyPartPriorities[i].order > bodyPartPriorities[j].order)
                 {
                     BodyPartPriority bodyPartPriorityTemponary = bodyPartPriorities[i];
                     bodyPartPriorities[i] = bodyPartPriorities[j];
                     bodyPartPriorities[j] = bodyPartPriorityTemponary;
                 }
             }
         }
        
         for (int i = 0; i < bodyPartPriorities.Count; i++)
         {
             var bodyPartIndex =
                 CharacterID.GetIndexBasedOnElementPosition(bodyPartPriorities[i].id, ElementIndexOfBodyPartID.BodyPart);
        
             switch ((BodyPartIndex) bodyPartIndex)
             {
                 case BodyPartIndex.Face:
                     faceImage.gameObject.transform.SetSiblingIndex(i);
                     break;
                 case BodyPartIndex.Hair:
                     faceImage.gameObject.transform.SetSiblingIndex(i);
                     break;
                 case BodyPartIndex.Body:
                     bodyImage.gameObject.transform.SetSiblingIndex(i);
                     break;
                 case BodyPartIndex.Hand:
                     handImage.gameObject.transform.SetSiblingIndex(i);
                     break;
                 case BodyPartIndex.Leg:
                     legImage.gameObject.transform.SetSiblingIndex(i);
                     break;
                 case BodyPartIndex.Deco:
                     decoImage.gameObject.transform.SetSiblingIndex(i);
                     break;
             }
         }
    }

    private const float RatioBetweenBackgroundAndElements = 512f / 400f;

    public void ChangeSize(int newSize)
    {
        var sizeBackground = new Vector2(newSize, newSize);
        var sizeComponents = new Vector2(newSize, newSize) * RatioBetweenBackgroundAndElements;
        gameObject.GetComponent<RectTransform>().sizeDelta = sizeBackground;
        faceImage.GetComponent<RectTransform>().sizeDelta = sizeComponents;
        hairImage.GetComponent<RectTransform>().sizeDelta = sizeComponents;
        bodyImage.GetComponent<RectTransform>().sizeDelta = sizeComponents;
        handImage.GetComponent<RectTransform>().sizeDelta = sizeComponents;
        legImage.GetComponent<RectTransform>().sizeDelta = sizeComponents;
        decoImage.GetComponent<RectTransform>().sizeDelta = sizeComponents;
    }
}