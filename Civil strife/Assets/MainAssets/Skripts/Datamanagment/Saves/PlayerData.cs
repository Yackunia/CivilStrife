[System.Serializable]
public class PlayerData
{
    //inventoty
    public bool[] weapons;
    public bool[] helmets;
    public bool[] breastPlates;
    public bool[] boots;
    public bool[] stands;

    public int standId;
    public int helmetId;
    public int breastPlateId;
    public int bootsId;
    public int weaponId;
    public int sekWeaponId;
    public int moneyCount;
    public int activeAbilityId;

    public int[] sekWeapons;
    public int[] activeAbilitys;

    public int[][] garbage;

    //bestiary
    public bool[] isOpened;

    //scene management
    public bool[] isScene;

    public int sceneId;
    public int health;

    public float stamina;
    public float armorData;//save this

    //respawn
    public bool isRespawn;
    public int campId;
    public int sceneOfCampId;
    public PlayerData(PlayerInventory data)
    {
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
        sekWeaponId = data.sekWeaponId;
        moneyCount = data.moneyCount;
        activeAbilityId = data.activeAbilitysId;

        sekWeapons = data.sekWeapons;
        activeAbilitys = data.activeAbilitys;

        garbage = data.garbage;

        //bestiary
        isOpened = data.isOpened;

        //scene management
        isScene = data.isScene;

        sceneId = data.sceneId;
        health = data.health;
        stamina = data.standsManager.stamina;
        armorData = data.armorData;

        //respawn
        isRespawn = data.isRespawn;
        campId = data.campId;
        sceneOfCampId = data.sceneOfCampId;
    }
}
