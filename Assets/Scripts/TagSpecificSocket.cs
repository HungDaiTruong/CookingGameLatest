using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TagSpecificSocket : XRSocketInteractor
{
    // This script allows for socketing according to multiple tags on the collider

    public string targetTag1 = string.Empty, targetTag2 = string.Empty, targetTag3 = string.Empty, targetTag4 = string.Empty;


    public override bool CanHover(XRBaseInteractable interactable)
    {
        return base.CanHover(interactable) && MatchUsingTag(interactable);
    }

    public override bool CanSelect(XRBaseInteractable interactable)
    {
        return base.CanSelect(interactable) && MatchUsingTag(interactable);
    }

    // This method allows for the writing of socketable tags in the inspector
    private bool MatchUsingTag(XRBaseInteractable interactable)
    {
        return interactable.CompareTag(targetTag1) || interactable.CompareTag(targetTag2) || interactable.CompareTag(targetTag3) || interactable.CompareTag(targetTag4);
    }
}
