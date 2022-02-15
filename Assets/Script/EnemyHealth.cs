using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour, IDamagable<float>, ISpawner
{
    [SerializeField] protected Slider HealthBar;
    [SerializeField] private Text HealthCounter;
    [SerializeField] private bool UseSuggested;
    [SerializeField] private float fHP;
    [SerializeField] private float fsuggestedHP;
    [SerializeField] private float fHPClone;
    [SerializeField] private SongManager2 sMan;
    [SerializeField] private NoteBlockade ntBlkde;
    [SerializeField] private bool isDead;
    [SerializeField] private Animator EnemyAnimator;
    [SerializeField] private GameObject damageParticles;

    [Header("Existing Animations")]
    [SerializeField] private bool LAttack;
    [SerializeField] private bool MAttack;
    [SerializeField] private bool RAttack;
    [SerializeField] private bool JAttack;

    // Start is called before the first frame update
    void Start()
    {
        float NumberOfNotes = sMan.Notes.Count;
        float PercentileOfTime = ntBlkde.GetRequiredAmountToDodge / sMan.AudioSource.clip.length;
        fsuggestedHP = NumberOfNotes - (NumberOfNotes * PercentileOfTime);
        if (UseSuggested)
        {
            fHPClone = fsuggestedHP;
            fHP = fsuggestedHP;
        }
        else
            fHPClone = fHP;

        //Debug.Log(fHP);
    }

    // Update is called once per frame
    void Update()
    {
        HealthBar.value = fHP / fHPClone;
        HealthCounter.text = "HP " + fHP.ToString() + "/" + fHPClone.ToString();
    }

    public float GetfHP { get { return fHP; } }

    public void UpdateHealth(float HP)
    {
        fHP = HP;
        fHPClone = fHP;
    }

    public bool GetIsDead { 
        get {
            if (fHP <= 0) return true;
            return false;
        } 
    }
    public void Damage(float dmgValue)
    {
        fHP -= dmgValue;
        if(damageParticles != null)
        {
            GameObject dmgParticles = (GameObject)Instantiate(damageParticles, this.transform.position, this.transform.rotation);
            Destroy(dmgParticles, 2f);
        }
        
        EnemyAnimator.Play("Damage");
    }

    public void Spawn(Spawner spawner, bool IsAttack, int spawnerIndex)
    {
        EnemyAnimator.StopPlayback();


        if (IsAttack)
        {
            spawner.SpawnAttackNote();
        }
        else
        {
            spawner.SpawnDodgeNote();
        }


        if(spawnerIndex == 0 && LAttack) //Left Spawn Animation
        {
            //EnemyAnimator.StopPlayback();
            EnemyAnimator.Play("LAttack");
            return;
        }
        if (spawnerIndex == 1 && MAttack) //Mid Spawn Animation
        {
            //EnemyAnimator.StopPlayback();
            EnemyAnimator.Play("MAttack");
            return;
        }
        if (spawnerIndex == 2 && RAttack) //Right Spawn Animation
        {
            //EnemyAnimator.StopPlayback();
            EnemyAnimator.Play("RAttack");
            return;
        }
        if(spawnerIndex == 3 && JAttack)
        {
            //EnemyAnimator.StopPlayback();
            EnemyAnimator.Play("JAttack");
            return;
        }
        EnemyAnimator.Play("Attack");
    }

    public Animator enemyAnimator
    {
        get { return EnemyAnimator; }
        set { EnemyAnimator = value; }
    }

    public bool LeftAtk { set { LAttack = value; } }
    public bool MidAtk { set { MAttack = value; } }
    public bool RightAtk { set { RAttack = value; } }
    public bool JumpAtk { set { JAttack = value; } }
}
