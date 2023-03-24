using UnityEngine;

public class UtilsGabryzzzzz : MonoBehaviour
{

    public static void good_look_at(Transform obj, GameObject look_at, float speed = 2f)
    {
        var lookPos = look_at.transform.position - new Vector3(obj.transform.position.x + 0.2f, obj.transform.position.y * 2, obj.transform.position.z);
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        obj.transform.rotation = Quaternion.Slerp(obj.transform.rotation, rotation, Time.deltaTime * speed);
    }

    public static bool two_object_near(Transform obj1, Transform obj2, float distance)
    {
        if (Vector3.Distance(obj1.transform.position, obj2.transform.position) <= distance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}


