using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class BodyPartPriority
{
    public string id;
    public int order;

    public BodyPartPriority(string id, int order)
    {
        this.id = id;
        this.order = order;
    }
}

[CreateAssetMenu(fileName = "ConfigBodyPartSprite", menuName = "Scriptable Objects/Set Config Body Part Sprite")]
public class ConfigBodyPartSprite : ScriptableObject
{
    [Header("Standard Config")] public int faceStandardOrder = 300;
    public int hairStandardOrder = 400;
    public int bodyStandardOrder = 100;
    public int handStandardOrder = 500;
    public int legStandardOrder = 200;
    public int decoStandardOrder = 600;

    [Header("Specific Config")] public List<BodyPartPriority> specificBodyPartPriorities = new List<BodyPartPriority>();

    public int GetOrderBodyPartSprite(string id)
    {
        for (int i = 0; i < specificBodyPartPriorities.Count; i++)
        {
            if (id == specificBodyPartPriorities[i].id)
            {
                return specificBodyPartPriorities[i].order;
            }
        }

        var indexBodyPart = CharacterID.GetIndexBasedOnElementPosition(id, ElementIndexOfBodyPartID.BodyPart);

        switch ((BodyPartIndex) indexBodyPart)
        {
            case BodyPartIndex.Face:
                return faceStandardOrder;
            case BodyPartIndex.Hair:
                return hairStandardOrder;
            case BodyPartIndex.Body:
                return bodyStandardOrder;
            case BodyPartIndex.Hand:
                return handStandardOrder;
            case BodyPartIndex.Leg:
                return legStandardOrder;
            case BodyPartIndex.Deco:
                return decoStandardOrder;
            default:
                return 0;
        }
    }
}