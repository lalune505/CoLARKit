using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;


public class SwipeSwitcher : MonoBehaviour, IEndDragHandler, IDragHandler {
    
    //текущий индекс для показываемого изображения
    public int CurrentIndex = 0;

   // public GameObject ButtonBack;

    //ссылка на геймобджект Image у которого и будут меняться спрайты
    //public GameObject Container;

    //ВАЖНО! ДЛИНА ОБОИХ СПИСКОВ ДОЛЖНА БЫТЬ ОДИНАКОВОЙ, И В НИХ НЕ ДОЛЖНО БЫТЬ ПУСТЫХ ЭЛЕМЕНТОВ
    //Спрайты для выставления на ContentImage
    public List<GameObject> Content;
    //Ссылка на геймобджекты которые показывают текущие страницы
    public List<UnityEngine.UI.Image> Pagers;

    ////Ссылка на спрайты выбранного и не выбранного пейджера
    public Sprite SelectedPager, DeselectedPager;


    void Start()
    {
        Show();
    }

    public void Show()
    {
        CurrentIndex = 0;

        Content[0].SetActive(true);

       foreach (var item in Pagers){
           item.sprite = DeselectedPager;
        }

        Pagers[0].sprite = SelectedPager;
    }

    

    public void ScrollNext()
    {
        if (CurrentIndex != Content.Count - 1)
        {
            CurrentIndex++;

            Content[CurrentIndex - 1].SetActive(false);
            Content[CurrentIndex].SetActive(true);

            
            Pagers[CurrentIndex].sprite = SelectedPager;
            if (CurrentIndex != 0){
                Pagers[CurrentIndex - 1].sprite = DeselectedPager;
            }

        }

       /* if (CurrentIndex == Content.Count - 1)
        {
            ButtonBack.SetActive(true);
        }*/
    }

    public void ScrollPrevious()
    {
        if (CurrentIndex != 0)
        {
            Pagers[CurrentIndex].sprite = DeselectedPager;

            CurrentIndex--;

            Content[CurrentIndex + 1].SetActive(false);
            Content[CurrentIndex].SetActive(true);
            Pagers[CurrentIndex].sprite = SelectedPager;
        }

        
          //  ButtonBack.SetActive(false);
        


    }

    //без ондрэг не работает онэнддрэг
    public void OnDrag(PointerEventData eventData)
    {
       
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("Press position + " + eventData.pressPosition);
        //.Log("End position + " + eventData.position);
        Vector3 dragVectorDirection = (eventData.position - eventData.pressPosition).normalized;
        //Debug.Log("norm + " + dragVectorDirection);
        GetDragDirection(dragVectorDirection);
    }

    private enum DraggedDirection
    {
        Up,
        Down,
        Right,
        Left
    }
    private void GetDragDirection(Vector3 dragVector)
    {
        float positiveX = Mathf.Abs(dragVector.x);
        float positiveY = Mathf.Abs(dragVector.y);
        DraggedDirection draggedDir;
        if (positiveX > positiveY)
        {
            draggedDir = (dragVector.x > 0) ? DraggedDirection.Right : DraggedDirection.Left;
        }
        else
        {
            draggedDir = (dragVector.y > 0) ? DraggedDirection.Up : DraggedDirection.Down;
        }
        Debug.Log(draggedDir);

        if (draggedDir == DraggedDirection.Left)
        {
            //print("BOOP LEFT");
            ScrollNext();
        }

        if (draggedDir == DraggedDirection.Right)
        {
            //print("BOOP RIGHT");
            ScrollPrevious();
        }
    }
}
