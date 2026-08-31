using UnityEngine;

public class CardUIReference : MonoBehaviour
{
    public Card card;

    public void OnBeginHighlight()
    {
        if (card == null)
        {
            Debug.LogWarning($"[{name}] No Card assigned. Instance path: {transform.root.name}/{transform.name}");
            return;
        }

        Debug.Log($"Looking at card: {card.name}, idString={card.idString}");
        card.LookingAtCard(true);
    }

    public void OnEndHighlight()
    {
        if (card == null) return;

        card.LookingAtCard(false);
    }
}