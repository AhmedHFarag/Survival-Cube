using UnityEngine;
using System.Collections;

public class TracerTargetDestroy : MonoBehaviour {

    private RocketLauncher RL;

    void OnDisable()
    {
        gameObject.name.Replace("TracerTarget(Clone)", "");
       
    }

}
