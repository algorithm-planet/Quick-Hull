public class Quick_hull
{
    public List<Vector3> Hull;

    public Quick_hull(List<Vector3> p_)
    {
        List<Vector3> region_1 = p_;

        Vector3 A = Vector3.zero;
        Vector3 B = Vector3.zero;

        float max = 0f;
        for (int y = 0; y <= p_.Count - 2; y += 1)
        {
            for (int x = y + 1; x <= p_.Count - 1; x += 1)
            {
                float d = Vector3.SqrMagnitude(p_[y] - p_[x]);
                if (d > max)
                {
                    A = p_[y];
                    B = p_[x];
                    max = d;
                }
            }
        }

        Hull = new List<Vector3>();
        Hull.Add(A);

        Convex_flow(A, B, region_1);
    }

    List<Vector3> Up_region(Vector3 a , Vector3 b , List<Vector3> current_region)
    {
        List<Vector3> new_region = new List<Vector3>();
        foreach(Vector3 p in current_region)
        {
            Vector3 n1 = b - a;
            Vector3 n2 = p - a;

            float orient = Vector3.Dot(Vector3.up, -Vector3.Cross(n1, n2));
            if(orient > 0f)
            {
                new_region.Add(p);
            }
        }
        return new_region;
    }

    Vector3 Far(Vector3 a, Vector3 b , List<Vector3> up_region)
    {
        Vector3 far = Vector3.zero;
        float max = 0f;

        Vector3 n = b - a;

        foreach(Vector3 p in up_region)
        {
            float num = Vector3.Dot(p - a , n);
            float deno = Vector3.Dot(n, n);

            float L = num / deno;

            Vector3 foot = a + n * L;
            float d = Vector3.SqrMagnitude(p - foot);
            if(d > max)
            {
                max = d;
                far = p;
            }
        }
        return far;
    }

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
}
