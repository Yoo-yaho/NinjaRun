using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_PlatformCheck : MonoBehaviour
{
    public bool _IsCheck = false;
    // Start is called before the first frame update
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Platform")
        {
            _IsCheck = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Platform")
        {
            _IsCheck = false;
        }
    }
}
