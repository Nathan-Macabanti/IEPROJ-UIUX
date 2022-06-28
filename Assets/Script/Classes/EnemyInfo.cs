using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class EnemyInfo
{
    public float Health;
    //public string ChartFile;
    public NoteSequencer noteSequencer;
    public GameObject EnemySprite;  
    public Animator EnemyAnimator;
    public bool LAttack;
    public bool MAttack;
    public bool RAttack;
    public bool JAttack;
    public Note DodgeNote;
    public Note AttackNote;
    public Note JumpNote;
}
