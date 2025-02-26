using UnityEngine;
using Netick;
using Netick.Unity;

public class NetworkedCharacterController : NetworkBehaviour
{
    protected CharacterController _CC;
    protected LayerMask _collisionLayerMask;

    public void InitializeComponent()
    {
        _CC = GetComponent<CharacterController>();
        SetCollisionLayerMask();
    }

    protected void SetCollisionLayerMask()
    {
        _collisionLayerMask = 0;
        for (int i = 0; i < 32; i++)
        {
            if (!Physics.GetIgnoreLayerCollision(_CC.gameObject.layer, i))
                _collisionLayerMask |= 1 << i;
        }
    }

    public override void NetcodeIntoGameEngine()
    {
        if (!IsPredicted)
            return;
        //disable and re-enable the character controller to force its internal collider to update position.
        _CC.enabled = false;
        _CC.enabled = true;
    }

    protected Vector3 GetCharacterCapsuleBase()
    {
        return transform.position + transform.TransformVector(_CC.center) - (Vector3.up * _CC.height / 2);
    }

    protected RaycastHit GroundHitCheck;
    protected bool CheckGroundHit(float distance)
    {
        Vector3 CapsuleBase = GetCharacterCapsuleBase();
        return Sandbox.Physics.SphereCast(CapsuleBase + Vector3.up * _CC.radius,
            _CC.radius,
            Vector3.down,
            out GroundHitCheck,
            maxDistance: _CC.skinWidth + distance,
            layerMask: _collisionLayerMask);
    }

    //CharacterController.IsGrounded doesnt update during resims (only when physics is stepped), and therefore doesnt work with CSP.
    //We use this function to check grounding status instead.
    protected bool IsGrounded()
    {
        return CheckGroundHit(.001f);
    }
}
