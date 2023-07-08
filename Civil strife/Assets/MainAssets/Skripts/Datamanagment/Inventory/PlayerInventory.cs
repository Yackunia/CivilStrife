using UnityEngine.UI;
using UnityEngine;
using System;

public class PlayerInventory : MonoBehaviour
{
    public Stands standsManager;

    [Header("Data")]
    public bool[] weapons;//save this
    public bool[] helmets;//save this
    public bool[] breastPlates;//save this
    public bool[] boots;//save this
    public bool[] stands;//save this 

    public int standId;//save this
    public int helmetId;//save this
    public int breastPlateId;//save this
    public int bootsId;//save this
    public int weaponId;//save this
    public int sekWeaponId;//save this
    public int moneyCount;//save this
    public int activeAbilitysId;//save this

    public int[] sekWeapons;//save this
    public int[] activeAbilitys;//save this

    public int[][] garbage;//save this

    [Header("inventory Output")]
    [SerializeField] private RectTransform weaponParent;
    [SerializeField] private RectTransform sekWeaponParent;
    [SerializeField] private RectTransform abilityParent;
    [SerializeField] private RectTransform standsParent;
    [SerializeField] private RectTransform helmetParent;
    [SerializeField] private RectTransform breastParent;
    [SerializeField] private RectTransform bootsParent;

    //front Images
    [SerializeField] private Image[] weaponImage;
    [SerializeField] private Image[] sekWeaponImage;
    [SerializeField] private Image helmetImage;
    [SerializeField] private Image breastImage;
    [SerializeField] private Image bootsImage;
    [SerializeField] private Image standsImage;
    [SerializeField] private Image abilityImage;
<<<<<<< Updated upstream
=======

>>>>>>> Stashed changes
    //front Sprites
    [SerializeField] private Sprite[] weaponSprite;
    [SerializeField] private Sprite[] sekWeaponSprite;
    [SerializeField] private Sprite[] helmetSprite;
    [SerializeField] private Sprite[] breastSprite;
    [SerializeField] private Sprite[] bootsSprite;
    [SerializeField] private Sprite[] standsSprite;
    [SerializeField] private Sprite[] abilitySprite;
    [SerializeField] private Sprite[] garbSprites;

    //downloadableObj
    [SerializeField] private GameObject[] weaponObjcts;
    [SerializeField] private GameObject[] sekWeaponObjcts;
    [SerializeField] private GameObject[] helmetObjcts;
    [SerializeField] private GameObject[] breastObjcts;
    [SerializeField] private GameObject[] bootsObjcts;
    [SerializeField] private GameObject[] stansObjcts;
    [SerializeField] private GameObject[] abilityObjcts;

    //CountOutputs
    [SerializeField] private Text[] moneyCountOutput;
    [SerializeField] private Text[] sekWeaponCountOutput;
    [SerializeField] private Text[] abilityCountOutput;
    [SerializeField] private Text[] garbageCountOutput;

    [Header("Avatar")]
    [SerializeField] private SpriteRenderer[] bodyPart;
    [SerializeField] private SpriteRenderer headPart;

    [SerializeField] private Sprite[] headSprite;

    [SerializeField] private Sprite[] bodySprite;
    [SerializeField] private Sprite[] leftSholderSprite;
    [SerializeField] private Sprite[] leftArmSprite;
    [SerializeField] private Sprite[] leftHandSprite;
    [SerializeField] private Sprite[] rightSholderSprite;
    [SerializeField] private Sprite[] rightArmSprite;
    [SerializeField] private Sprite[] rightHandSprite;
    [SerializeField] private Sprite[] leftLeg1Sprite;
    [SerializeField] private Sprite[] leftLeg2Sprite;
    [SerializeField] private Sprite[] rightLeg1Sprite;
    [SerializeField] private Sprite[] rightLeg2Sprite;
    [SerializeField] private Sprite[] DickSprite;

    [SerializeField] private Sprite[] leftFootSprite;
    [SerializeField] private Sprite[] rightFootSprite;

    [Header("Bestiary")]
    public bool[] isOpened;//save this 

    [SerializeField] private GameObject[] characters;

    [Header("SceneManagment")]
    public bool isMenu;
    public bool[] isScene;//save this  

    public int sceneId;//save this
    public int health;//save this
    [SerializeField] private int[] countOfInputersOnScene;

    public float armorData;//save this

    [SerializeField] private Vector2[] scenePos;

    [Header("Respawn")]
    public bool isRespawn;//save this

    public int campId;//save this
    public int sceneOfCampId;//save this

    [SerializeField] private Vector2[] campPos;

    //Ура, ебаные данные кончились
    [SerializeField] private Loader loader;
    [SerializeField] private SceneData sceneData;
<<<<<<< Updated upstream
    [Header("Player")]
    [SerializeField] private PlayerHealth pl_Health;
    [SerializeField] private WeaponManager weaponManager;
=======
    [SerializeField] private Slovar slovar;

    [Header("Player")]
    [SerializeField] private PlayerHealth pl_Health;
    [SerializeField] private WeaponManager weaponManager;
    [SerializeField] private PlayerAttackSistem attacker;
>>>>>>> Stashed changes
    public Armor armor;

    [SerializeField] private GameObject spawnText;
    [SerializeField] private Transform spawntextParent;

    private Transform pl_Transform;

    private void Start()
    {
<<<<<<< Updated upstream
=======
        SetInvBut(false);

>>>>>>> Stashed changes
        pl_Transform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        sceneData = GameObject.Find("SceneData").GetComponent<SceneData>();

        if (!isMenu) SetData();
    }

    private void Update()
    {
        UpdInfo();
    }

    private void UpdInfo()
    {
        for (int i = 0; i < moneyCountOutput.Length; i++) moneyCountOutput[i].text = moneyCount.ToString();
        for (int i = 0; i < sekWeaponCountOutput.Length; i++) sekWeaponCountOutput[i].text = sekWeapons[sekWeaponId].ToString();
        for (int i = 0; i < abilityCountOutput.Length; i++) abilityCountOutput[i].text = activeAbilitys[activeAbilitysId].ToString();
        //for (int i = 0; i < garbageCountOutput.Length; i++) garbageCountOutput[i].text = ga.ToString();
    }

    #region Save/Load Data
    public void SetData()
    {
        Load();

        if(!isRespawn) SetSceneData();
        else SetRespawn();

        SetInventory();
    }

    //private void LoadOnlyInventory

    private void SetRespawn()
    {
        pl_Transform.position = campPos[campId];
        pl_Health.healthPoint = pl_Health.maxHP;
    }

    private void SetSceneData()
    {
        int flagCount = 0;
        for (int i = 0; i < isScene.Length; i++)
        {
            if (isScene[i]) pl_Transform.position = scenePos[flagCount + sceneId];//не будет корректно работать, если на сцену нет возможности попать из другой

            else flagCount += countOfInputersOnScene[i];
        }
        pl_Health.healthPoint = health;
    }
    private void Load()
    {
        var data = SaveData.LoadPlayerData();
        //inventoty
        weapons = data.weapons;
        helmets = data.helmets;
        breastPlates = data.breastPlates;
        boots = data.boots;
        stands = data.stands;

        standId = data.standId;
        helmetId = data.helmetId;
        breastPlateId = data.breastPlateId;
        bootsId = data.bootsId;
        weaponId = data.weaponId;
        moneyCount = data.moneyCount;
<<<<<<< Updated upstream
=======
        sekWeaponId = data.sekWeaponId;
>>>>>>> Stashed changes

        sekWeapons = data.sekWeapons;
        activeAbilitys = data.activeAbilitys;

        garbage = data.garbage;

        //bestiary
        isOpened = data.isOpened;

        //scene management
        isScene = data.isScene;

        sceneId = data.sceneId;
        health = data.health;

        standsManager.stamina = data.stamina;
        armorData = data.armorData;

        //respawn
        isRespawn = data.isRespawn;
        campId = data.campId;
        sceneOfCampId = data.sceneOfCampId;
    }

    public void SaveAll()
    {
        SaveData.SavePlayerData(this);
    }
    public void SetCamp(int id, int scene)
    {
        campId = id;
        sceneOfCampId = scene;
        isRespawn = true;
        SaveAll();
    }

    public void LoadCamp()
    {
        isRespawn = true;
        SaveAll();
        loader.LoadScene(sceneOfCampId);
        sceneData.Respawn();
    }

    public void DestroySave()
    {
        SaveData.DestroyPlayerData();
    }
    #endregion

    # region Set Invetoty
    private void SetInventory()
    {
        SetOutput();

        MainSet();
        MainSelect();

        if (!isRespawn)
        {
            armor.valueOfArmor = armorData;
            armor.SetArmorObjParametersNotRespawn();
        }

        else
        {
            armor.SetArmorObjParameters();
        }
    }
    #endregion

    #region Output
    private void SetOutput()
    {
        OutputAllCharacter();
        OutputAllMoneyCount();
        OutputAllSekondWeapons(sekWeaponSprite[sekWeaponId]);
        OutputAllWeapons(weaponSprite[weaponId]);
    }

    private void OutputAllSekondWeapons(Sprite sprite)
    {
        for (int i = 0; i < sekWeaponCountOutput.Length; i++)
        {
            sekWeaponCountOutput[i].text = sekWeapons[sekWeaponId].ToString();
        }
        for (int i = 0; i < sekWeaponImage.Length; i++)
        {
            sekWeaponImage[i].sprite = sprite;
        }
    }

    private void OutputAllMoneyCount()
    {
        for (int i = 0; i < moneyCountOutput.Length; i++)
        {
            moneyCountOutput[i].text = moneyCount.ToString();
        }
    }

    private void OutputAllWeapons(Sprite sprite)
    {
        for (int i = 0; i < weaponImage.Length; i++)
        {
            weaponImage[i].sprite = sprite;
        }
    }

    private void OutputAllCharacter()
    {
        for (int i = 0; i < isOpened.Length; i++)
        {
            characters[i].SetActive(isOpened[i]);
        }
    }

    public void UpdHealth()
    {
        if (isRespawn) health = ((int)pl_Health.maxHP);
        else
        {
            health = ((int)pl_Health.healthPoint);
            armorData = armor.valueOfArmor;
        }
    }

    public void ChangeBody()
    {
        headPart.sprite = headSprite[helmetId];

        bodyPart[0].sprite = bodySprite[breastPlateId];
        bodyPart[1].sprite = leftSholderSprite[breastPlateId];
        bodyPart[2].sprite = leftArmSprite[breastPlateId];
        bodyPart[3].sprite = leftHandSprite[breastPlateId];
        bodyPart[4].sprite = rightSholderSprite[breastPlateId];
        bodyPart[5].sprite = rightArmSprite[breastPlateId];
        bodyPart[6].sprite = rightHandSprite[breastPlateId];
        bodyPart[7].sprite = leftLeg1Sprite[breastPlateId];
        bodyPart[8].sprite = leftLeg2Sprite[breastPlateId];
        bodyPart[9].sprite = rightLeg1Sprite[breastPlateId];
        bodyPart[10].sprite = rightLeg2Sprite[breastPlateId];
        bodyPart[14].sprite = DickSprite[breastPlateId];

        bodyPart[12].sprite = leftFootSprite[bootsId];
        bodyPart[13].sprite = rightFootSprite[bootsId];
    }
    #endregion

    #region Add Obj
    public void AddWeapon(int id)
    {
        weapons[id] = true;
        SetWeapon(id, true);
        var x = Instantiate(spawnText, spawntextParent).GetComponent<Text>();
        x.text = weaponObjcts[id].name + "у вас в инвентаре";
        SetALLWeapons();
    }
    public void AddSekWeapon(int id, int count)
    {
        sekWeapons[id] += count;
        SetSekWeapon(id, true);
        var x = Instantiate(spawnText, spawntextParent).GetComponent<Text>();
        x.text = sekWeaponObjcts[id].name + "(" + count.ToString() + " шт) у вас в инвентаре";
        SetALLSekWeapons();
    }
    public void AddArmor(int id)
    {
        breastPlates[id] = true;
        SetBreastPlate(id, true);
        var x = Instantiate(spawnText, spawntextParent).GetComponent<Text>();
        x.text = breastObjcts[id].name + "у вас в инвентаре";
        SetAllBreastPlates();
    }
    public void AddAbility(int id, int count)
    {
        activeAbilitys[id] += count;
        SetActiveAbility(id, true);
        var x = Instantiate(spawnText, spawntextParent).GetComponent<Text>();
        x.text = abilityObjcts[id].name + "у вас в инвентаре";
        SetALLAbilitys();
    }
    public void AddStand(int id)
    {
        stands[id] = true;
        SetStands(id, true);
        var x = Instantiate(spawnText, spawntextParent).GetComponent<Text>();
        x.text = stansObjcts[id].name + "у вас в инвентаре";
        SetAllStands();
    }
    public void AddHelmet(int id)
    {
        helmets[id] = true;
        SetHelmets(id, true);
        var x = Instantiate(spawnText, spawntextParent).GetComponent<Text>();
        x.text = helmetObjcts[id].name + "у вас в инвентаре";
        SetAllHelmets();
    }
    public void AddBoots(int id)
    {
        boots[id] = true;
        SetBoots(id, true);
        var x = Instantiate(spawnText, spawntextParent).GetComponent<Text>();
        x.text = bootsObjcts[id].name + "у вас в инвентаре";
        SetAllBoots();
    }
    public void AddMoney()
    {
        moneyCount += 1;
    }
    #endregion

    #region Remove Obj
    public void RemoveWeapon(int id)
    {
        weapons[id] = false;
        SetWeapon(id, false);
        var x = Instantiate(spawnText, spawntextParent).GetComponent<Text>();
        x.text = "Вы лишились - " + weaponObjcts[id].name;
        SetALLWeapons();
    }
    public void RemoveSekWeapon(int id, int count)
    {
        sekWeapons[id] -= count;
        if (sekWeapons[id] == 0) SetSekWeapon(id, false);

        SetALLSekWeapons();
    }
    public void RemoveArmor(int id)
    {
        breastPlates[id] = false;
        SetBreastPlate(id, false);
        var x = Instantiate(spawnText, spawntextParent).GetComponent<Text>();
        x.text = "Вы лишились - " + breastObjcts[id].name;

        SetAllBreastPlates();
    }
    public void RemoveAbility(int id, int count)
    {
        activeAbilitys[id] -= count;
        if (activeAbilitys[id] == 0) SetActiveAbility(id, false);

        SetALLAbilitys();
    }
    public void RemoveStand(int id)
    {
        stands[id] = false;
        SetStands(id, false);
        var x = Instantiate(spawnText, spawntextParent).GetComponent<Text>();
        x.text = "Вы лишились - " + stansObjcts[id].name;

        SetAllStands();
    }
    public void RemoveHelmet(int id)
    {
        helmets[id] = false;
        SetHelmets(id, false);
        var x = Instantiate(spawnText, spawntextParent).GetComponent<Text>();
        x.text = "Вы лишились - " + helmetObjcts[id].name;

        SetAllHelmets();
    }
    public void RemoveBoots(int id)
    {
        boots[id] = false;
        SetBoots(id, false);
        var x = Instantiate(spawnText, spawntextParent).GetComponent<Text>();
        x.text = "Вы лишились - " + bootsObjcts[id].name;

        SetAllBoots();
    }
    public void RemoveMoney(int count)
    {
        moneyCount -= count;
    }
    #endregion

    #region SetObj
    public void SetWeapon(int id, bool flag)
    {
        weaponObjcts[id].SetActive(flag);
    }
    public void SetSekWeapon(int id, bool flag)
    {
        sekWeaponObjcts[id].SetActive(flag);
    }
    public void SetBreastPlate(int id, bool flag)
    {
        breastObjcts[id].SetActive(flag);
    }
    public void SetActiveAbility(int id, bool flag)
    {
        abilityObjcts[id].SetActive(flag);
    }
    public void SetStands(int id, bool flag)
    {
        stansObjcts[id].SetActive(flag);
    }
    public void SetHelmets(int id, bool flag)
    {
        helmetObjcts[id].SetActive(flag);
    }
    public void SetBoots(int id, bool flag)
    {
        bootsObjcts[id].SetActive(flag);
    }
    #endregion

    #region Select Obj
    private void MainSelect()
    {
        SelectWeapon(weaponId);
        SelectSekWeapon(sekWeaponId);
        SelectAbility(activeAbilitysId);
        SelectBreast(breastPlateId);
        SelectStand(standId);
        SelectHelmet(helmetId);
        SelectBoots(bootsId);
    }

    public void SelectWeapon(int id)
    {
<<<<<<< Updated upstream
        if (weapons[id])
        {
            weaponId = id;
            OutputAllWeapons(weaponSprite[weaponId]);
        }
        weaponManager.SetweaponSprite(weaponId);
    }
    public void SelectSekWeapon(int id)
    {
        if (sekWeapons[id] > 0)
=======
        if (weapons[id] && !attacker.plAttacking())
        {
            weaponId = id;
            OutputAllWeapons(weaponSprite[weaponId]);

            weaponManager.SetweaponSprite(weaponId);

        }
    }
    public void SelectSekWeapon(int id)
    {
        if (sekWeapons[id] > 0) 
>>>>>>> Stashed changes
        {
            sekWeaponId = id;
            for (int i = 0; i < sekWeaponImage.Length; i++)
            {
                sekWeaponImage[i].sprite = sekWeaponSprite[sekWeaponId];
            }
        }
    }
    public void SelectAbility(int id)
    {
        if (activeAbilitys[id] > 0)
        {
            activeAbilitysId = id;

            abilityImage.sprite = abilitySprite[activeAbilitysId];
        }
    }
    public void SelectBreast(int id)
    {
        if (breastPlates[id])
        {
            breastPlateId = id;
            breastImage.sprite = breastSprite[breastPlateId];
<<<<<<< Updated upstream
        }
        ChangeBody();
=======
            ChangeBody();
            armor.SetArmorObjParameters();
        }
        
>>>>>>> Stashed changes
    }
    public void SelectStand(int id)
    {
        if (stands[id])
        {
            standId = id;
            standsImage.sprite = standsSprite[standId];
        }
    }
    public void SelectHelmet(int id)
    {
        if (helmets[id])
        {
            helmetId = id;
            helmetImage.sprite = helmetSprite[helmetId];
        }
        ChangeBody();
    }
    public void SelectBoots(int id)
    {
        if (boots[id])
        {
            bootsId = id;
            bootsImage.sprite = bootsSprite[bootsId];
<<<<<<< Updated upstream
        }
        ChangeBody();
=======
            ChangeBody();
        }
>>>>>>> Stashed changes
    }
    #endregion

    #region MainSet

    private void MainSet()
    {
        SetALLWeapons();
        SetALLSekWeapons();
        SetALLAbilitys();
        SetAllBreastPlates();
        SetAllStands();
        SetAllHelmets();
        SetAllBoots();
    }

    private void SetALLWeapons()
    {
        var x = 0;
        var parentHeight = 0f;

        for (int i = 0; i < weapons.Length; i++)
        {
            if (weapons[i])
            {
                x++;
                parentHeight += 220f;
            }
            SetWeapon(i, weapons[i]);
        }
        if (x == 0)
        {
            OutputAllWeapons(weaponSprite[0]);
            weaponImage[0].gameObject.SetActive(false);
        }
        else
        {
            OutputAllWeapons(weaponSprite[weaponId]);
            weaponImage[0].gameObject.SetActive(true);
        }

        weaponParent.sizeDelta = new Vector2(weaponParent.sizeDelta.x, parentHeight);
        weaponParent.localPosition = new Vector2(0f, parentHeight / 2);
    }
    private void SetALLSekWeapons()
    {
        var x = 0;
        var parentHeight = 0f;

        for (int i = 0; i < sekWeapons.Length; i++)
        {
            var l = true;
            if (sekWeapons[i] <= 0) l = false;
            else
            {
                parentHeight += 220f;
                x++;
            }
            SetSekWeapon(i, l);
        }
        if (x == 0)
        {
            OutputAllSekondWeapons(sekWeaponSprite[0]);
            sekWeaponImage[0].gameObject.SetActive(false);
        }
        else
        {
            OutputAllSekondWeapons(sekWeaponSprite[sekWeaponId]);
            sekWeaponImage[0].gameObject.SetActive(true);
        }

        sekWeaponParent.sizeDelta = new Vector2(sekWeaponParent.sizeDelta.x, parentHeight);
        sekWeaponParent.localPosition = new Vector2(0f, parentHeight / 2);
    }
    private void SetALLAbilitys()
    {
        var x = 0;
        var parentHeight = 0f;

        for (int i = 0; i < activeAbilitys.Length; i++)
        {
            var l = true;
            if (activeAbilitys[i] != 0)
            {
                x++;
                parentHeight += 400f;
            }
            else l = false;

            SetActiveAbility(i, l);
        }
        if (x == 0)
        {
            abilityImage.sprite = null;
            abilityImage.gameObject.SetActive(false);
        }
        else
        {
            abilityImage.sprite = abilitySprite[activeAbilitysId];
            abilityImage.gameObject.SetActive(true);
        }

        abilityParent.sizeDelta = new Vector2(abilityParent.sizeDelta.x, parentHeight);
        abilityParent.localPosition = new Vector2(0f, parentHeight / 2);
    }

    private void SetAllBreastPlates()
    {
        var x = 0;
        var parentHeight = 0f;

        for (int i = 0; i < breastPlates.Length; i++)
        {
            if (breastPlates[i])
            {
                x++;
                parentHeight += 300f;
            }
            SetBreastPlate(i, breastPlates[i]);

        }
        if (x == 0)
        {
            breastImage.sprite = null;
            breastImage.gameObject.SetActive(false);
        }
        else
        {
            breastImage.sprite = breastSprite[breastPlateId];
            breastImage.gameObject.SetActive(true);
        }

        breastParent.sizeDelta = new Vector2(breastParent.sizeDelta.x, parentHeight);
        breastParent.localPosition = new Vector2(0f, parentHeight / 2);
    }

    private void SetAllStands()
    {
        var x = 0;
        var parentHeight = 0f;

        for (int i = 0; i < stands.Length; i++)
        {
            if (stands[i])
            {
                x++;
                parentHeight += 600f;
            }
            SetStands(i, stands[i]);
        }
        if (x == 0)
        {
            standsImage.sprite = null;
            standsImage.gameObject.SetActive(false);
            standsManager.canUseStand = false;
        }
        else
        {
            standsImage.sprite = standsSprite[standId];
            standsImage.gameObject.SetActive(true);
            standsManager.canUseStand = true;
        }

        standsParent.sizeDelta = new Vector2(standsParent.sizeDelta.x, parentHeight);
        standsParent.localPosition = new Vector2(0f, parentHeight / 2);
    }

    private void SetAllHelmets()
    {
        var x = 0;
        var parentHeight = 0f;

        for (int i = 0; i < helmets.Length; i++)
        {
            if (helmets[i])
            {
                x++;
                parentHeight += 300f;
            }
            SetHelmets(i, helmets[i]);
        }
        if (x == 0)
        {
            helmetImage.sprite = null;
            helmetImage.gameObject.SetActive(false);
        }
        else
        {
            helmetImage.sprite = helmetSprite[activeAbilitysId];
            helmetImage.gameObject.SetActive(true);
        }

        helmetParent.sizeDelta = new Vector2(helmetParent.sizeDelta.x, parentHeight);
        helmetParent.localPosition = new Vector2(0f, parentHeight / 2);
    }

    private void SetAllBoots()
    {
        var x = 0;
        var parentHeight = 0f;

        for (int i = 0; i < boots.Length; i++)
        {
            if (boots[i])
            {
                x++;
                parentHeight += 200f;
            }
            SetHelmets(i, boots[i]);
        }
        if (x == 0)
        {
            bootsImage.sprite = null;
            bootsImage.gameObject.SetActive(false);
        }
        else
        {
            bootsImage.sprite = helmetSprite[activeAbilitysId];
            bootsImage.gameObject.SetActive(true);
        }

        bootsParent.sizeDelta = new Vector2(bootsParent.sizeDelta.x, parentHeight);
        bootsParent.localPosition = new Vector2(0f, parentHeight / 2);
    }
    
    #endregion
<<<<<<< Updated upstream
=======

    public void SetInvBut(bool flag)
    {
        breastImage.GetComponent<Button>().interactable = flag;
        abilityImage.GetComponent<Button>().interactable = flag;
        helmetImage.GetComponent<Button>().interactable = flag;
        bootsImage.GetComponent<Button>().interactable = flag;
    }
>>>>>>> Stashed changes
}
