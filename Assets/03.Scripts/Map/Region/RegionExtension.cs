using System;
using System.Collections.Generic;
using UnityEngine;

public struct REGION
{
    private int value;

    public REGION(int value)
    {
        this.value = value;
    }
    
    // implicit 은 int i = REGION_TYPE이 가능하고
    // explcit 은 int i = (REGION)REGION_TYPE이 가능하다.
    
    public static implicit operator int(REGION r) => r.value;
    
    public static implicit operator REGION(int value) => new REGION(value);
    
    // 1st floor
    public static readonly REGION Holl = new REGION(1);
    public static readonly REGION ManRestroom = new REGION(2);
    public static readonly REGION WomanRestroom = new REGION(3);
    public static readonly REGION DayRoom = new REGION(4);
    
}

public static class RegionExtension
{
    private static List<Tuple<Region_Collider, REGION>> regions = new List<Tuple<Region_Collider, REGION>>();

    public static void Init()
    {
        regions.Clear();
    }

    public static void AddRegion(Region_Collider region, REGION regionInfo)
    {
        regions.Add(new Tuple<Region_Collider, REGION>(region, regionInfo));
    }

    public static void RemoveRegion(Region_Collider region, REGION regionInfo)
    {
        regions.Remove(new Tuple<Region_Collider, REGION>(region, regionInfo));
    }

    public static List<Tuple<Region_Collider, REGION>> GetAllRegion()
    {
        return regions;
    }
}