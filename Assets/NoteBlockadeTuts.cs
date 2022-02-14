using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteBlockadeTuts : MonoBehaviour
{
    [Header("Notes Dodge")]
    [SerializeField] protected float NotesDodged;
    [SerializeField] private float RequiredAmountToDodge = 10;
    [SerializeField] private PlayerController player;

    [Header("Timer")]
    [SerializeField] private float ticks;
    [SerializeField] private float ISATTACKNOTE_INTERVAL;
    [SerializeField] private bool AlwaysAttacking = false;
    [SerializeField] private bool AlwaysVulnerable = false;

    [Header("Slider")]
    [SerializeField] private Text notesDodgeStateText;
    [SerializeField] private Slider notesDodgeSlider;
    [SerializeField] private Image notesDodgeSliderFill;
    [SerializeField] private bool attackPhase;
    [SerializeField] private Color ColorTextNotesDodge;
    [SerializeField] private Color ColorTextTime;
    [SerializeField] private Color ColorSliderFillNotesDodge;
    [SerializeField] private Color ColorSliderFillTime;

    private void Start()
    {
        NotesDodged = 0;
        ticks = 0;
        notesDodgeStateText.text = " ";
    }

    private void Update()
    {
        //When it is full it will turn the timer blue and go down
        //Sets the enemy to VULNERABLE
        if (ticks > 0 && NotesDodged >= RequiredAmountToDodge)
        {
            SliderIsTheTimer();
            ticks -= Time.deltaTime;
        }
        //Sets the enemy to ATTACKING
        else if (ticks <= 0)
        {
            // Resets the collected notes to 0
            SliderIsNotesDodge();
            //Resets 
            ticks = ISATTACKNOTE_INTERVAL;
            NotesDodged = 0;
            Debug.Log("Reset Collected AttackNotes to 0");

        }
        else if (NotesDodged < RequiredAmountToDodge)
        {
            SliderIsNotesDodge();
        }

        if (AlwaysAttacking)
        {
            ticks = 0;
        }
        else if(AlwaysVulnerable)
        {
            ticks = ISATTACKNOTE_INTERVAL;
        }
        Debug.Log(notesDodgeSlider.value);
    }

    public void SliderIsNotesDodge()
    {
        player.CollectedAttackNotes = 0;
        notesDodgeSlider.value = NotesDodged / RequiredAmountToDodge;
        notesDodgeSliderFill.color = ColorSliderFillNotesDodge;
        notesDodgeStateText.color = ColorTextNotesDodge;
        notesDodgeStateText.text = "ATTACKING";
        attackPhase = false;
    }

    public void SliderIsTheTimer()
    {
        notesDodgeSlider.value = ticks / ISATTACKNOTE_INTERVAL;
        notesDodgeSliderFill.color = ColorSliderFillTime;
        notesDodgeStateText.color = ColorTextTime;
        notesDodgeStateText.text = "VULNERABLE";
        attackPhase = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        bool isAttackNote = other.GetComponent<TutorialNote>().GetIsAttackNote;
        if (other.gameObject.CompareTag("Note") && !isAttackNote)
        {
            NotesDodged++;
        }
        Debug.Log("NotesTouchedBlockade");
        Destroy(other.gameObject);
    }

    //public int GetNotesDodged { get { return NotesDodged; } }
    public bool GetIsAttackNote()
    {
        if (NotesDodged >= RequiredAmountToDodge)
        {
            return true;
        }
        return false;
    }

    public float GetRequiredAmountToDodge { get { return RequiredAmountToDodge / 2.0f; } }

    public bool AttackPhase
    {
        get { return attackPhase; }
        set { attackPhase = value; }
    }

    public void AlwaysVulnerableSwitch(bool b)
    {
        AlwaysVulnerable = b;
        AlwaysAttacking = !b;
    }
    public void AlwaysAttacksSwitch(bool b)
    {
        AlwaysAttacking = b;
        AlwaysVulnerable = !b;
    }
    public void TurnOffAllAlways()
    {
        AlwaysAttacking = false;
        AlwaysVulnerable = false;
    }
}
