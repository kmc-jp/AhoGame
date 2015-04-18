using UnityEngine;
using System.Collections;

public class StageController : MonoBehaviour
{
    public GameObject Target;
    public int BeginX;
    public int EndX;
    Animator animator;


    public void Awake()
    {
        animator = this.GetComponent<Animator>();
    }

    public void Start()
    {
        
    }

    public void Update()
    {
    }

    public void Enter()
    {
        animator.SetBool("Enter", true);
        animator.SetBool("Entered", true);
    }

    public void Exit()
    {
        animator.SetBool("Exit", true);
    }

    public Rect GetImageRect()
    {
        
        return new Rect();
    }

    public enum CutType
    {
        Vertical, Horizontal
    }
}