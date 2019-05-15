# Quick-Hull

- [2D](2D.cs)
````c#
    void Convex_flow(Vector3 a , Vector3 b , List<Vector3> Region)
    {
        List<Vector3> up_region = Up_region(a, b, Region);
        
        if(up_region.Count > 0)
        {
            Vector3 far = Far(a, b, up_region);
            Convex_flow( a, far, up_region);
            Convex_flow(far, b , up_region);
        }
        else
        {
            Hull.Add(b);
        }
    }
````
