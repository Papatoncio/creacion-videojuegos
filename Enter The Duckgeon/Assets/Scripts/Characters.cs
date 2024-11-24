using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewCharacter", menuName = "Character")]
public class Characters : ScriptableObject
{
    public GameObject playableCharacter;

    public Sprite characterImage;

    public string characterName;

    public string healthQuantity;

    public string shotSpeed;
}
