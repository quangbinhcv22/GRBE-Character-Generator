using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

public static class CharacterID
{
    public static int GetIndexBasedOnElementPosition(string spriteID, ElementIndexOfBodyPartID elementIndex)
    {
        return Convert.ToInt32(spriteID.Substring((int) elementIndex, 1));
    }
}

[DefaultExecutionOrder(1)]
public class SpriteManager : MonoBehaviour
{
    public static SpriteManager Instance;

    private const string LinkResourceSprite = "Body Part";

    public Dictionary<BodyPartIndex, Dictionary<string, Sprite>> allSpritesExist;


    void Awake()
    {
        Instance = this;

        int sumBodyPartIndex = 6;
        allSpritesExist = new Dictionary<BodyPartIndex, Dictionary<string, Sprite>>();
        for (int i = 1; i <= sumBodyPartIndex; i++)
        {
            allSpritesExist.Add((BodyPartIndex) i, new Dictionary<string, Sprite>());
        }

        LoadAllSpriteExist();
    }

    private void LoadAllSpriteExist()
    {
        Sprite[] allBodyPartSprites = Resources.LoadAll<Sprite>(LinkResourceSprite);

        foreach (var bodyPartSprite in allBodyPartSprites)
        {
            string spriteID = bodyPartSprite.name;
            int bodyPartIndex = CharacterID.GetIndexBasedOnElementPosition(spriteID, ElementIndexOfBodyPartID.BodyPart);

            allSpritesExist[(BodyPartIndex) bodyPartIndex].Add(spriteID, bodyPartSprite);
        }
    }

    public Sprite GetSpriteBasedOnID(string bodyPartID)
    {
        int bodyPartIndex = CharacterID.GetIndexBasedOnElementPosition(bodyPartID, ElementIndexOfBodyPartID.BodyPart);
        return allSpritesExist[(BodyPartIndex) bodyPartIndex][bodyPartID];
    }

    public void CombinationAllCharacterCaseAndExecuteAction(CharacterView characterView)
    {
        ImageExporter imageExporter = FindObjectOfType<ImageExporter>().GetComponent<ImageExporter>();
        StartCoroutine(CombinationAllCharacterCaseAndExport(characterView, imageExporter));
    }

    public IEnumerator CombinationAllCharacterCaseAndExport(CharacterView characterView, ImageExporter imageExporter)
    {
        System.Diagnostics.Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        int allCase = allSpritesExist[BodyPartIndex.Face].Count;

        foreach (var faceID in allSpritesExist[BodyPartIndex.Face])
        {
            foreach (var hairID in allSpritesExist[BodyPartIndex.Hair])
            {
                foreach (var bodyID in allSpritesExist[BodyPartIndex.Body])
                {
                    foreach (var handID in allSpritesExist[BodyPartIndex.Hand])
                    {
                        foreach (var legID in allSpritesExist[BodyPartIndex.Leg])
                        {
                            foreach (var decoID in allSpritesExist[BodyPartIndex.Deco])
                            {
                                yield return new WaitForSeconds(float.Epsilon);
                                characterView.UpdateView(faceID.Key, hairID.Key, bodyID.Key, handID.Key, legID.Key,
                                    decoID.Key);

                                string nameImage =
                                    $"{faceID.Key}-{hairID.Key}-{bodyID.Key}-{handID.Key}-{legID.Key}-{decoID.Key}";

                                imageExporter.ScreenShotAndExportFile(nameImage, 512);
                            }
                        }
                    }
                }
            }
        }

        stopwatch.Stop();
        Debug.Log($"Done Export with {stopwatch.Elapsed.TotalSeconds} seconds");
    }
}