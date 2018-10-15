using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixelV2
{
    [RequireComponent(typeof(CircleCollider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class PXR2V2_Follow : PXReact2Vec2
    {
        public float _LerpSpd = 1.0f;
        public float _Multilier = 1.0f;
        public AnimationCurve _MultiplierOnDist01;

        protected override void React2Vec2s(List<PXVec2> vec2s, float dt)
        {
            CircleCollider2D ccld = GetComponent<CircleCollider2D>();

            float cldRadius = ccld.radius;
            float scl =
                0.5f * (transform.lossyScale.x + transform.lossyScale.y);
            float radius = scl * cldRadius;
            Vector2 vel = Vector2.zero;
            foreach(var pv2 in vec2s){
                Vector2 offset = (Vector2)(
                    pv2.transform.position - transform.position);

                float dist = offset.magnitude;
                float dist01 = dist / radius;
                float multiplier = _MultiplierOnDist01.Evaluate(dist01);
                Vector2 v2 = pv2.GetVec2D();
                vel += v2 * multiplier;
            }
            vel *= _Multilier;

            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            Vector2 velNow = rb.velocity;
            rb.velocity = Vector2.Lerp(velNow, vel, _LerpSpd * dt);
        }




    }
}

