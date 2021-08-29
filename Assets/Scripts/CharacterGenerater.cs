using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class CharacterGenerater : MonoBehaviour
{
    public TextMeshProUGUI idCharacterText;
    public TextMeshProUGUI processImportedText;

    public List<InformationBodyPartID> informationBodyPartIDs;

    public CharacterView characterView;

    // Start is called before the first frame update
    void Start()
    {
        ResetCharacter();
    }

    private void Update()
    {
        string faceID = informationBodyPartIDs[0].GetIDFromInputFields();
        string hairID = informationBodyPartIDs[1].GetIDFromInputFields();
        string bodyID = informationBodyPartIDs[2].GetIDFromInputFields();
        string handID = informationBodyPartIDs[3].GetIDFromInputFields();
        string legID = informationBodyPartIDs[4].GetIDFromInputFields();
        string decoID = informationBodyPartIDs[5].GetIDFromInputFields();

        characterView.UpdateView(faceID, hairID, bodyID, handID, legID, decoID);
    }

    public void GenerateRandomCharacter()
    {
        for (int i = 0; i < informationBodyPartIDs.Count; i++)
        {
            informationBodyPartIDs[i].RandomBodyPartID();
        }
    }

    public void ResetCharacter()
    {
        for (int i = 0; i < informationBodyPartIDs.Count; i++)
        {
            informationBodyPartIDs[i].ResetBodyPartID();
        }
    }
}