using UnityEngine;

public class LookDurationDetector : MonoBehaviour
{
    [Header("Cast")]
    public float maxDistance = 1000f;
    public LayerMask layerMask = ~0;
    public float castRadius = 0.15f;

    [Header("Optional")]
    public double minimumLogDuration = 0d;

    private string currentLookedCard;
    private double startTime;
    private bool isLooking = false;
    public GameManager gameManager;

    // void Update()
    // {
    //     Camera cam = Camera.main;
    //     if (cam == null)
    //     {
    //         // Debug.Log("[LOOK] Camera.main is NULL");
    //         return;
    //     }

    //     Ray ray = new Ray(cam.transform.position, cam.transform.forward);
    //     // Debug.DrawRay(cam.transform.position, cam.transform.forward * maxDistance, Color.red);

    //     bool hasHit = Physics.SphereCast(ray, castRadius, out RaycastHit hit, maxDistance, layerMask);

    //     if (hasHit)
    //     {
    //         Debug.Log($"[LOOK] SphereCast HIT collider='{hit.collider.name}', obj='{hit.collider.gameObject.name}'");

    //         string resolvedCard = FindCardFromHit(hit.collider.transform);

    //         if (resolvedCard == null)
    //         {
    //             // Debug.Log("[LOOK] FindCardFromHit returned NULL");

    //             if (isLooking)
    //             {
    //                 // Debug.Log("[LOOK] Was looking before, so stop current look");
    //                 StopAndLogCurrentObject();
    //             }

    //             return;
    //         }

    //         Debug.Log($"[LOOK] Resolved card = '{resolvedCard}'");

    //         if (!isLooking)
    //         {
    //             // Debug.Log("[LOOK] Start looking");
    //             // StartLookingAt(resolvedCard);
    //         }
    //         else if (resolvedCard != currentLookedCard)
    //         {
    //             // Debug.Log($"[LOOK] Switched target from '{currentLookedCard?.idString}' to '{resolvedCard.idString}'");
    //             StopAndLogCurrentObject();
    //             StartLookingAt(resolvedCard);
    //         }
    //         else
    //         {
    //             // Debug.Log($"[LOOK] Still looking at '{resolvedCard.idString}'");
    //         }
    //     }
    //     else
    //     {
    //         // Debug.Log("[LOOK] SphereCast HIT NOTHING");

    //         if (isLooking)
    //         {
    //             // Debug.Log("[LOOK] Lost sight, stopping");
    //             StopAndLogCurrentObject();
    //         }
    //     }
    // }

    // private string FindCardFromHit(Transform hitTransform)
    // {
    //     Transform t = hitTransform;

    //     while (t != null)
    //     {
    //         Debug.Log($"[LOOK] Checking transform '{t.name}'");
    //         if (t.name.Contains("CardUI"))
    //         {
    //             string[] strings = t.name.Split("_");
    //             string cardID = strings[strings.Length-1];
    //             Debug.Log($"[LOOK] FOUND Card component on " + cardID);
    //             return cardID;
    //         }
            
    //         // Card card = t.GetComponent<Card>();
    //         // if (card != null)
    //         // {
    //         //     Debug.Log($"[LOOK] FOUND Card component on '{t.name}', idString='{card.idString}'");
    //         //     return card;
    //         // }

    //         t = t.parent;
    //     }

    //     Debug.Log("[LOOK] No Card component found in parent chain");
    //     return null;
    // }

    // private void StartLookingAt(string card)
    // {
    //     if (card == null) return;

    //     currentLookedCard = card;
    //     startTime = Time.timeAsDouble;
    //     isLooking = true;
    //     Debug.Log("[HIGHLIGHT] Found Card!");
    //     Debug.Log("[HIGHLIGHT] Card is " + card);
    //     Debug.Log("[HIGHLIGHT] Card id is " + card);
    //     gameManager.HighlightWeakEnemys(int.Parse(card));

    //     Debug.Log($"[LOOK] BEGIN '{currentLookedCard}' at {startTime:F3}");
    // }

    // private void StopAndLogCurrentObject()
    // {
    //     if (currentLookedCard == null)
    //     {
    //         // Debug.Log("[LOOK] StopAndLogCurrentObject: currentLookedCard is NULL");
    //         isLooking = false;
    //         return;
    //     }

    //     double endTime = Time.timeAsDouble;
    //     double elapsed = endTime - startTime;

    //     // Debug.Log($"[LOOK] END '{currentLookedCard.idString}', elapsed={elapsed:F3}");

    //     if (elapsed >= minimumLogDuration)
    //     {
    //         // Debug.Log($"[LOOK] LOGGING '{currentLookedCard.idString}' for {elapsed:F2}s");

    //         StudyLogger.LogDuration(
    //             "LookedAtObject",
    //             startTime,
    //             endTime,
    //             "0",
    //             currentLookedCard,
    //             null,
    //             null,
    //             ""
    //         );
    //     }

    //     currentLookedCard = null;
    //     isLooking = false;
    //     gameManager.HideWeakEnemys();
    // }
    public string GetCurrentLookCardID()
    {
        if(currentLookedCard == null) return "0";
        return currentLookedCard;
    }
    // public void LookingAtHUD(Transform hitTransform)
    // {
    //     FindCardFromHit(hitTransform);
    // }

    public void StartLookingAt(string card)
    {
        if (card == null) return;

        currentLookedCard = card;
        startTime = Time.timeAsDouble;
        isLooking = true;
        Debug.Log("[HIGHLIGHT] Found Card!");
        Debug.Log("[HIGHLIGHT] Card is " + card);
        Debug.Log("[HIGHLIGHT] Card id is " + card);
        gameManager.HighlightWeakEnemys(int.Parse(card));

        Debug.Log($"[LOOK] BEGIN '{currentLookedCard}' at {startTime:F3}");
    }

    public void StopLookingAt(string card)
    {
        if (card == null)
        {
            Debug.Log("[LOOK] StopAndLogCurrentObject: currentLookedCard is NULL");
            isLooking = false;
            return;
        }

        double endTime = Time.timeAsDouble;
        double elapsed = endTime - startTime;

        // Debug.Log($"[LOOK] END '{currentLookedCard.idString}', elapsed={elapsed:F3}");

        if (elapsed >= minimumLogDuration)
        {
            // Debug.Log($"[LOOK] LOGGING '{currentLookedCard.idString}' for {elapsed:F2}s");

            StudyLogger.LogDuration(
                "LookedAtObject",
                startTime,
                endTime,
                "0",
                card,
                null,
                null,
                ""
            );
        }
        isLooking = false;
        gameManager.HideWeakEnemys();
    }


    private void StopAndLogCurrentObject()
    {
        if (currentLookedCard == null)
        {
            // Debug.Log("[LOOK] StopAndLogCurrentObject: currentLookedCard is NULL");
            isLooking = false;
            return;
        }

        double endTime = Time.timeAsDouble;
        double elapsed = endTime - startTime;

        // Debug.Log($"[LOOK] END '{currentLookedCard.idString}', elapsed={elapsed:F3}");

        if (elapsed >= minimumLogDuration)
        {
            // Debug.Log($"[LOOK] LOGGING '{currentLookedCard.idString}' for {elapsed:F2}s");

            StudyLogger.LogDuration(
                "LookedAtObject",
                startTime,
                endTime,
                "0",
                currentLookedCard,
                null,
                null,
                ""
            );
        }

        currentLookedCard = null;
        isLooking = false;
        gameManager.HideWeakEnemys();
    }

}