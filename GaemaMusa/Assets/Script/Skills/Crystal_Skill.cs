﻿using System.Collections.Generic;
using UnityEngine;

public class Crystal_Skill : Skill
{
    [SerializeField] private float crystalDuration;
    [SerializeField] private GameObject crystalPrefab;
    private GameObject currentCrystal;

    public bool crystalInsteadOfClone;

    [Header("Explosive crystal")]
    [SerializeField] private bool canExplode;

    [Header("Moving crystal")]
    [SerializeField] private bool canMoveToEnemy;
    [SerializeField] private float moveSpeed;

    [Header("Multi stacing crystal")]
    [SerializeField] private int amountOfStack;
    [SerializeField] private float multiStackCooldown;
    [SerializeField] private float useTimeWindow;
    [SerializeField] private bool canUseMultiStacks;
    [SerializeField] private List<GameObject> crystalList = new List<GameObject>();


    public override void UseSkill()
    {
        base.UseSkill();


        if (CanUseMultiCrystal())
            return;


        if (currentCrystal == null)
        {
            CreateCrystal();
        }

        else
        {
            if (canMoveToEnemy)
                return;


            Vector2 playerPos = player.transform.position;

            player.transform.position = currentCrystal.transform.position;
            currentCrystal.transform.position = playerPos;

            if (crystalInsteadOfClone)
            {
                SkillManager.instance.clone.CreateClone(currentCrystal.transform, Vector3.zero);
                Destroy(currentCrystal);
            }
            else
            {
                currentCrystal.GetComponent<Crystal_Skill_Controller>()?.FinishCrystal();

            }
        }

    }

    public void CreateCrystal()
    {
        currentCrystal = Instantiate(crystalPrefab, player.transform.position, Quaternion.identity);
        Crystal_Skill_Controller currentCystalScript = currentCrystal.GetComponent<Crystal_Skill_Controller>();

        currentCystalScript.SetupCrystal(crystalDuration, canExplode, canMoveToEnemy, moveSpeed, FindClosestEnemy(currentCrystal.transform));


    }

    public void CurrentCrystalChooseRandomTarget() => currentCrystal.GetComponent<Crystal_Skill_Controller>().ChooseRandomEnemy();

    private bool CanUseMultiCrystal()
    {
        if (canUseMultiStacks)
        {
            if (crystalList.Count > 0)
            {

                if(crystalList.Count == amountOfStack)
                {
                    Invoke("ResetAbility", useTimeWindow);
                }


                cooldown = 0;
                GameObject crystalSpawn = crystalList[crystalList.Count - 1];
                GameObject newCrystal = Instantiate(crystalSpawn, player.transform.position, Quaternion.identity);

                crystalList.Remove(crystalSpawn);
                newCrystal.GetComponent<Crystal_Skill_Controller>().SetupCrystal(crystalDuration, canExplode, canMoveToEnemy, moveSpeed, FindClosestEnemy(newCrystal.transform));

                if (crystalList.Count <= 0)
                {
                    //리필
                    cooldown = multiStackCooldown;
                    RefillCrystal();
                }
            }

            return true;
        }
        return false;
    }

    private void ResetAbility()
    {
        if (cooldownTimer > 0)
            return;

        cooldown = multiStackCooldown;
        RefillCrystal();
    }

    private void RefillCrystal()
    {
        int amountToAdd = amountOfStack - crystalList.Count;

        for (int i = 0; i < amountToAdd; i++)
        {
            crystalList.Add(crystalPrefab);
        }
    }

}