using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogNPC : Interactable
{
    public override void Interact()
    {
        // Base logic
        base.Interact();
        // Custom logic
        print("DialogNPC Interact()");
    }
}
