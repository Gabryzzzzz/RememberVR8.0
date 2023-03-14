using UnityEngine;

public class AssetsHelpers : MonoBehaviour
{

    private static AssetsHelpers _i;

    public static AssetsHelpers i
    {
        get
        {
            if (_i == null)
            {
                _i = FindObjectOfType<AssetsHelpers>();
            }
            return _i;
        }
    }

    public Transform pftextHelper;

}
