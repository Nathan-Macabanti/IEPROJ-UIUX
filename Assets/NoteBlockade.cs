using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteBlockade : MonoBehaviour
{
    [Header("Notes Dodge")]
    [SerializeField] protected float NotesDodged;
    [SerializeField] private float RequiredAmountToDodge = 10;

    [Header("Timer")]
    [SerializeField] private float ticks;
    [SerializeField] private float ISATTACKNOTE_INTERVAL;

    [Header("Slider")]
    [SerializeField] private Text notesDodgeStateText;
    [SerializeField] private Slider notesDodgeSlider;
    [SerializeField] private Image notesDodgeSliderFill;
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
        if (ticks > 0 && NotesDodged >= 10)
        {
            SliderIsTheTimer();
            ticks -= Time.deltaTime;
        }
        else if (ticks <= 0)
        {
            SliderIsNotesDodge();
            ticks = ISATTACKNOTE_INTERVAL;
            NotesDodged = 0;
        }
        else if(NotesDodged < 10)
        {
            SliderIsNotesDodge();
        }
    }

    public void SliderIsNotesDodge()
    {
        notesDodgeSlider.value = NotesDodged / RequiredAmountToDodge;
        notesDodgeSliderFill.color = ColorSliderFillNotesDodge;
    }

    public void SliderIsTheTimer()
    {
        notesDodgeSlider.value = ticks / ISATTACKNOTE_INTERVAL;
        notesDodgeSliderFill.color = ColorSliderFillTime;
        notesDodgeStateText.text = "ATTACK PHASE";
    }

    private void OnTriggerEnter(Collider other)
    {
        bool isAttackNote = other.GetComponent<Note>().GetIsAttackNote;
        if (other.gameObject.CompareTag("Note") && !isAttackNote)
        {
            Debug.Log("NotesTouchedBlockade");
            Destroy(other.gameObject);
            NotesDodged += 1;
        }
        else if(other.gameObject.CompareTag("Note") && isAttackNote)
        {
            Debug.Log("NotesTouchedBlockade");
            Destroy(other.gameObject);
        }
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
}
