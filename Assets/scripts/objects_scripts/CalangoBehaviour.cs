﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

abstract public class CalangoBehaviour : AnimalModel
{

    public Sprite mateSprite;
    public Sprite eatSprite;
    public Sprite combatSprite;
    public Sprite losingSprite;
    public Sprite heatSprite;

    public bool male;
    public float matingChance = 30;

    protected bool canReproduce = false;
    protected bool mating;

    protected SpriteRenderer actionIcon;

    protected CalangoBehaviour competitor;
    protected bool looser = false;
    protected bool noCloseFoodSource = false;

    protected GameObject closestEdible = null;

    protected float hideoutSearchCounter = 0;
    protected float hideoutSearchIntervalInSec = 1;

    protected float hideCounter = 0;
    protected float hiddingTimeInSec = 5;
    public float temperatureTransferInSec = .02f;
    public float maxBodyTemp = 33;
    public float highBodyTempThreshold = 30;
    public float lowBodyTempThreshold = 26;
    protected float currentTemp = 25;
    bool inShadow = false;

    GameRules rules;

    // Use this for initialization
     protected void Start()
    {
        base.Start();
        rules = GameObject.Find("MapController").GetComponent<GameRules>();
        actionIcon = transform.Find("actionIcon").GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();

        if(rules.isHeatEnabled()) updateTemp();
    }

    void updateTemp()
    {
        float EnvTemp = inShadow ? GameConstants.temperatureInShadow : GameConstants.temperatureInSun;
        if (currentTemp > EnvTemp)
        {
            currentTemp -= (currentTemp - EnvTemp) * temperatureTransferInSec * Time.deltaTime * 5;
        }
        else
        {
            currentTemp += (EnvTemp - currentTemp) * temperatureTransferInSec * Time.deltaTime;
        }
        if (currentTemp > maxBodyTemp)
        {
            this.die(Deaths.INSOLACAO);
        }
    }

    protected override void check_events()
    {
        inShadow = false;
        //Check for events that may change the calango behaviour in order of importance
        if (!CheckIfDead())
        {
            if (!CheckIfStarving())
            {
                if (!CheckIfRunningFromPredator())
                {
                    if (!CheckIfBeingHunted())
                    {
                        if (!CheckIfInsolated())
                        {
                            CheckIfHungry();
                        }
                    }
                }
            }
        }
    }

    bool CheckIfDead()
    {
        if (energy <= 0)
        {
            this.die(Deaths.INANICAO);
            return true;
        }
        return false;
    }

    bool CheckIfRunningFromPredator()
    {
        string[] lifeOrDeathStates = { GameConstants.states.RUNNINGFROMPREDATOR, GameConstants.states.HIDDING };
        if (lifeOrDeathStates.Contains(currState))
        {
            if (predatorList.Count == 0)
            {
                currState = GameConstants.states.IDDLE;
                return false;
            }
            return true;
        }
        else return false;
    }

    bool CheckIfBeingHunted()
    {
        if (check_for_predators())
        {
            findClosestHideoutInSight(focusedPredator);
            hideoutSearchCounter = 0;
            currState = "runningFromPredator";
            return true;
        }
        return false;
    }

    bool CheckIfStarving()
    {
        if (!starving && energy / maxEnergy < lowNutritionBoundery / 100)
        {
            starving = true;
            looser = false;
            mating = false; // if it is starving it stops running from other animals or mating and start looking for food
            currState = GameConstants.states.SEARCHINGFOOD;
            return true;
        }
        return false;
    }

    bool CheckIfInsolated()
    {
        if (currState == GameConstants.states.SEARCHINGSHADOW || currState == GameConstants.states.RUNNINGTOSHADOW || currState == GameConstants.states.COOLING) return true;
        else if (currentTemp > highBodyTempThreshold )
        {
            currState = GameConstants.states.SEARCHINGSHADOW;
            return true;
        }
        return false;
    }

    bool CheckIfHungry()
    {

        if (!hungry && energy / maxEnergy < maxNutritionBoundery / 100)
        {
            hungry = true;
            return true;
        }
        return false;
    }

    protected override void plan_action()
    {

        switch (currState)
        {

            case GameConstants.states.IDDLE:
                actionIcon.sprite = null;
                iddle();
                break;
            case GameConstants.states.TRYTOMATE:
                actionIcon.sprite = mateSprite;
                tryMating();
                break;
            case GameConstants.states.SEARCHINGFOOD:
                actionIcon.sprite = eatSprite;
                searchFood();
                break;
            case GameConstants.states.TRYTOEAT:
                actionIcon.sprite = eatSprite;
                tryEating();
                break;
            case GameConstants.states.SEARCHINGSHADOW:
                actionIcon.sprite = heatSprite;
                searchShadow();
                break;
            case GameConstants.states.RUNNINGTOSHADOW:
                actionIcon.sprite = heatSprite;
                runToShadow();
                break;
            case GameConstants.states.COOLING:
                actionIcon.sprite = heatSprite;
                coolDown();
                break;
            case GameConstants.states.RUNNINGFROMPREDATOR:
                actionIcon.sprite = losingSprite;
                runFromPredator();
                break;
            case GameConstants.states.HIDDING:
                actionIcon.sprite = losingSprite;
                hide();
                break;
        }


        energy -= Time.deltaTime * defaultBasalExpense;
    }

    protected void hide()
    {
        hideCounter += Time.deltaTime;
        if (hideCounter > hiddingTimeInSec)
        {
            currState = GameConstants.states.IDDLE;
            Hidden = false;
        }
    }

    protected void runFromPredator()
    {
        //if there is a hideout close run towards it
        if (focusedHideout != null)
        {
            walk_towards(focusedHideout.transform.position);
            if ((focusedHideout.transform.position - transform.position).sqrMagnitude < 0.03f)
            {
                currState = GameConstants.states.HIDDING;
                hideCounter = 0;
                Hidden = true;
            }
        }
        else
        {
            hideoutSearchCounter += Time.deltaTime;
            if (hideoutSearchCounter > hideoutSearchIntervalInSec)
            {
                findClosestHideoutInSight(focusedPredator);
                hideoutSearchCounter = 0;
            }
            walk_away(focusedPredator.transform.position);
            if ((focusedPredator.transform.position - transform.position).sqrMagnitude > lineOfSight)
            {
                currState = GameConstants.states.HIDDING;
            }
        }
    }

    protected void iddle()
    {

        if (age < reproductiveAge)
        { // If not in reproductive stage
            if (hungry)
                currState = GameConstants.states.SEARCHINGFOOD;
            else
                currState = GameConstants.states.IDDLE;
        }
        else
        {
            searchMate();
        }

        animator.SetBool("iddle", true);
    }

    private bool Is_Hungry()
    {
        hungry = (energy / maxEnergy < maxNutritionBoundery / 100);
        return hungry;
    }

    protected void searchFood()
    {
        findClosestEdible();
        if (noCloseFoodSource)
        {
            walk_randomly();
        }
        else
        {
            tryEating();
            currState = GameConstants.states.TRYTOEAT;
        }
    }

    protected void searchShadow()
    {
        findClosestShadowInSight();

        if (focusedShadowProducer == null)
        {
            walk_randomly();
        }
        else
        {
            currState = GameConstants.states.RUNNINGTOSHADOW;
        }
    }

    protected abstract void searchMate();

    protected abstract void tryMating();

    /*protected abstract void tryMating();
	protected abstract void findClosestMate();*/

    public bool isFertile()
    {
        return canReproduce;
    }

    protected void die(string cause)
    {
        registry.unregisterCalango(gameObject, cause);
        this.die();
    }

    protected override void die()
    {
        Destroy(gameObject);
    }

    protected override void dieOfOldAge()
    {
        die(Deaths.NATURAL);
    }

    public void getChallenged(CalangoBehaviour source)
    {
        competitor = source;
    }

    public void winChallenge(CalangoBehaviour looser)
    {
        if (competitor == looser)
            competitor = null;
    }

    public void atack(CalangoBehaviour competitor)
    {
        float damage = age / 10 * Time.deltaTime; // Preciso bolar um calculo para o ataque (dano por segundo)
        competitor.getAttacked(damage);
    }

    public void getAttacked(float damage)
    {
        this.energy -= damage;
    }

    protected void runFromCompetitor()
    {
        if (competitor != null)
        {
            Vector3 diff = competitor.transform.position - transform.position;
            if (diff.sqrMagnitude < 10f)
            { // still in range			
                walk_away(competitor.transform.position);
            }
            else
            {
                competitor = null;
                looser = false;
            }
        }
        else
        {  // In case the competitor died
            looser = false;
        }
    }

    public override void get_eaten()
    {
        registry.unregisterCalango(this.gameObject, Deaths.PREDADOR);
        base.get_eaten();
    }

    protected void tryEating()
    {

        if (closestEdible != null)
        {
            Vector3 diff = closestEdible.transform.position - transform.position;

            if (diff.sqrMagnitude > 0.03f)
            {
                walk_towards(closestEdible.transform.position);
            }
            else
            {
                if (closestEdible.tag == "plant")
                {
                    PlantModel plant = closestEdible.GetComponent<PlantModel>();
                    if (plant.edible)
                    {
                        eat_plant(plant);
                    }
                    else
                    {
                        eat_insect(plant);
                    }
                }
                else
                {

                }
                closestEdible = null;
                currState = GameConstants.states.IDDLE;
            }
        }
        else
        { // The plant was taken	
            findClosestEdible();
            currState = GameConstants.states.SEARCHINGFOOD;
        }
    }

    protected override void findClosestEdible()
    { // refatorar para procurar por animais também

        float distance = Mathf.Infinity;
        Vector3 position = transform.position;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, lineOfSight);

        foreach (Collider2D collider in colliders)
        {
            PlantModel plant;
            if (collider.gameObject.tag == "plant")
            {
                plant = collider.gameObject.GetComponent<PlantModel>();
                if (plant.edible || plant.hasAnyInsect())
                {
                    Vector3 diff = collider.gameObject.transform.position - position;
                    float curDistance = diff.sqrMagnitude;
                    if (curDistance < distance)
                    {
                        closestEdible = collider.gameObject;
                        distance = curDistance;
                    }
                }
            }
        }

        if (closestEdible == null)
        {
            noCloseFoodSource = true;
        }
        else
        {
            noCloseFoodSource = false;
        }
    }

    protected void runToShadow()
    {
        //if there is a shadow close run towards it
        if (focusedShadowProducer != null)
        {
            walk_towards(focusedShadowProducer.transform.position);
            if ((focusedShadowProducer.transform.position - transform.position).sqrMagnitude < .4f)
            {
                currState = GameConstants.states.COOLING;
            }
        }
    }

    protected void coolDown()
    {
        inShadow = true;
        if (currentTemp < lowBodyTempThreshold)
        {
            currState = GameConstants.states.IDDLE;
            inShadow = false;
        }
    }

    public float getBodyTemp()
    {
        return currentTemp;
    }
}
