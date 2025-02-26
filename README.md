# Netick-Character-Controller
 A project showing how to use the Character Controller component with Netick


While I recomend against it, this code is mostly just workarounds to make the Character Controller component work in server-authoritative multiplayer games.

The code gets around the following issues:
1) CharacterController.IsGrounded only updates when physics is stepped, not on CharacterController.Move(). To get around this, I have written a custom IsGrounded() check (NetworkedCharacterController.cs)
2) The CharacterController has an internal collider, whose position is not updated when the transform is moved, until the next physics step. This means it does not play nice with client reconciliation. Luckily, we can force update the internal collider position by disabling and re-enabling the character controller (NetworkedCharacterController.cs, NetcodeIntoGameEngine())
