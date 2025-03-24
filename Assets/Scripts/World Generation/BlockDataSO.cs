using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Block Data" ,menuName = "Scriptable Object/Block Data")]
public class BlockDataSO : ScriptableObject
{
    public float textureSizeX, textureSizeY;
    public List<TextureData> textureDataList;
    private void OnValidate()
    {
        for (int i = 0; i < textureDataList.Count; i++)
        {
            textureDataList[i].name = textureDataList[i].blockType.ToString();
        }
    }
}

[Serializable]
public class TextureData
{
    [HideInInspector] public string name;
    public BlockType blockType;
    public Vector2Int up, down, side;
    public bool isSolid = true;
    public bool generatesCollider = true;
}

