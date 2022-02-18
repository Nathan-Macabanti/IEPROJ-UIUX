using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteBlockade : MonoBehaviour
{
    [Header("Notes Dodge")]
    [SerializeField] protected float NotesDodged;
    [SerializeField] private float RequiredAmountToDodge = 10;
    [SerializeField] private PlayerCollision player;

    [Header("Timer")]
    [SerializeField] private float ticks;
    [SerializeField] private float ISATTACKNOTE_INTERVAL;

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
        if (ticks > 0 && NotesDodged >= RequiredAmountToDodge)
        {
            SliderIsTheTimer();
            ticks -= Time.deltaTime;
        }
        else if (ticks <= 0)
        {
            SliderIsNotesDodge();
            ticks = ISATTACKNOTE_INTERVAL;
            NotesDodged = 0;
            Debug.Log("Reset CollectedAttackNotes to 0");
            
        }
        else if(NotesDodged < RequiredAmountToDodge)
        {
            SliderIsNotesDodge();
        }
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
        bool isAttackNote = other.GetComponent<Note>().GetIsAttackNote;
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
        if(NotesDodged >= RequiredAmountToDodge)
        {
            return true;
        }
        return false;
    }

    public void BackToZero()
    {
        NotesDodged = 0;
    }

    public float GetRequiredAmountToDodge { get { return RequiredAmountToDodge / 2.0f; } }

    public bool AttackPhase
    {
        get { return attackPhase; }
        set { attackPhase = value; }
    }
}
