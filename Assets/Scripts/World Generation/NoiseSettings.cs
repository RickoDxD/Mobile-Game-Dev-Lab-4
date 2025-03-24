using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "noiseSettings", menuName = "Scriptable Object/Noise Settings")]
public class NoiseSettings : ScriptableObject
{
    public float noiseZoom;
    public int octaves;
    public Vector2Int offest;
    public Vector2Int worldOffset;
    public float persistance;
    public float redistributionModifier;
    public float exponent;
}
