# Netick-Character-Controller
 A project showing how to use the Character Controller component with Netick


**WARNING: it is generally not recommended to use the CharacterController for server-authoritative multiplayer games.**

&nbsp;

While I recommend against using it, this code contains workarounds to make the Character Controller component work in server-authoritative multiplayer games.

&nbsp;

The code works around the following issues:
1) ```CharacterController.IsGrounded``` only updates when physics is stepped, not on ```CharacterController.Move()```. This means it does not play nice with Client Side Prediction. To get around this, I have written a custom ```IsGrounded()``` method (NetworkedCharacterController.cs)
2) The CharacterController has an internal collider, whose position is not updated when the transform is moved, until the next physics step. This means it does not play nice with client reconciliation. Luckily, we can force update the internal collider position by disabling and re-enabling the character controller (NetworkedCharacterController.cs, ```NetcodeIntoGameEngine()```)

&nbsp;

This project contains a few key scripts:
- NetworkedCharacterController.cs: base class for networked character controllers, containing the code needed to work around bugs with CharacterController
- SimpleFirstPersonController.cs: a simple character controller with acceleration, jumping, and gravity
- FirstPersonController.cs: a slightly more developed character controller with step-down handling, and separate deceleration values


![image](https://github.com/user-attachments/assets/0edc3cc9-07bf-4650-85da-a7053aea4947)
