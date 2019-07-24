using UnityEngine;
using System.Collections;

public abstract class Character_Component_Base : MonoBehaviour
{
    protected Character_Base character;

    public void SetCharacter(Character_Base pCharacter)
    {
        character = pCharacter;
    }
}
