using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class Card : MonoBehaviour
{
    public int hp = 100;
    public int maxHP = 100;
    public int dmg = 0;
    [SerializeField] public (int count, Effect effect)[] activeEffects;
    public bool canEffectFrost = false;
    public bool canEffectFire = false;
    public bool canEffectStun = false;
    public List<Effect> vulnerabilities;
    public List<Effect> procEffectsOnAttacks;

    public bool isActive = false;

    public bool isDead = false;

    public int id;
    public string idString;

    private bool isVisible;

    // public Slider cardHPSlider;
    // [SerializeField] private Transform hpBarUI;
    // [SerializeField] private Transform effectUI;



    public QrCodeDisplayManager qrCodeDisplayManager;
    public FloorCubeConsumer models;
    public FloorCubeConsumerExperimental experimentalModels;

    // public Image fireIcon;
    // public Image frostIcon;
    // public Image poisonIcon;

    // public Image fireIconProperty;
    // public Image frostIconProperty;
    // public Image poisonIconProperty;
    // public Image charBlockedIcon;

    // public TextMeshProUGUI effectCounter;

    int posX = 0;
    int posY = 0;
    int posZ = 0;
    Vector3 currentPos;
    float hpPercentage;
    private GameObject cardModel = null;
    public bool isOnCooldown = false;

    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private GameObject cardUIPrefab;
    private GameObject cardUIInstance;
    private Transform hpBarUI;
    private Transform effectUI;
    private Slider cardHPSlider;
    private Image fireIcon;
    private Image frostIcon;
    private Image poisonIcon;
    private Image fireIconProperty;
    private Image frostIconProperty;
    private Image poisonIconProperty;
    private Image charBlockedIcon;
    private TextMeshProUGUI effectCounter;

    public bool showUI = true;
    private bool instantiatePrefabs = false;

    public LookDurationDetector lookDurationDetector;
    
    [SerializeField] private SoundManager soundManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Awake()
    {
        if (models == null || !models.enabled)
        {
            #if UNITY_2023_1_OR_NEWER
            experimentalModels = FindFirstObjectByType<FloorCubeConsumerExperimental>();
            #else
            experimentalModels = FindObjectOfType<FloorCubeConsumerExperimental>();
            #endif
        }

        activeEffects = new (int count, Effect effect)[] 
        {
            (0, Effect.FROST),
            (0, Effect.FIRE), 
            (0, Effect.STUN)
        };

        // vulnerabilities = new List<Effect>();
        // procEffectsOnAttacks = new List<Effect>();
        // if (canEffectFrost) {
        //     procEffectsOnAttacks.Add(Effect.FROST);
        // }
        // if (canEffectFire) {
        //     procEffectsOnAttacks.Add(Effect.FIRE);
        // }
        // if (canEffectStun) {
        //     procEffectsOnAttacks.Add(Effect.STUN);
        // }
    }

    void Update()
    {
        if (isVisible)
        {
            if(!instantiatePrefabs)
            {
                SpawnCardUI();
                soundManager.PlaySoundNewCard();
                instantiatePrefabs = true;

            }
            hpPercentage = (float)hp/maxHP;
            if (showUI)
            {
                SetHPUI();
                SetPropertyUI();
            }
            var modelDictionary = GetModelDictionary();
            if(modelDictionary == null)
            {
                // Debug.Log("[CARD] Model is null, Has idString: " + idString);
                return;
            } 
            bool found = modelDictionary.TryGetValue(idString, out var model);
            // Debug.Log("[CARD] Has found model for card: " + found + ", Has idString: " + idString + ", model: " + model);
            if (found && model != null)
            {
                // Debug.Log("[CARD] Card found with position : " + currentPos);
                currentPos = model.transform.position;
                UpdateHPBarPosition();
                if (showUI)
                {
                    SetGreyoutUI();
                    SetEffectUI(cachedEffect);
                }
                cardModel = model;
                // var currentRot = cube.transform.rotation;
                // var currentScale = cube.transform.localScale;
            } 
        }
    }

    private Dictionary<string, GameObject> GetModelDictionary()
    {
        if (experimentalModels != null) return experimentalModels.GetGameObjectsByID();
        if (models != null) return models.GetGameObjectsByID();
        return null;
    }

    public (List<Effect>, int) Attack() {
        // Debug.Log("[ATTACK] 1");
        int calculatedDmg = (int) (dmg*GetDamageReduction());
        return (new List<Effect>(), calculatedDmg);
    }
    
    public Effect GetCardEffect()
    {
        if(procEffectsOnAttacks.Count > 0) return procEffectsOnAttacks[0];
        return Effect.NONE;
    }

    // returns true if the attack was effective
    public bool ReceiveAttack(int dmg, Effect effect, int effectDuration, bool isTick, int originAttackerID) {
        bool wasEffective = false;
        hp = hp - dmg;
        // Debug.Log("[ATTACK] Received Attack with dmg: " + dmg + " and I have leftover HP: " + hp);
        int effectDmg = 0;
        foreach (var activeEffect in activeEffects)
        {
            if (activeEffect.count > 0)
            {
                if (vulnerabilities.Contains(activeEffect.effect)){
                    hp = hp - CardConfig.EFFECTIVEDMGBONUS;
                    effectDmg = CardConfig.EFFECTIVEDMGBONUS;
                    wasEffective = true;
                }
                switch (activeEffect.effect)
                {
                    case Effect.FROST: 
                        hp = hp - CardConfig.FROSTTICKDMG;
                        effectDmg += CardConfig.FROSTTICKDMG;
                        break;
                    case Effect.FIRE: 
                        hp = hp - CardConfig.FIRETICKDMG;
                        effectDmg += CardConfig.FIRETICKDMG;
                        break;
                    case Effect.STUN:
                        break;
                    default:
                        break;
                }
            }
        }
        if(!isTick){
            StudyLogger.LogEvent("DmgFromAttack",originAttackerID.ToString(),idString, dmg,null,"-");
        }
        else
        {
            StudyLogger.LogEvent("DmgFromEffectTick","0",idString, effectDmg,null,"-");
        }
        if(IsDead())
        {
            if(!isTick){
                StudyLogger.LogEvent("KilledUnitWithAttack",originAttackerID.ToString(),idString, dmg,null,"-");
            }
            else
            {
                StudyLogger.LogEvent("KilledUnitWithEffect","0",idString, dmg,null,"-");
            }
            soundManager.PlaySoundDead();
            isDead = true;
            isActive = false;
            setVisible(false);
            // cardHPSlider.gameObject.SetActive(false);
            hpBarUI.position = new Vector3(-100000,-1000000,-1000000);
            effectUI.position = new Vector3(-100000,-1000000,-1000000);
        }
        if (effect != Effect.NONE)
        {
            switch (effect)
            {
                case Effect.FROST: 
                    activeEffects[0].count = activeEffects[0].count + effectDuration;
                    SetText(activeEffects[0].count.ToString(), effectCounter);
                    StudyLogger.LogEvent("EffectedUnit","0",idString, 0,null,effect.ToString());
                    break;
                case Effect.FIRE: 
                    activeEffects[1].count = activeEffects[1].count + effectDuration;
                    SetText(activeEffects[1].count.ToString(), effectCounter);
                    StudyLogger.LogEvent("EffectedUnit","0",idString, 0,null,effect.ToString());
                    break;
                case Effect.STUN: 
                    activeEffects[2].count = activeEffects[2].count + effectDuration;
                    SetText(activeEffects[2].count.ToString(), effectCounter);
                    StudyLogger.LogEvent("EffectedUnit","0",idString, 0,null,effect.ToString());
                    break;
                default:
                    break;
            }
            // Debug.Log("[EFFECT] effected by effect: " + effect);
            // Debug.Log("[EFFECT] remaining effect duration in turns frost: " + activeEffects[0].count);
            // Debug.Log("[EFFECT] remaining effect duration in turns fire: " + activeEffects[1].count);
            // Debug.Log("[EFFECT] remaining effect duration in turns stun: " + activeEffects[2].count);
            if(!isTick) ChangeCardModelColor(effect);
        }
        return wasEffective;
    }

    public int HealHp(int healValue) {
        hp = hp + healValue;
        return hp;
    }

    public bool IsDead() {
        return hp <=0;
    }

    public float GetDamageReduction() {
        // Debug.Log("[ATTACK] activeEffects.Length: " + activeEffects.Length);
        float damageFactor = (float) (activeEffects[2].count * 0.125f);
        if (damageFactor > 0.5f) damageFactor = 0.5f;
        return 1-damageFactor;
    }

    // returns true if the tick effect was effective against this entity
    public bool TickEffect()
    {
        bool wasEffective = false;
        bool isEffected = false;
        for (int i = 0; i < activeEffects.Length; i++)
        {
            if (activeEffects[i].count > 0)
            {
                isEffected = true;
                var e = activeEffects[i];
                e.count--;
                activeEffects[i] = e;
                SetText(activeEffects[i].count.ToString(), effectCounter);
                if(activeEffects[i].count < 1) SetText("", effectCounter);
                var tmp = ReceiveAttack(0, Effect.NONE, 0, true,0);
                if (tmp) wasEffective = true;
            }
        }
        if(!isEffected)
        {
            ChangeCardModelColor(Effect.NONE);
        }
        return wasEffective;
    }

    bool alreadySetActive = false;    
    public void SetActive()
    {
        alreadySetActive = true;
        isActive = true;
        setVisible(true);
        if(alreadySetActive) return;
        SetEffectUI(Effect.NONE);
    }

    public bool IsActive()
    {
        return isActive;
    }

    public bool IsVisible()
    {
        return isVisible;
    }

    public void setVisible(bool state)
    {
        isVisible = state;
    }
    private Color originalColor = Color.cyan;
    private Color burningColor = Color.red;
    private Color freezingColor = Color.blue;
    private Color stunnedColor = Color.yellow;
    private Color waitColor = Color.gray;
    private Effect cachedEffect = Effect.NONE;
    private Color colorTemp;
    private bool greyOut = false;


    private void ChangeCardModelColor(Effect effect)
    {
        if (cardModel == null) return;
        cachedEffect = effect;
        SetEffectUI(effect);
        // if(greyOut) return;
        switch (effect)
        {
            case Effect.FROST: 
                colorTemp = freezingColor;
                break;
            case Effect.FIRE: 
                colorTemp = burningColor;
                break;
            case Effect.STUN: 
                colorTemp = stunnedColor;
                break;
            default:
                colorTemp = originalColor;
                break;
        }
        var renderers = cardModel.GetComponentsInChildren<Renderer>(true);

        foreach (var rend in renderers)
        {
            var mats = rend.materials;

            for (int i = 0; i < mats.Length; i++)
            {
                var mat = mats[i];
                if (mat == null) continue;

                // Debug.Log("[EFFECT] Material: " + mat.name + ", Shader: " + mat.shader.name);

                if (mat.HasProperty("baseColorFactor"))
                {
                    // Debug.Log("[EFFECT] before baseColorFactor: " + mat.GetColor("baseColorFactor"));
                    mat.SetColor("baseColorFactor", colorTemp);
                    // Debug.Log("[EFFECT] after baseColorFactor: " + mat.GetColor("baseColorFactor"));
                }

                if (mat.HasProperty("emissiveFactor"))
                {
                    // Debug.Log("[EFFECT] before emissiveFactor: " + mat.GetColor("emissiveFactor"));
                    mat.SetColor("emissiveFactor", Color.white * 20f);
                    // Debug.Log("[EFFECT] after emissiveFactor: " + mat.GetColor("emissiveFactor"));
                }

                if (mat.HasProperty("baseColorTexture"))
                {
                    var tex = mat.GetTexture("baseColorTexture");
                    // Debug.Log("[EFFECT] baseColorTexture is null: " + (tex == null));
                }
            }
        }
    }

    public void RestoreCardEffectColor()
    {
        greyOut = false;
    }

    public void GreyoutCard()
    {
        greyOut = true;
    }
    private GameObject arrowInstance;
    // public void ShowHighlightArrow()
    // {
    //     if(!showUI) return;
    //     Debug.Log("[HIGHLIGHT] Showing Card hightlightning for card: " + id);
    //     bool found = models.GetGameObjectsByID().TryGetValue(idString, out var model);
    //     if(!found) return;
    //     Debug.Log("[HIGHLIGHT] Model found for card: " + id);
    //     arrowInstance = Instantiate(arrowPrefab);
    //     ArrowFollowTarget follow = arrowInstance.GetComponent<ArrowFollowTarget>();
    //     follow.target = model.transform;
    // }
    bool alreadyShowing = false;
    bool lockArrow = false;
    public void ShowHighlightArrow()
    {
        if (!showUI) return;
        if(lockArrow || alreadyShowing) return;
        lockArrow = true;

        Debug.Log("[HIGHLIGHT] Showing Card hightlightning for card: " + id);

        bool found = models.GetGameObjectsByID().TryGetValue(idString, out var model);
        if (!found || model == null) {
            lockArrow = false;
            return;
        }

        Debug.Log("[HIGHLIGHT] Model found for card: " + id);
        arrowInstance = Instantiate(arrowPrefab);
        arrowInstance.name = $"HighlightArrow_{id}_{Time.frameCount}";

        Debug.Log("[HIGHLIGHT] Arrow instantiated for card: " + id + " / obj: " + arrowInstance.name);

        ArrowFollowTarget follow = arrowInstance.GetComponentInChildren<ArrowFollowTarget>(true);

        if (follow == null)
        {
            Debug.LogError("[HIGHLIGHT] ArrowFollowTarget missing on arrowPrefab for card: " + id);
            Destroy(arrowInstance);
            lockArrow = false;
            return;
        }

        follow.target = model.transform;
        Debug.Log(
            $"[HIGHLIGHT] Arrow created: {arrowInstance.name}, " +
            $"arrowPos={arrowInstance.transform.position}, " +
            $"targetPos={model.transform.position}, " +
            $"targetScale={model.transform.lossyScale}, " +
            $"arrowScale={arrowInstance.transform.lossyScale}, " +
            $"targetName={model.name}"
        );

        Debug.Log("[HIGHLIGHT] Arrow target set for card: " + id + " target=" + model.name);
        alreadyShowing = true;
        lockArrow = false;
    }

    public void HideHighlightArrow()
    {
        if(!showUI) return;
        Destroy(arrowInstance);
        alreadyShowing = false;
        Debug.Log("[HIGHLIGHT] Hiding Card hightlightning for card: " + id);
    }

    public void LookingAtCard(bool looking)
    {
        if (looking)
        {
            lookDurationDetector.StartLookingAt(idString);
        }
        else
        {
            lookDurationDetector.StopLookingAt(idString);
        }
    }

    //UI

    private void SpawnCardUI()
    {
        if (cardUIPrefab == null)
        {
            Debug.LogError($"[{name}] cardUIPrefab is not assigned.");
            return;
        }

        cardUIInstance = Instantiate(cardUIPrefab);
        cardUIInstance.name = $"CardUI_{name}_{id}";

        hpBarUI = cardUIInstance.transform.Find("HPBar");
        effectUI = cardUIInstance.transform.Find("CardEffect");

        if (hpBarUI == null)
        {
            Debug.LogError($"[{name}] HPBar not found in CardUIPrefab.");
            return;
        }

        if (effectUI == null)
        {
            Debug.LogError($"[{name}] CardEffect not found in CardUIPrefab.");
            return;
        }

        AssignCardReferenceToUI();

        CacheHPBarReferences();
        CacheEffectReferences();

        MakeCanvasMeshMaterialUnique(hpBarUI);
        MakeCanvasMeshMaterialUnique(effectUI);

        hpBarCanvasGroup = GetOrAddCanvasGroup(hpBarUI);
        effectCanvasGroup = GetOrAddCanvasGroup(effectUI);
        ApplyUIVisibility();
    }

    private CanvasGroup hpBarCanvasGroup;
    private CanvasGroup effectCanvasGroup;

    private CanvasGroup GetOrAddCanvasGroup(Transform root)
    {
        if (root == null) return null;

        CanvasGroup group = root.GetComponent<CanvasGroup>();

        if (group == null)
            group = root.gameObject.AddComponent<CanvasGroup>();

        return group;
    }

    private void ApplyUIVisibility()
    {
        float alpha = showUI ? 1f : 0f;

        if (hpBarCanvasGroup != null)
        {
            hpBarCanvasGroup.alpha = alpha;
            hpBarCanvasGroup.interactable = showUI;
            hpBarCanvasGroup.blocksRaycasts = showUI;
        }

        if (effectCanvasGroup != null)
        {
            effectCanvasGroup.alpha = alpha;
            effectCanvasGroup.interactable = showUI;
            effectCanvasGroup.blocksRaycasts = showUI;
        }
    }
    private void AssignCardReferenceToUI()
    {
        // Erst alle bereits im Prefab vorhandenen CardUIReference-Komponenten setzen
        foreach (CardUIReference reference in cardUIInstance.GetComponentsInChildren<CardUIReference>(true))
        {
            reference.card = this;
        }

        // Zusätzlich sicherstellen, dass alle Collider auch eine Referenz haben
        foreach (Collider col in cardUIInstance.GetComponentsInChildren<Collider>(true))
        {
            CardUIReference reference = col.GetComponent<CardUIReference>();

            if (reference == null)
                reference = col.gameObject.AddComponent<CardUIReference>();

            reference.card = this;
        }
    }

    private void MakeCanvasMeshMaterialUnique(Transform root)
    {
        if (root == null) return;

        foreach (Renderer renderer in root.GetComponentsInChildren<Renderer>(true))
        {
            if (renderer.sharedMaterial != null)
                renderer.material = new Material(renderer.sharedMaterial);
        }
    }

    private void CacheHPBarReferences()
    {
        Transform content = hpBarUI.Find("Unity Canvas/Content");

        if (content == null)
        {
            Debug.LogError($"[{name}] HPBar/Unity Canvas/Content nicht gefunden.");
            return;
        }

        cardHPSlider = content.Find("HPSlider")?.GetComponent<Slider>();

        fireIcon = content.Find("Handle/FireIcon")?.GetComponent<Image>();
        frostIcon = content.Find("Handle/FrostIcon")?.GetComponent<Image>();
        poisonIcon = content.Find("Handle/PoisonIcon")?.GetComponent<Image>();

        charBlockedIcon = content.Find("CharBlockedIcon/RedCross")?.GetComponent<Image>();
        effectCounter = content.Find("PlayerHPText/NumberText")?.GetComponent<TextMeshProUGUI>();

        if (cardHPSlider == null) Debug.LogError($"[{name}] HPSlider fehlt.");
        if (fireIcon == null) Debug.LogError($"[{name}] HPBar FireIcon fehlt.");
        if (frostIcon == null) Debug.LogError($"[{name}] HPBar FrostIcon fehlt.");
        if (poisonIcon == null) Debug.LogError($"[{name}] HPBar PoisonIcon fehlt.");
        if (charBlockedIcon == null) Debug.LogError($"[{name}] CharBlockedIcon fehlt.");
        if (effectCounter == null) Debug.LogError($"[{name}] PlayerHPText/NumberText fehlt.");
    }

    private void CacheEffectReferences()
    {
        Transform handle = effectUI.Find("Unity Canvas/Content/Handle");

        if (handle == null)
        {
            Debug.LogError($"[{name}] CardEffect/Unity Canvas/Content/Handle nicht gefunden.");
            return;
        }

        fireIconProperty = handle.Find("FireIconProperty")?.GetComponent<Image>();
        frostIconProperty = handle.Find("FrostIconProperty")?.GetComponent<Image>();
        poisonIconProperty = handle.Find("PoisonIconProperty")?.GetComponent<Image>();

        if (fireIconProperty == null) Debug.LogError($"[{name}] CardEffect FireIconProperty fehlt.");
        if (frostIconProperty == null) Debug.LogError($"[{name}] CardEffect FrostIconProperty fehlt.");
        if (poisonIconProperty == null) Debug.LogError($"[{name}] CardEffect PoisonIconProperty fehlt.");
    }

    private void ValidateHPBarReferences()
    {
        if (cardHPSlider == null) Debug.LogError($"[{name}] HPSlider fehlt.");
        if (fireIcon == null) Debug.LogError($"[{name}] FireIcon fehlt.");
        if (frostIcon == null) Debug.LogError($"[{name}] FrostIcon fehlt.");
        if (poisonIcon == null) Debug.LogError($"[{name}] PoisonIcon fehlt.");
        if (charBlockedIcon == null) Debug.LogError($"[{name}] CharBlockedIcon fehlt.");
        if (effectCounter == null) Debug.LogError($"[{name}] NumberText/effectCounter fehlt.");
    }

    public void SetHPUI()
    {
        if (cardHPSlider == null) return;
        cardHPSlider.SetValueWithoutNotify(hpPercentage);
    }

    Effect propertyEffect = Effect.NONE;
    bool propertyUIAlreadySet = false;
    private void SetPropertyUI()
    {
        if(propertyUIAlreadySet) return;
        if(procEffectsOnAttacks.Count<1) return;
        propertyEffect = procEffectsOnAttacks[0];
        switch (propertyEffect)
        {
            case Effect.FROST: 
                SetAlpha(fireIconProperty, 0);
                SetAlpha(frostIconProperty, 1);
                SetAlpha(poisonIconProperty, 0);
                break;
            case Effect.FIRE: 
                SetAlpha(fireIconProperty, 1);
                SetAlpha(frostIconProperty, 0);
                SetAlpha(poisonIconProperty, 0);
                break;
            case Effect.STUN: 
                SetAlpha(fireIconProperty, 0);
                SetAlpha(frostIconProperty, 0);
                SetAlpha(poisonIconProperty, 1);
                break;
            default:
                SetAlpha(fireIconProperty, 0);
                SetAlpha(frostIconProperty, 0);
                SetAlpha(poisonIconProperty, 0);
                break;
        }
        propertyUIAlreadySet = true;
    }

    public void SetEffectUI(Effect effect)
    {
        switch (effect)
        {
            case Effect.FROST: 
                SetAlpha(fireIcon, 0);
                SetAlpha(frostIcon, 1);
                SetAlpha(poisonIcon, 0);
                break;
            case Effect.FIRE: 
                SetAlpha(fireIcon, 1);
                SetAlpha(frostIcon, 0);
                SetAlpha(poisonIcon, 0);
                break;
            case Effect.STUN: 
                SetAlpha(fireIcon, 0);
                SetAlpha(frostIcon, 0);
                SetAlpha(poisonIcon, 1);
                break;
            default:
                SetAlpha(fireIcon, 0);
                SetAlpha(frostIcon, 0);
                SetAlpha(poisonIcon, 0);
                break;
        }
    }
    
    private void SetGreyoutUI()
    {
        if (greyOut)
        {
            SetAlpha(charBlockedIcon, 1);
        }
        else
        {
            SetAlpha(charBlockedIcon, 0);
        }
        
    }
    private Vector3 hpBarOffset = new Vector3(0f, 0.105f, 0f);
    private Vector3 propertyBarOffset = new Vector3(0f, -0.105f, 0f);
    private Vector3 tempPos = new Vector3(0,0,0);
    public void UpdateHPBarPosition()
    {
        if (hpBarUI == null) return;
        // Debug.Log("[position] current model pos: " + currentPos);
        tempPos = currentPos+hpBarOffset;
        // Debug.Log("[position] offset hp bar pos: " + tempPos);
        hpBarUI.position = tempPos;

        if (effectUI == null) return;
        tempPos = currentPos+propertyBarOffset;
        effectUI.position = tempPos;
    }

    public void SetAlpha(Image icon, float a)
    {
        if (icon == null) return;
        Color c = icon.color;
        c.a = Mathf.Clamp01(a);
        icon.color = c;
    }
    public void SetText(string newText, TextMeshProUGUI tmp)
    {
        if (tmp != null)
        {
            tmp.text = newText;
        }
    }


}
