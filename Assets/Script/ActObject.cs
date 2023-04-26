using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Act", order = 1)]
public class ActObject : ScriptableObject
{
    public CharacterDirector.PlayerState state;
    public GameObject King;
    public GameObject Murder;
}
