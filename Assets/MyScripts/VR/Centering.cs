using UnityEngine;

public class Centering : MonoBehaviour
{
    [SerializeField] private Transform pivot;
    [SerializeField] Vector3 offset = new Vector3(0, -0.35f, 0);

    private Transform myTrans;
    private Vector3 vec;

    // Use this for initialization
    void Start()
    {
        myTrans = transform;
    }
	
	// Update is called once per frame
	void Update ()
    {
        vec.x = pivot.localPosition.x + offset.x;
        vec.y = pivot.localPosition.y + offset.y;
        vec.z = pivot.localPosition.z + offset.z;

        myTrans.localPosition = vec;
    }
}
