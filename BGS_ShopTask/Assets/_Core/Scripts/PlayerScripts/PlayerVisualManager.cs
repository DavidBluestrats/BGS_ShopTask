using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerVisualManager : MonoBehaviour
{
    [Header("Player Body Parts")]
    public SpriteRenderer headRenderer;
    public SpriteRenderer torsoRenderer;
    public SpriteRenderer legsRenderer;
    public SpriteRenderer feetRenderer;

    [Header("Player Apparel")]
    public Apparel headApparel;
    public Apparel torsoApparel;
    public Apparel legsApparel;
    public Apparel feetApparel;
    public Dictionary<int, Apparel> wornApparel;

    [Header("Tests")]

    public Apparel testApparel;

    [Header("Components")]
    private Mover moverComponent;


    void Start()
    {
        moverComponent = GetComponent<Mover>();
        wornApparel = new Dictionary<int, Apparel>();

        //Add the default apparel to the sprites and start off updating from there.
        wornApparel.Add(headApparel.id, headApparel);
        wornApparel.Add(torsoApparel.id, torsoApparel);
        wornApparel.Add(legsApparel.id, legsApparel);
        wornApparel.Add(feetApparel.id, feetApparel);

    }

    void Update()
    {
        UpdatePlayerVisuals();
        FlipPlayerVisuals();
    }

    public bool PlayerIsWearingApparel(Apparel _apparelToTest)
    {
        return wornApparel.ContainsKey(_apparelToTest.id);
    }

    private void UpdatePlayerVisuals()
    {
        MoveBodyPart(headRenderer, headApparel);
        MoveBodyPart(torsoRenderer, torsoApparel);
        MoveBodyPart(legsRenderer, legsApparel);
        MoveBodyPart(feetRenderer, feetApparel);
    }

    private void ResetPlayerSpritesToLookDown()
    {
        headRenderer.sprite = headApparel.downSide;
        torsoRenderer.sprite = torsoApparel.downSide;
        legsRenderer.sprite = legsApparel.downSide;
        feetRenderer.sprite = feetApparel.downSide;
    }

    public void UpdateApparel(Apparel _newApparel)
    {
        //Remove worn apparel type.
        switch (_newApparel.type)
        {
            case ApparelType.HEAD:
                wornApparel.Remove(headApparel.id);
                headApparel = _newApparel;
                break;
            case ApparelType.TORSO:
                wornApparel.Remove(torsoApparel.id);
                torsoApparel = _newApparel;
                break;
            case ApparelType.LEGS:
                wornApparel.Remove(legsApparel.id);
                legsApparel = _newApparel;
                break;
            case ApparelType.FEET:
                wornApparel.Remove(feetApparel.id);
                feetApparel = _newApparel;
                break;
            default:
                break;
        }
        //Add new apparel and refresh sprites.
        wornApparel.Add(_newApparel.id, _newApparel);
        ResetPlayerSpritesToLookDown();
    }

    private void MoveBodyPart(SpriteRenderer _bodyPart, Apparel _apparel)
    {
        //If the player moves, then their sprites must be updated depending on move direction.
        if (moverComponent.moveDirection.x != 0 || moverComponent.moveDirection.y != 0)
        {
            if (moverComponent.moveDirection.y > 0)
            {
                _bodyPart.sprite = _apparel.upSide;
            }
            else if (moverComponent.moveDirection.y < 0)
            {
                _bodyPart.sprite = _apparel.downSide;
            }
            else
            {
                _bodyPart.sprite = _apparel.rightSide;
            }
        }
    }

    private void FlipPlayerVisuals()
    {
        //If player stops moving, then flip their horizontal direction depending on last move direction.

        if (moverComponent.lastMoveDirection.x < 0)
        {
            headRenderer.flipX = true;
            torsoRenderer.flipX = true;
            legsRenderer.flipX = true;
            feetRenderer.flipX = true;
        }
        else
        {
            headRenderer.flipX = false;
            torsoRenderer.flipX = false;
            legsRenderer.flipX = false;
            feetRenderer.flipX = false;
        }
    }
}
