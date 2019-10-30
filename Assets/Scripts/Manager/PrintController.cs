using System;
using UnityEngine;

public class PrintController : MonoBehaviour
{
    public static Action OnShapeGuessed;
    public static Action OnShapeWrong;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ShapeMatch shape = collision.GetComponent<ShapeMatch>();

        if (shape != null)
        {
            if (shape.CheckShape(ShapeController.GetCurrentShape()))
            {
                OnShapeGuessed?.Invoke();
                anim.SetTrigger("Print");
                return;
            }

            OnShapeWrong?.Invoke();            
        }
    }
}
