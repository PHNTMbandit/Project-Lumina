using System;
using ProjectLumina.Capabilities;
using ProjectLumina.Effects;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectLumina.Data
{
    [Serializable, HideLabel]
    public class MeleeAttack : Attack
    {
        public override bool TryAttack(GameObject user, Damageable target)
        {
            if (target.IsDamageable)
            {
                if (UnityEngine.Random.Range(0, 100) <= criticalChance)
                {
                    float criticalDamage = damage * (criticalDamageMultiplier / 100) + damage;
                    target.Damage(criticalDamage);

                    if (target.TryGetComponent(out DamageIndicator damageIndicator))
                    {
                        damageIndicator.ShowDamageIndicator(true, criticalDamage, user.transform.position, criticalDamageIndicatorColour);
                    }

                    if (target.TryGetComponent(out CameraShake cameraShake))
                    {
                        cameraShake.Shake(criticalDamage);
                    }
                }
                else
                {
                    target.Damage(damage);

                    if (target.TryGetComponent(out DamageIndicator damageIndicator))
                    {
                        damageIndicator.ShowDamageIndicator(false, damage, user.transform.position, damageIndicatorColour);
                    }

                    if (target.TryGetComponent(out CameraShake cameraShake))
                    {
                        cameraShake.Shake(damage * 0.5f);
                    }
                }

                if (target.TryGetComponent(out HitFX hitFX))
                {
                    hitFX.ShowHitFX(base.hitFX.name);
                }

                if (target.TryGetComponent(out HitStop hitStop))
                {
                    hitStop.Stop(hitStopDuration);
                }

                return true;
            }

            return false;
        }
    }
}