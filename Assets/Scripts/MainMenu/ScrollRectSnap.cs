using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScrollRectSnap : MonoBehaviour
{

    float[] points;
    [Tooltip("how many screens or pages are there within the content (steps)")]
    public int screens = 1;
    public int CurrentScreen = 0;
    float stepSize;

    ScrollRect scroll;
    bool LerpH;
    float targetH;
    [Tooltip("Snap horizontally")]
    public bool snapInH = true;
    public Button Right;
    public Button Left;
    bool LerpV;
    float targetV;
    [Tooltip("Snap vertically")]
    public bool snapInV = true;
    private Vector2 startPos;
    private Vector2 endPos;
    float minSwipeDist = 100;
    void Start()
    {
        scroll = gameObject.GetComponent<ScrollRect>();
        scroll.inertia = false;

        if (screens > 0)
        {
            points = new float[screens];

            stepSize = 1.0f / (float)(screens - 1);

            for (int i = 0; i < screens; i++)
            {
                points[i] = i * stepSize;
            }
        }
        else
        {
            points[0] = 0;
        }
        if (snapInV)
        {
            CurrentScreen = points.Length - 1;
            targetV = points[CurrentScreen];
            LerpV = true;
        }
        if (snapInH)
        {
            CurrentScreen = 0;
            targetH = points[CurrentScreen];
            LerpH = true;
            if (Right)
            Right.gameObject.SetActive(true);
            if(Left)
            Left.gameObject.SetActive(false);
            if (Right)
            {
                Right.onClick.AddListener(delegate 
                {
                    if (CurrentScreen < points.Length - 1)
                    {
                        CurrentScreen++;
                        targetH = points[CurrentScreen];
                        LerpH = true;
                        if (CurrentScreen == points.Length - 1)
                        {
                            Right.gameObject.SetActive(false);
                            Left.gameObject.SetActive(true);
                        }
                        else
                        {
                            Right.gameObject.SetActive(true);
                            Left.gameObject.SetActive(true);
                        }
                    }
                });
            }
            if (Left)
            {
                Left.onClick.AddListener(delegate
                {
                    if (CurrentScreen > 0)
                    {
                        CurrentScreen--;
                        targetH = points[CurrentScreen];
                        LerpH = true;
                        if (CurrentScreen == 0)
                        {
                            Right.gameObject.SetActive(true);
                            Left.gameObject.SetActive(false);
                        }
                        else
                        {
                            Right.gameObject.SetActive(true);
                            Left.gameObject.SetActive(true);
                        }
                    }
                });
            }
        }
        
    }

    void Update()
    {
        if (LerpH)
        {
            scroll.horizontalNormalizedPosition = Mathf.Lerp(scroll.horizontalNormalizedPosition, targetH, 100 * scroll.elasticity * Time.deltaTime);
            if (Mathf.Approximately(scroll.horizontalNormalizedPosition, targetH)) LerpH = false;
        }
        if (LerpV)
        {
            scroll.verticalNormalizedPosition = Mathf.Lerp(scroll.verticalNormalizedPosition, targetV, 100 * scroll.elasticity * Time.deltaTime);
            if (Mathf.Approximately(scroll.verticalNormalizedPosition, targetV)) LerpV = false;
        }
    }

    public void DragEnd()
    {
        endPos = Input.mousePosition;
        if (scroll.horizontal && snapInH && Mathf.Abs(endPos.x - startPos.x) >minSwipeDist)
        {
            //targetH = points[FindNearest(scroll.horizontalNormalizedPosition, points)];
            float swipeValue = Mathf.Sign(endPos.x - startPos.x);
            //Debug.Log("x:" + swipeValue +"Start:"+ startPos + "End:"+ endPos);
            if (swipeValue > 0)//Left swipe
            {
                if (CurrentScreen > 0)
                {
                    CurrentScreen--;
                }
            }
            else if (swipeValue < 0)//Right swipe
            {
                if (CurrentScreen < points.Length - 1)
                {
                    CurrentScreen++;
                }
            }
            targetH = points[CurrentScreen];
        }
        if (scroll.vertical && snapInV && Mathf.Abs(endPos.y - startPos.y) > minSwipeDist)
        {
            //targetV = points[FindNearest(scroll.verticalNormalizedPosition, points)];
            // Get Direction
            float swipeValue = Mathf.Sign(endPos.y - startPos.y);
            //Debug.Log("y:"+swipeValue + "Start:" + startPos + "End:" + endPos);
            if (swipeValue > 0)//up swipe
            {
                if (CurrentScreen>0)
                {
                    CurrentScreen--;
                }
            }
            else if (swipeValue < 0)//down swipe
            {
                if (CurrentScreen < points.Length-1)
                {
                    CurrentScreen++;
                }
            }
            targetV = points[CurrentScreen];

        }
        if (snapInH)
        {
            LerpH = true;
            if (CurrentScreen == 0)
            {
                Right.gameObject.SetActive(true);
                Left.gameObject.SetActive(false);
            }
            else if(CurrentScreen == points.Length - 1)
            {
                Right.gameObject.SetActive(false);
                Left.gameObject.SetActive(true);
            }
            else
            {
                Right.gameObject.SetActive(true);
                Left.gameObject.SetActive(true);
            }
        }
        if(snapInV)
        LerpV = true;
    }

    public void BeginDrag()
    {
        startPos = Input.mousePosition;
        LerpH = false;
        LerpV = false;
    }

    int FindNearest(float f, float[] array)
    {
        float distance = Mathf.Infinity;
        int output = 0;
        for (int index = 0; index < array.Length; index++)
        {
            if (Mathf.Abs(array[index] - f) < distance)
            {
                distance = Mathf.Abs(array[index] - f);
                output = index;
            }
        }
        return output;
    }
    public void GoToScreen(int index)
    {
        CurrentScreen = index;
        targetV = points[index];
        LerpV = true;
    }
}