using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RTP : MonoBehaviour {

    AudioSource audioSource;
    GameController gg;

    public List<Character> turn;

    bool changeTurn = true;
    int characterTurn = 0;
    int battleStatus = 0;
    int skillStatus = -1;
    int targetEnemySelected = 0;
    int targetCharacterSelected = 0;

    float C_ZERO = 0f;

    //public Transform target1;
    //public Transform target2;

    [Header("Characters")]
    public List<GameObject> characterLoadedList;
    List<Character> BattlersList = new List<Character>();
    
    [Header("BattleMenu")]
    public List<Button> battleMenuButton;
    public GameObject skillContent;
    public GameObject prefabPower;

    [Header("SoundsMenu")]
    public List<AudioClip> soundList;

    int totalEnemy = 1;
    bool winConditionPlayed = false;

    // Use this for initialization
    void Start () {

        audioSource = new AudioSource();
        gg = new GameController();

        //IF CHARACTERS = NULL ? RETURN INTRO//
        if (gg.GetAllCharactersList() == null)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            //GET CHARACTER LIST//
            var allCharacter = gg.GetCharactersList();
            var totalCount = 0;
            var count = 0;

            //PRE EVENTS//
            if (gg.GetOpenLevel().Equals(2))
            {
                if (gg.GetCharactersList().Find(o => o.ID == 2) == null)
                {
                    Debug.Log("OLD MAN IS ADDED");
                    gg.GetCharactersList().Add(gg.GetAllCharactersList()[1]);
                    gg.GetCurrentCharactersList().Add(2);
                }
                else
                {
                    Debug.Log("OLD MAN CANNOT BE ADDED");
                }
            }
               

            //LOAD CHARACTER SPRITES & STATS//
            foreach (var i in gg.GetCurrentCharactersList())
            {
                count++;
                var o = allCharacter.Find(ch => ch.ID == i);
                o.TempName = "Player" + count;
                o.StatsInGame = o.Stats;
                BattlersList.Add(o);
                GameObject.Find("Player" + count).GetComponent<CharacterBehaviour>().CharacterArrayID = totalCount;
                GameObject.Find("Player" + count).GetComponent<SpriteRenderer>().sprite = o.Sprite;
                GameObject.Find("Player" + count).GetComponent<Animator>().runtimeAnimatorController = o.Animator;
                GameObject.Find("Player" + count).GetComponent<CharacterBehaviour>().characterSounds = o.Sounds;
                totalCount++;
            }
            //CLEAN NOT USED PLAYERS//
            for (var i = count + 1; i <= 3; i++) { GameObject.Find("Player" + i).SetActive(false); }

            var allEnemies = gg.GetEnemiesList();

            count = 0;
            
            var enemyLevelList = new List<Enemy>();

            if (gg.GetOpenLevel().Equals(1))
            {
                enemyLevelList.Add(allEnemies[0]);
            }
            else if(gg.GetOpenLevel().Equals(2))
            {
                enemyLevelList.Add(allEnemies[0]);
                enemyLevelList.Add(allEnemies[1]);
            }
            else if (gg.GetOpenLevel().Equals(3))
            {
                enemyLevelList.Add(allEnemies[0]);
            }
            else if (gg.GetOpenLevel().Equals(4))
            {
                enemyLevelList.Add(allEnemies[2]);
                enemyLevelList.Add(allEnemies[0]);
                enemyLevelList.Add(allEnemies[1]);
            }

            totalEnemy = enemyLevelList.Count;

            //LOAD ENEMIES SPRITES & STATS//
            foreach (var i in enemyLevelList)
            {
                count++;
                i.TempName = "Enemy" + count;
                i.StatsInGame = i.Stats;
                BattlersList.Add(i);

                Debug.Log(i.Name + " has " + i.StatsInGame.HP + " HP");

                GameObject.Find("Enemy" + count).GetComponent<CharacterBehaviour>().CharacterArrayID = totalCount;
                GameObject.Find("Enemy" + count).GetComponent<SpriteRenderer>().sprite = i.Sprite;
                GameObject.Find("Enemy" + count).GetComponent<Animator>().runtimeAnimatorController = i.Animator;
                GameObject.Find("Enemy" + count).GetComponent<CharacterBehaviour>().characterSounds = i.Sounds;

                totalCount++;
            }

            //CLEAN NOT USED ENEMIES//
            for (var i = count + 1; i <= 3; i++) { GameObject.Find("Enemy" + i).SetActive(false); }

            //ADD COMMAND BUTTON//
            for (int i = 0; i < battleMenuButton.Count; i++)
            {
                var newi = i + 1;
                UnityAction<int> action = new UnityAction<int>(ButtonDoAction);
                battleMenuButton[i].onClick.AddListener(delegate { action.Invoke(newi); });
            }
            
            //ORDER TURNS//
            GenerateOrder();
        }
    }
    
    bool targetReach = false;
    
    bool isCharacterDoingAction = true;
    bool isEnemyDoingAction = true;
    int enemyIsAttacking = 0;
	// Update is called once per frame
	void Update () {

        var oo = GetCharacterTurn(characterTurn);

        //Debug.Log(oo.TempName + " EIA: " + enemyIsAttacking + " IDA: " + isEnemyDoingAction);

        //Debug.Log(turn[2].TempName);
        //Debug.Log(turn[3].TempName);

        if (!oo.IsPlayer){
            //DEFAULT ATTACK//
            if (isEnemyDoingAction && enemyIsAttacking == 0)
            {
                //var enemyInstance = GetCharacterTurn(characterTurn);

                Debug.Log(oo.TempName + " : " + oo.Stats.HP);

                if(oo.StatsInGame.HP>0){
                    Debug.Log("ENEMY IS ATTACKING");
                    isEnemyDoingAction = false;
                    targetReach = false;
                    isCharacterDoingAction = false;
                    enemyIsAttacking = 1;
                    StartCoroutine(Attack(GameObject.Find(oo.TempName).GetComponent<Transform>(), 
                                          GameObject.Find(string.Concat("Player", "1")).GetComponent<Transform>()));
                    
                } else{
                    ChangeTurnMethod();
                }
                //StartCoroutine(Attack(GameObject.Find(string.Concat("Player", "1")).GetComponent<Transform>(), GameObject.Find(GetCharacterTurn(characterTurn).TempName).GetComponent<Transform>()));
            }
        } 
        else
        {
            var character = GetCharacterTurn(characterTurn);
            var o = GameObject.Find("Enemies");
            if (battleStatus.Equals(GameConstants.BS_ATTACK)) { //ATTACK!

                //Debug.Log("dd: " + character.TempName);

                ShowSkillAnim(character.TempName, false);
                //Debug.Log("ESPERANDO TARGET : TS (" + targetEnemySelected+ ")");
                if (targetEnemySelected !=0)
                {
                    if(isCharacterDoingAction)
                    {
                        isCharacterDoingAction = false;
                        ShowEnemyTargets(false);
                        isCharacterDoingAnimation = true;
                        targetReach = false;                        
                        StartCoroutine(Attack(GameObject.Find(character.TempName).GetComponent<Transform>(), GameObject.Find("Enemy"+targetEnemySelected).GetComponent<Transform>()));
                    }
                }
                else 
                {
                    ShowEnemyTargets(true);
                }
            }
            else if(battleStatus == 2)
            {
                ShowSkillAnim(character.TempName, true);
                if (skillStatus != GameConstants.SKILL_TYPE_NONE)
                {
                    var skillSelected = character.Skills[skillStatus];

                    if (skillSelected.Type.Equals(GameConstants.SKILL_TYPE_FOR_SELF))
                    {

                    }
                    else if (skillSelected.Type.Equals(GameConstants.SKILL_TYPE_FOR_ALLY))
                    {

                    }
                    else if (skillSelected.Type.Equals(GameConstants.SKILL_TYPE_FOR_ENEMY))
                    {
                        if (targetEnemySelected != 0)
                        {
                            ShowEnemyTargets(false);
                            isCharacterDoingAnimation = true;
                            targetReach = false;
                            isCharacterDoingAction = false;
                            StartCoroutine(Attack(GameObject.Find(character.TempName).GetComponent<Transform>(), GameObject.Find("Enemy" + targetEnemySelected).GetComponent<Transform>()));
                        }
                        else
                        {
                            ShowEnemyTargets(true);
                        }
                    }
                }
                
            }
            else if (battleStatus.Equals(GameConstants.BS_ITEM))
            {
                if (ItemSelected != null)
                {
                    var Formula = ItemSelected.Formula.Split('|');
                    if (Formula[0].Equals(GameConstants.FORMULA_ALLY))
                    {
                        if (targetCharacterSelected != 0)
                        {
                            if (isCharacterDoingAction)
                            {
                                Debug.Log("PRE-ITEM");
                                isCharacterDoingAction = false;
                                ShowAllyTargets(false);
                                isCharacterDoingAnimation = true;
                                StartCoroutine(ItemUse(GameObject.Find("Player" + targetCharacterSelected).transform));
                            }
                        }
                        else
                        {
                            ShowAllyTargets(true);
                        }
                    }
                    else
                    {
                        ShowEnemyTargets(true);
                    }
                }
            }
            else 
            {
                ShowSkillAnim(character.TempName, false);
                ShowEnemyTargets(false);
            }
        }
        
        /**[DRAW SKILLS]**/
        if (changeTurn)
        {
            var currentSkills = GameObject.Find("ContentSkill");
            for (var i = 0; i < skillContent.transform.childCount; i++)
            {
                Destroy(skillContent.transform.GetChild(i).gameObject);
            }

            changeTurn = false;
            var ch = GetCharacterTurn(characterTurn);
			if (ch.Skills != null) {
				for (var i = 0; i < ch.Skills.Count; i++)
				{   
					GameObject Prefab = Instantiate(prefabPower);
					Prefab.transform.GetComponent<Image>().sprite = ch.Skills[i].Icon;
					Prefab.transform.Find("Panel/Text").GetComponent<Text>().text = ch.Skills[i].Name;

					var newi = i;
                    UnityAction<int> action = new UnityAction<int>(ButtonDoSkill);
                    Prefab.transform.GetComponent<Button>().onClick.AddListener(delegate { action.Invoke(newi); });

					Prefab.transform.SetParent(skillContent.transform);

				}
			}           
        }

        if (totalEnemy == 0)
        {
            if (!winConditionPlayed)
            {
                winConditionPlayed = true;
                StartCoroutine(WinCondition());
            }
            
        }
    }

    int defaultX = -2;
    int defaultY = 3;

    Vector3 defaultPosition = new Vector3(-2, 3, 0);
    public Vector3 CharacterFormation(int position){
        var pos = new Vector3(defaultX - (position * 2), defaultY - (position * 1.5f), 0);
//        Debug.Log(pos);
        return pos;
    }

    #region "BATTLE MENU"

    void ButtonDoAction(int power)
    {
        battleStatus = power;
        PlaySound(GameConstants.SOUND_CLICK);
        transform.Find("BattlePanel/SkillPanel").GetComponent<Transform>().gameObject.SetActive(false);
        transform.Find("BattlePanel/ObjectPanel").GetComponent<Transform>().gameObject.SetActive(false);

		if (power.Equals (GameConstants.BS_ITEM)) {
			transform.Find ("BattlePanel/ObjectPanel").GetComponent<Transform> ().gameObject.SetActive (true);
		} else if (power.Equals (GameConstants.BS_SKILL)) {
			transform.Find ("BattlePanel/SkillPanel").GetComponent<Transform> ().gameObject.SetActive (true);
		} else if (power.Equals (GameConstants.BS_RUN)) {
            Run();
		}
    }

    void ButtonDoSkill(int power)
    {
        battleStatus = 2;
        skillStatus = power;
        var character = GetCharacterTurn(characterTurn);
        Debug.Log(character.Name + " will use " + character.Skills[power].Name);
    }

    #endregion

    #region "ACTIONS"

    IEnumerator ShowMessage(string message)
    {
        transform.Find("MessagePanel").GetComponent<Transform>().gameObject.SetActive(true);
        transform.Find("MessagePanel/Text").GetComponent<Text>().text = message;
        yield return new WaitForSeconds(0.8f);
        GameObject.Find("MessagePanel").SetActive(false);
        yield return null;
    }

    void ReachTarget(Character first, Transform target1, Transform target2){
        if (!first.IsRange)
        {
            float distanceTarget = Vector2.Distance(target1.position, target2.Find("TargetPlace").GetComponent<Transform>().position);
            if (distanceTarget.CompareTo(C_ZERO) != 0 && !targetReach)
            {
                target1.position = Vector2.MoveTowards(target1.position, target2.Find("TargetPlace").GetComponent<Transform>().position, 7.5f * Time.deltaTime);
                target1.GetComponent<Animator>().SetBool("Run", true);
            }
            else
            {
                targetReach = true;
            }
        }
        else
        {
            targetReach = true;
        }

        //Debug.Log("OOOO === OOOO ");

    }

    void AttackAnimation(Character first, Transform target1, Transform target2){
        var tar2 = target2.GetComponent<CharacterBehaviour>().CharacterArrayID;
        var second = GetCharacterStats(tar2);

        targetReach = false;

        isCharacterDoingAction = true;
        isEnemyDoingAction = true;

        StartCoroutine(ShowMessage(first.Name + " is attacking"));

        targetEnemySelected = 0;
        battleStatus = 0;
        target1.GetComponent<Animator>().SetTrigger("DealDamage");
        var damageDone = CharacterDamage(first, second, 1);
        target2.Find("Damage").GetComponent<TextMesh>().text = damageDone.ToString();
        target2.Find("Damage").GetComponent<TextMesh>().color = Color.white;
        target2.Find("Damage").GetComponent<Transform>().gameObject.SetActive(true);

        target2.GetComponent<Animator>().SetTrigger("Hit");

        BattlersList[tar2].StatsInGame.HP -= damageDone;

        if (BattlersList[tar2].StatsInGame.HP < 0)
        {

            Debug.Log(BattlersList[tar2].Name + " takes " + damageDone + " damage");

            if (!second.IsPlayer)
            {
                target2.GetComponent<Animator>().SetTrigger("Death");
                totalEnemy--;
            }
            else
            {
                target2.GetComponent<Animator>().SetTrigger("KO");
            }

        }

    }

    public void UsingItem(Transform target)
    {
        var tar = target.GetComponent<CharacterBehaviour>().CharacterArrayID;

        isCharacterDoingAction = true;
        isEnemyDoingAction = true;

        targetCharacterSelected = GameConstants.NO_SELECTED;
        targetEnemySelected = GameConstants.NO_SELECTED;
        battleStatus = GameConstants.NO_SELECTED;

        StartCoroutine(ShowMessage(BattlersList[tar].Name + " used " + ItemSelected.Name));

        var formula = ItemSelected.Formula.Split('|');

        Debug.Log(formula[0]);

        if (formula[0].Equals(GameConstants.FORMULA_ALLY))
        {
            if (formula[1].Equals("HEAL"))
            {
                if (BattlersList[tar].Stats.HP > BattlersList[tar].StatsInGame.HP + int.Parse(formula[2]))
                {
                    BattlersList[tar].StatsInGame.HP = BattlersList[tar].Stats.HP;
                }
                else
                {
                    BattlersList[tar].StatsInGame.HP += int.Parse(formula[2]);
                }
                //target.GetComponent<Animator>().SetTrigger("Heal");
                Debug.Log(BattlersList[tar].TempName + " WILL BE CURE ... " + target.name);

                target.Find("Damage").GetComponent<TextMesh>().text = formula[2].ToString();
                target.Find("Damage").GetComponent<TextMesh>().color = Color.green;
                target.Find("Damage").GetComponent<Transform>().gameObject.SetActive(true);
                gg.GetCharacterItemList().Find(o => o.Item.ID == ItemSelected.ID).Quantity--;
                var itemTemp = gg.GetCharacterItemList().Find(o => o.Item.ID == ItemSelected.ID);
                GameObject.Find("ItemShort" + ItemSelected.ID + "/Total/Text").transform.GetComponent<Text>().text = itemTemp.Quantity.ToString();
                if (itemTemp.Quantity == 0)
                {
                    Destroy(GameObject.Find("ItemShort" + ItemSelected.ID));
                }

                PlaySound(2);
                ItemSelected = null;
                ChangeTurnMethod();
            }
        }
        else
        {

        }
    }

    bool isCharacterDoingAnimation = false;
    bool isEnemyDoingAnimation = true;
    IEnumerator Attack(Transform target1, Transform target2)
    {

        var first = GetCharacterStats(characterTurn);

        if(first.IsPlayer){
            ShowSkillAnim(first.TempName, false);

            while (isCharacterDoingAnimation)
            {
                ReachTarget(first, target1, target2);
                if (targetReach)
                {
                    AttackAnimation(first, target1, target2);
                    isCharacterDoingAnimation = false;
                }
                yield return null;
            }
        } else {

            Debug.Log("ENEMY WILL ATACK");

            while (isEnemyDoingAnimation)
            {
                ReachTarget(first, target1, target2);
                if (targetReach)
                {
                    AttackAnimation(first, target1, target2);
                    isEnemyDoingAnimation = false;
                }
                yield return null;
            }
        }
    }

    IEnumerator ItemUse(Transform target1)
    {
        while (isCharacterDoingAnimation)
        {
            Debug.Log("PRE-ITEM2");
            isCharacterDoingAnimation = false;
            UsingItem(target1);
            yield return null;
        }
    }

    public void ShowSkillAnim(string name, bool val)
    {
        if(!name.Contains("Enemy"))
        GameObject.Find(name).GetComponent<Transform>().Find("SkillActive").gameObject.SetActive(val);
    }

    public void ShowEnemyTargets(bool val){
        var enemyList = GameObject.Find("Enemies");
        for(var i = 0; i < enemyList.transform.childCount; i++){
            var enemy = enemyList.transform.GetChild(i).GetComponent<Transform>();
            if(enemy.GetComponent<SpriteRenderer>().color.a != 0)
            {
                enemyList.transform.GetChild(i).GetComponent<Transform>().Find("TargetDamage").gameObject.SetActive(val);
            }            
        } 
    }

    public void ShowAllyTargets(bool val)
    {
        var enemyList = GameObject.Find("Characters");
        for (var i = 0; i < enemyList.transform.childCount; i++)
        {
            var enemy = enemyList.transform.GetChild(i).GetComponent<Transform>();
            if (enemy.GetComponent<SpriteRenderer>().color.a != 0)
            {
                enemyList.transform.GetChild(i).GetComponent<Transform>().Find("TargetDamage").gameObject.SetActive(val);
            }
        }
    }

    public void Run()
    {
        int c = Random.Range(0, 2);
        Debug.Log(c);
        if (c.Equals(0))
        {
            PlaySound(GameConstants.SOUND_RUN);
            SceneManager.LoadScene(5);
        }
        else
        {
            PlaySound(GameConstants.SOUND_CANT_RUN);
            ChangeTurnMethod();
        }
    }

    #endregion

    #region "CHANGE TURN"

    public void GenerateOrder()
    {
        turn = new List<Character>();
        turn.AddRange(BattlersList);
        turn.Sort((first, second) => second.Stats.SPE.CompareTo(first.Stats.SPE));

        LogTurn();
    }

    public void ChangeTurnMethod()
    {
        LogTurn();

        //CHANGE TURN//
        characterTurn++;
        if (characterTurn == turn.Count) characterTurn = 0;

        Debug.Log(turn[characterTurn].ID + " - " + turn[characterTurn].Name + " >> ");

        isCharacterDoingAction = true;
        isEnemyDoingAnimation = true;
        enemyIsAttacking = 0;

        changeTurn = true;
    }

    IEnumerator WinCondition()
    {
        yield return new WaitForSeconds(3);
        foreach (var i in BattlersList)
        {
            if (i.TempName.Contains("Player") && i.Status != -1)
            {
                GameObject.Find(i.TempName).GetComponent<Animator>().SetBool("Win", true);
            }
        }
        GameObject.Find("Main Camera").GetComponent<AudioSource>().Stop();
        PlaySound(1);
        transform.Find("Victory").GetComponent<Transform>().gameObject.SetActive(true);
    }

    #endregion

    #region "GETTER AND SETTERS"

    Item ItemSelected;

    public void SetBattleStatusForItem(Item item)
    {
        battleStatus = GameConstants.BS_ITEM;
        var character = GetCharacterTurn(characterTurn);
        ItemSelected = item;
        Debug.Log(character.Name + " will use an " + item.Name);
    }

    public void SetTargetEnemySelected(int t)
    {
        //Debug.Log("CHOOSE " + t);
        isCharacterDoingAction = true;
        targetEnemySelected = t;
    }

    public void SetTargetCharacterSelected(int t)
    {
        //Debug.Log("CHOOSE " + t);
        isCharacterDoingAction = true;
        targetCharacterSelected = t;
    }

    public Character GetCharacterStats(int p)
    {
        return BattlersList[p];
    }

	public Character GetCharacterTurn(int p)
	{
        if (turn != null)
        {
            return turn[p];
        }
        return null;
	}

	public int GetCharacterTurn()
	{
		return characterTurn;
	}

    public int CharacterDamage(Character attack, Character defense, int damagetType){
        var damageDone = 0; 
        if(damagetType==1){
            if(attack.StatsInGame.ATK>defense.StatsInGame.DEF){
                damageDone = attack.StatsInGame.ATK - defense.StatsInGame.DEF;
                if(damageDone<0) damageDone = 1;
            }
        } else if(damagetType==2){
            if(attack.StatsInGame.MAG>defense.StatsInGame.MDF){
                damageDone = attack.StatsInGame.MAG - defense.StatsInGame.MDF;
                if (damageDone < 0) damageDone = 1;
            }
        }
        return damageDone;
    }

    #endregion

    #region "SOUND"

    public void PlaySound(int i)
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.PlayOneShot(soundList[i]);
    }

    #endregion

    void LogTurn()
    {
        var xx = "";
        foreach (var o in turn)
        {
            xx += o.ID + " >> ";
        }
        Debug.Log(xx);
    }

}
