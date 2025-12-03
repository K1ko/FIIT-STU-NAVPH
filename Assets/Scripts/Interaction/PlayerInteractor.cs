using UnityEngine;
using System.Collections.Generic;

public class PlayerInteractor : MonoBehaviour
{
    public KeyCode interactKey = KeyCode.E;
    public Transform interactionPoint;
    public float interactionRadius = 1f;
    public LayerMask interactableLayer;
    
    private IInteractable currentInteractable;    

    void Update()
    {
        DetectInteractable();

        if (Input.GetKeyDown(interactKey) && currentInteractable != null)
        {
            currentInteractable.Interact();
        }
    }

    void DetectInteractable()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(interactionPoint.position, interactionRadius, interactableLayer);

        currentInteractable = null;

        float closestDist = Mathf.Infinity;
        foreach (var hit in hits)
        {
            var interactable = hit.GetComponent<IInteractable>();
            if (interactable != null)
            {
                float dist = Vector2.Distance(transform.position, hit.transform.position);
                if (dist < closestDist)
                {
                    closestDist = dist;
                    currentInteractable = interactable;
                }
            }
        }

        UIInteractionPrompt.instance?.SetPrompt(currentInteractable?.GetInteractionPrompt());
    }

    void OnDrawGizmosSelected()
    {
        if (interactionPoint == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionPoint.position, interactionRadius);
    }
    
}
