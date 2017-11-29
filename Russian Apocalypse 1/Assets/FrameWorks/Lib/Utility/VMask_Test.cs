using UnityEngine;
using System.Collections.Generic;

public struct VMask
{
    int maskValue;

    public void AddMask(int i)
    {
        maskValue |= i;
    }

    public void AddMaskPos(int pos)
    {
        maskValue |= (1 << pos);
    }

    /// <summary>
    /// Check if the crossponding position has value
    /// </summary>
    /// <param name="i">Value to check</param>
    public bool HasValue(int i)
    {
        return (maskValue & i) == 0 ? false : true;
    }

    public bool HasValuePos(int pos)
    {
        return (maskValue & (1 << pos)) == 0 ? false : true;
    }

    public void RemoveMask(int i)
    {
        maskValue &= ~i;   //~i equal to i ^ 0xfffffff
    }

    public void RemoveMaskPos(int pos)
    {
        maskValue &= ~(1 << pos);
    }

    public bool Composition_Empty(int value)
    {
        int t = value ^ maskValue;
        t &= value;
        return t == value;
    }

    public int Composition_GetFirst_Empty_Pos(int[] value)
    {
        for (int i = 0; i < value.Length; i++)
        {
            if ((maskValue & value[i]) == 0) return i;
        }
        return -1;
    }

    public static bool HasValueOnPos(int value,int pos)
    {
        return (value & (1 << pos)) == 0 ? false : true;
    }

}

public class VMask_Test : MonoBehaviour
{
    private void Start()
    {
        VMask mask = new VMask();

        bool blocked = mask.HasValue(1 << 2);
        Debug.LogFormat("The Bit Pos {0} get blocked ? : {1}", 2, blocked);
        blocked = mask.HasValue(1 << 5);
        Debug.LogFormat("The Bit Pos {0} get blocked ? : {1}", 5, blocked);

        mask.AddMask(1 << 5);
        mask.AddMask(1 << 2);
        blocked = mask.HasValue(1 << 2);
        Debug.LogFormat("The Bit Pos {0} get blocked ? : {1}", 2 , blocked);
        blocked = mask.HasValue(1 << 5);
        Debug.LogFormat("The Bit Pos {0} get blocked ? : {1}", 5, blocked);


        mask.RemoveMask(1 << 2);
        mask.RemoveMask(1 << 5);
        blocked = mask.HasValue(1 << 2);
        Debug.LogFormat("The Bit Pos {0} get blocked ? : {1}", 2, blocked);
        blocked = mask.HasValue(1 << 5);
        Debug.LogFormat("The Bit Pos {0} get blocked ? : {1}", 5, blocked);

        mask.AddMaskPos(2);
        mask.AddMaskPos(5);
        blocked = mask.HasValuePos(2);
        Debug.LogFormat("The Bit Pos {0} get blocked ? : {1}", 2, blocked);
        blocked = mask.HasValuePos(5);
        Debug.LogFormat("The Bit Pos {0} get blocked ? : {1}", 5, blocked);

        mask.RemoveMaskPos(2);
        mask.RemoveMaskPos(5);
        blocked = mask.HasValuePos(2);
        Debug.LogFormat("The Bit Pos {0} get blocked ? : {1}", 2, blocked);
        blocked = mask.HasValuePos(5);
        Debug.LogFormat("The Bit Pos {0} get blocked ? : {1}", 5, blocked);
    }

    void Bist_Test()
    {
        int a = 60;            /* 60 = 0011 1100 */
        int b = 13;            /* 13 = 0000 1101 */
        int c = 0;

        c = a & b;             /* 12 = 0000 1100 */
        Debug.LogFormat("Line 1 - Value of c is {0}", c);

        c = a | b;             /* 61 = 0011 1101 */
        Debug.LogFormat("Line 2 - Value of c is {0}", c);

        c = a ^ b;             /* 49 = 0011 0001 */
        Debug.LogFormat("Line 3 - Value of c is {0}", c);

        c = ~a;                /*-61 = 1100 0011 */           // ~a equal to a ^ 0xfffffff
        Debug.LogFormat("Line 4 - Value of c is {0}", c);

        c = a << 2;      /* 240 = 1111 0000 */
        Debug.LogFormat("Line 5 - Value of c is {0}", c);

        c = a >> 2;      /* 15 = 0000 1111 */
        Debug.LogFormat("Line 6 - Value of c is {0}", c);
    }

}
