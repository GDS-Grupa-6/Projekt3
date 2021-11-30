float3 _BoundsCenter, _BoundsExtents;

float BoxIntersection(float3 origin, float3 viewDir) {
    float3 ro = origin - _BoundsCenter;
    float3 invR   = 1.0.xxx / viewDir;
    float3 tbot   = invR * (-_BoundsExtents - ro);
    float3 ttop   = invR * (_BoundsExtents - ro);
    float3 tmin   = min (ttop, tbot);
    float2 tt0    = max (tmin.xx, tmin.yz);
    float  t0  = max(tt0.x, tt0.y);
    return max(t0, 0);
}

void SphereIntersection(float3 origin, float3 viewDir, out float t0, out float t1) {
    float3  oc = origin - _BoundsCenter;
    float   b = dot(viewDir, oc);
    float   c = dot(oc,oc) - _BoundsExtents.x;
    float   t = b*b - c;
    if (t>0) t = sqrt(t);
    t0 = -b-t;
    t1 = -b+t;
    if (t1>t0) {
        t0 = max(t0, 0);
    }
}