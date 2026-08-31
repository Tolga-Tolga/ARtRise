using UnityEngine;
using UnityEngine.UI;
using Meta.WitAi.Data;
using NUnit.Framework.Constraints;
using TMPro;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private QrCodeDisplayManager qrCodeDisplayManager;
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private Archer archer;
    [SerializeField] private Assassin assassin;
    [SerializeField] private EvilMage evilMage;
    [SerializeField] private FireSorcerer fireSorcerer;
    [SerializeField] private FrostSorcerer frostSorcerer;
    [SerializeField] private Goblin goblin;
    [SerializeField] private Healer healer;
    [SerializeField] private Knight knight;
    [SerializeField] private Ogre ogre;
    [SerializeField] private StunMage stunMage;
    public Transform cameraTransform;
    public float distance = 1.5f;
    public Transform endGameCanvasTransform;

    public string scannerObjectName = "QrCodeScanner";
    private List<QrCodeDisplayManager.MarkerPose> activeCards = null;
    public TextMeshProUGUI playerTurnText;
    public TextMeshProUGUI cardPlayedText;
    public TextMeshProUGUI winnerButtonText;
    public bool turn = true;
    public int cardID = 0;
    [SerializeField] private WitIntValue numberPlayed;
    public Slider playerHpSlider;
    public Slider enemyHpSlider;
    public TextMeshProUGUI playerHpText;
    public TextMeshProUGUI enemyHpText;
    public Image playerPlayingIcon;
    public Image enemyPlayingIcon;

    public ScrollRect AftergameButton;

    public Player player;
    public Player enemy;

    [SerializeField] public Dictionary<int,Card> allCards = null;
    [SerializeField] public Card[] allCardsArray;

    private int winnerId = 0;

    private bool gameFinished = false;

    private double startTime;

    public bool showUI = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (playerTurnText != null)
        {
            updatePlayerTurnText();
            updateCardPlayedText();
        }
        // Debug.Log("[CARD] I am alive 2");
        allCards = new Dictionary<int, Card>();

        foreach (var card in allCardsArray)
        {
            allCards[card.id] = card;
        }
        AftergameButton.gameObject.SetActive(false);
        // Debug.Log("[CARD] I am alive 3");
        SetHpUI();
        startTime = Time.timeAsDouble;
    }

    void Awake()
    {
        SetHpUI();
    }
    private void OnEnable()
    {
        // Debug.Log("[CARD] I am alive 4");
        if (qrCodeDisplayManager == null)
        {
            // Debug.Log("[CARD] I am alive 5");
            var go = GameObject.Find(scannerObjectName);
            if (go != null) qrCodeDisplayManager = go.GetComponent<QrCodeDisplayManager>();
            activeCards = qrCodeDisplayManager.objectPoses;
        }
        SetHpUI();
        // Debug.Log("[CARD] I am alive 6");
    }
    int counterUIUpdater = 0;
    // Update is called once per frame
    void Update()
    {   
        counterUIUpdater++;
        if(counterUIUpdater == 100)
        {
            counterUIUpdater = 0;
            SetHpUI();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Debug.Log("Escape / Back gedrückt");
        }
        // only update if game is not finished
        if (gameFinished)
        {
            LoadGameSceneFull();
            return;
        }
        // first check if a player has won
        // tie
        if (player.getHp() <= 0 && enemy.getHp() <= 0)
        {
            EndGame(0);
            gameFinished = true;
            return;
        }

        // enemy has won
        if (player.getHp() <= 0)
        {
            EndGame(1);
            gameFinished = true;
            return;
        }

        // player has won
        if (enemy.getHp() <= 0)
        {
            EndGame(2);
            gameFinished = true;
            return;
        }

        // afterwards activate scanned cards 
        // Debug.Log("[CARD] I am alive 7");
        if (!qrCodeDisplayManager.isBlocked)
        {
            // Debug.Log("[CARD] I am alive 8");
            if (activeCards == null)
            {
                activeCards = qrCodeDisplayManager.objectPoses;
                Debug.Log("[CARD] I have no objectPoses!");
                return;
            }
            // Debug.Log("[CARD] I am alive 9");
            // foreach (var kv in allCards)
            // {
            //     Debug.Log($"[CARD] Card[{kv.Key}] active = {kv.Value?.IsActive()}");
            // }
            activeCardsListBlocked = true;
            activeCardsList.Clear();

            // store all active cards in a list
            for (int i = -10; i < 0; i++)
            {
                if (allCards[i].IsVisible())
                {
                    activeCardsList.Add(i);
                }
            }
            for (int i = 1; i < 11; i++)
            {
                if (allCards[i].IsVisible())
                {
                    activeCardsList.Add(i);
                }
            }
            activeCardsListBlocked = false;

            // Debug.Log("[CARD] I am alive 10");
            activeCards = qrCodeDisplayManager.objectPoses;
            foreach (var card in activeCards)
            {
                // Debug.Log("[CARD] active Card from objectPoses: " + card.id);
                if (int.TryParse(card.id, out int activeCardID)){
                    // Debug.Log("[CARD] active Card from objectPoses parsed: " + activeCardID);
                    if (allCards.TryGetValue(activeCardID, out var card2))
                    {
                        // Debug.Log("[CARD] got Value via objectPoses: " + card2);
                        if (!card2.IsDead())
                            card2.SetActive();
                        
                    }
                }
                else
                {
                    Debug.Log("[CARD] Failed to receive Value via objectPoses: " + card.id);
                }
            }
        }
    }

    // int status, 0 = tie, 1 = enemy won, 2 = player won
    private void EndGame(int status)
    {
        StudyLogger.LogEvent("GameFinished", "0", "0", null, null, status.ToString());
        AftergameButton.gameObject.SetActive(true);
        if (status == 0)
        {
            SetText("Draw, nobody has won!", winnerButtonText);
        }
        if (status == 1)
        {
            SetText("Oh no, the enemy has won!", winnerButtonText);
        }
        if (status == 2)
        {
            SetText("Congratulations, you have won!", winnerButtonText);
        }
        Vector3 forward = cameraTransform.forward;
        forward.y = 0;

        endGameCanvasTransform.position =
            cameraTransform.position + forward.normalized * distance;

        endGameCanvasTransform.LookAt(cameraTransform);
        endGameCanvasTransform.Rotate(0, 180, 0);
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("App beendet"); // Im Editor sichtbar
    }

    public void getPlayedCard()
    {
        Debug.LogError("CARD PLAYED GET: " + numberPlayed);
    }
    int turnCounter = 0;
    public void setPlayedCard(string text)
    {
        if(gameFinished) return;
        Debug.Log("[VOICE] Got a new voice value: " + text);
        if (int.TryParse(text, out cardID) && cardID > (-11) && (turn == (cardID > 0)))
        {
            if (!validateCard(cardID))
            {
                // TODO play sound and log that player tried to play unplayable card.
                StudyLogger.LogEvent("TryPlayInvalidCard","cardID","", 0,null,"-");
                Debug.Log("[Play] Tried to play invalid card " + cardID);
                soundManager.PlaySoundFail();
                return;
            }
            soundManager.PlaySoundSuccess();
            if (cardID<0)
            {
                for (int i = -10; i < 0; i++)
                {
                    allCards[i].isOnCooldown = false;
                    allCards[i].RestoreCardEffectColor();
                }
            }
            if (cardID > 0)
            {
                for (int i = 1; i < 11; i++)
                {
                    allCards[i].isOnCooldown = false;
                    allCards[i].RestoreCardEffectColor();
                }
            }
            allCards[cardID].GreyoutCard();
            allCards[cardID].isOnCooldown = true;
            double elapsed = Time.timeAsDouble - startTime;
            StudyLogger.LogDuration("PlayerTurn", startTime,Time.timeAsDouble, "0", "0", null, null, turnCounter.ToString());
            startTime = Time.timeAsDouble;
            turnCounter++;
            turn = !turn;
            Debug.Log("[ATTACK] Attack registered");
            updateCardPlayedText();
            // Debug.Log(cardID);
            if (cardID > -11 && cardID < 11)
            {
                Debug.Log("[ATTACK] cardID: " + cardID);
                Card playedCard = allCards[cardID];
                (List<Effect> effects, int dmg) = playedCard.Attack();
                Debug.Log("[ATTACK] attacked with dmg: " + dmg);
                if (cardID<0)
                {
                    for (int i = 1; i < 11; i++)
                    {
                        if (allCards[i].IsActive())
                        {
                            allCards[i].TickEffect();
                            allCards[i].ReceiveAttack(dmg, playedCard.GetCardEffect(), CardConfig.TICKDURATION, false, cardID); // TODO: Implement Effects
                            //TODO: Update Healthbar of cards visually
                        }
                    }
                    if (GetActiveCardAmount(1) < 3)
                    {
                        player.SetDmg(dmg);
                    }
                }
                else
                {
                    for (int i = -10; i < 0; i++)
                    {
                        if (allCards[i].IsActive())
                        {
                            allCards[i].TickEffect();
                            allCards[i].ReceiveAttack(dmg, playedCard.GetCardEffect(), CardConfig.TICKDURATION, false, cardID); // TODO: Implement Effects
                            //TODO: Update Healthbar of cards visually
                        }
                    }
                    if (GetActiveCardAmount(0) < 3)
                    {
                        enemy.SetDmg(dmg);
                        soundManager.PlaySoundAttack();
                    }
                }
                // for (int i = 1; i < 11; i++)
                // {
                //     if (allCards[i].IsActive())
                //     {
                //         bool effected = allCards[i].TickEffect();
                //         if(effected)
                //         {
                //             //TODO create animation for effects

                //         }
                //     }
                // }
            }
            updatePlayerTurnText();
            SetHpUI();
        }
        else
        {
            // soundManager.PlaySoundFail();
        }
    }

    // 0 for enemy, 1 for player. Returns how many cards are active per player.
    private int GetActiveCardAmount(int player)
    {
        int count = 0;
        if (player == 0)
        {
            for (int i = -10; i < 0; i++)
            {
                if (allCards[i].IsActive())
                {
                    count++;
                }
            }
        }
        if (player == 1)
        {
            for (int i = 1; i < 11; i++)
            {
                if (allCards[i].IsActive())
                {
                    count++;
                }
            }
        }
        Debug.Log("[VOICE] active card count of player: " + player + ", with the count: " + count);
        return count;
    }

    private bool validateCard(int cardID)
    {
        Debug.Log("[VOICE] cardID: " + cardID + " ist active: " + allCards[cardID].IsActive() + ", player HP: " + player.getHp() + ", enemy HP: " + enemy.getHp());
        return allCards[cardID].IsActive() && !allCards[cardID].isOnCooldown;
        // foreach (var card in activeCards)
        // {
        //     if (int.TryParse(card.id, out int activeCardID) && activeCardID == cardID) return true;
        // }
        // return false;
    }
    private int playerTurn = 1;
    private void updatePlayerTurnText()
    {
        playerTurn = 1;
        if (!turn) playerTurn = 2;
        if (turn)
        {
            SetAlpha(playerPlayingIcon, 1);
            SetAlpha(enemyPlayingIcon, 1);
        }
        else {
            SetAlpha(playerPlayingIcon, 0);
            SetAlpha(enemyPlayingIcon, 1);
        }
        SetText("Player " + playerTurn + " is at turn", playerTurnText);
    }

    public int GetPlayerTurn()
    {
        return playerTurn;
    }

    private void updateCardPlayedText()
    {
        SetText("Card " + cardID + " has been played", cardPlayedText);
    }


    public void SetText(string newText, TextMeshProUGUI tmp)
    {
        if(!showUI)
        {
            if(tmp != null)
            {
                tmp.text = "";
            }
            return;
        }
        if (tmp != null)
        {
            tmp.text = newText;
        }
    }


    // UI Controller

    // player = true if hp set is player hp, for enemy hp set false
    public void SetHpUI()
    {
        playerHpSlider.SetValueWithoutNotify(player.GetHpPercentage());
        enemyHpSlider.SetValueWithoutNotify(enemy.GetHpPercentage());
        SetText(player.getHp().ToString(), playerHpText);
        SetText(enemy.getHp().ToString(), enemyHpText);
    }

    public void SetAlpha(Image icon, float a)
    {
        Color c = icon.color;
        c.a = Mathf.Clamp01(a);
        icon.color = c;
    }

    public void LoadGameSceneFull()
    {
        GameData.playerWon = winnerId;
            SceneManager.LoadScene("ModeSelection");
    }

    private List<int> activeCardsList = new List<int>();
    public bool activeCardsListBlocked = false;
    public List<int> GetActiveCards()
    {
        return activeCardsList;
    }

    List<Card> vulnerableCards = null;
    public void HighlightWeakEnemys(int id)
    {
        HideWeakEnemys();
        Debug.Log("[HIGHLIGHT] Highlight Cards vulnerable for id: " + id);
        Card highlightedCard = null;
        List<int> allActiveCards = GetActiveCards();
        foreach (var cardInActiveCards in allActiveCards)
        {
            if (cardInActiveCards == id)
            {
                highlightedCard = allCards[id];
            }
        }
        if (highlightedCard == null)
        {
            return;
        }

        List<Card> enemyCards = new List<Card>();
        if (id < 0)
        {
            foreach (var cardInActiveCards in allActiveCards)
            {
                if (cardInActiveCards > 0)
                {
                    enemyCards.Add(allCards[cardInActiveCards]);
                }
            }
        }
        if (id > 0)
        {
            foreach (var cardInActiveCards in allActiveCards)
            {
                if (cardInActiveCards < 0)
                {
                    enemyCards.Add(allCards[cardInActiveCards]);
                }
            }
        }
        if (enemyCards.Count == 0)
        {
            return;
        }

        Effect cardEffect = highlightedCard.GetCardEffect();
        if (cardEffect == Effect.NONE)
        {
            return;
        }
        
        vulnerableCards = new List<Card>();
        foreach (var activeEnemyCard in enemyCards)
        {
            if (activeEnemyCard.vulnerabilities.Contains(cardEffect))
            {
                vulnerableCards.Add(activeEnemyCard);
            }
        }
        Debug.Log("[HIGHLIGHT] Found " + vulnerableCards.Count + " vulnerable cards!");
        Debug.Log("[HIGHLIGHT] The vulnerable cards are: ");
        foreach (var vulnerableCard in vulnerableCards)
        {
            Debug.Log("[HIGHLIGHT] " + vulnerableCard.id);
            vulnerableCard.ShowHighlightArrow();
        }

    }
    public void HideWeakEnemys()
    {
        if (vulnerableCards == null)
        {
            return;
        }
        foreach (var vulnerableCard in vulnerableCards)
        {
            vulnerableCard.HideHighlightArrow();
        }
        vulnerableCards = null;
    }

}
