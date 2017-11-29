using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LESkinManager : MonoBehaviour {

    List<LESkinCollection> skinCollections = new List<LESkinCollection>();

    // Use this for initialization
    void Start()
    {
        HSkinComponent[] allskins = GetComponentsInChildren<HSkinComponent>();
        foreach (HSkinComponent skin in allskins)
        {
            System.Type skinType = skin.GetSkinType();
            LESkinCollection collection = skinCollections.FindLast(s => s.skinType == skinType);
            if (collection != null)
            {
                collection.skins.Add(skin);
            }
            else
            {
                LESkinCollection newCollection = new LESkinCollection(skinType);
                newCollection.skins.Add(skin);
                skinCollections.Add(newCollection);
            }
        }

        foreach (LESkinCollection collection in skinCollections)
        {
            collection.Init();
        }

    }

    private void OnGUI()
    {
        for (int i = 0; i < skinCollections.Count; i++)
        {
            if (GUILayout.Button(skinCollections[i].skinType.ToString()))
            {
                skinCollections[i].Next();
            }
        }
    }

}

public class LESkinCollection
{
    public System.Type skinType;
    public int currentActiveIndex;
    public List<HSkinComponent> skins;

    public LESkinCollection(System.Type type)
    {
        currentActiveIndex = -1;
        skins = new List<HSkinComponent>();
        skinType = type;
    }

    public void Init()
    {
        //Make sure only one set to active
        for (int i = 0; i < skins.Count; i++)
        {
            if (skins[i].Active)
            {
                if (currentActiveIndex != -1)
                {
                    skins[i].Active = false;
                }
                else
                {
                    currentActiveIndex = i;
                }
            }
        }
    }

    public void Next()
    {
        skins[currentActiveIndex].Active = false;
        currentActiveIndex += 1;
        currentActiveIndex %= skins.Count;
        skins[currentActiveIndex].Active = true;
    }
}
