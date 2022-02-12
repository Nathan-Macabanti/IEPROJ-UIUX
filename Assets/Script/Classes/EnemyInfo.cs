using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class EnemyInfo
{
    public float Health;
    public string ChartFile;
    public GameObject EnemySprite;  
    public Animator EnemyAnimator;
    public Note DodgeNote;
    public Note AttackNote;
    public Note JumpNote;
}
