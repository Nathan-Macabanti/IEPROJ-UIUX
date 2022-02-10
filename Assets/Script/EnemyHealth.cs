using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyHealth : MonoBehaviour
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
    [SerializeField] private Animator deathAnim;

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
    public void DamageEnemy(float dmgValue)
    {
        fHP -= dmgValue;
        deathAnim.Play("BossDamageAnimation");
    }
}
