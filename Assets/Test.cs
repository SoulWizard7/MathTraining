using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public int sign;
    public int sign1;
    public int sign0;
    
    
    
    private void OnDrawGizmos()
    {
        sign = Math.Sign(3.42);
        sign1 = Math.Sign(-3.42);
        sign0 = Math.Sign(0);
        
        
    }
}
