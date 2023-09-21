using UnityEngine;

public class PixelObject:MonoBehaviour
{
    public Vector3 position
    {
        get{ return transform.position; }
        set{ transform.position = value; }
    }

    public float x
    {
        get{ return transform.position.x; }
        set
        {
            Vector3 pos = transform.position;
            pos.x = value;
            transform.position = pos;
        }
    }

    public float y
    {
        get{ return transform.position.y; }
        set
        {
            Vector3 pos = transform.position;
            pos.y = value;
            transform.position = pos;
        }
    }

    public float z
    {
        get{ return transform.position.z; }
        set
        {
            Vector3 pos = transform.position;
            pos.z = value;
            transform.position = pos;
        }
    }

    private bool _visible;
    public bool visible
    {
        get { return _visible; }
        set
        {
            _visible = value;
            gameObject.SetActive(_visible);
        }
    }

}
