using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ApparelType { HEAD, TORSO, LEGS, FEET };

[CreateAssetMenu(fileName = "Apparel", menuName = "SÖ/Apparel")]
public class Apparel : ScriptableObject
{
    [Header("Wearing Variables")]
    public ApparelType type;
    public Sprite downSide;
    public Sprite rightSide;
    public Sprite upSide;

    [Header("Other Data")]
    public int id;
    public int price;
    public string apparelName;
    public Sprite icon;
}
