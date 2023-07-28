// GENERATED AUTOMATICALLY FROM 'Assets/InputSettings/InputMaster.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputMaster : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputMaster()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputMaster"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""bcbaba8d-f973-4e5a-b53b-1510d43ba0a9"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Button"",
                    ""id"": ""e2a62148-a0fc-4a8c-90f9-6e0be926f2bf"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""CombatMove"",
                    ""type"": ""Button"",
                    ""id"": ""a9d3e3f2-dc3a-46e5-b02b-0fe12d64d071"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Affirm"",
                    ""type"": ""Button"",
                    ""id"": ""634ab75b-2a79-45f8-a822-ac9a9ae509df"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Cancel"",
                    ""type"": ""Button"",
                    ""id"": ""87e1f833-dcba-4003-8f30-63a08e3b4936"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""OpenRadialMenu"",
                    ""type"": ""Button"",
                    ""id"": ""ee48565c-71a3-43a7-826d-2a4844db413e"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""09b30490-11eb-4f30-b9e0-4f6bce577081"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2135ddf2-a608-4c7b-859d-36e4b17f07b2"",
                    ""path"": ""<HID::Bensussen Deutsch & Associates,Inc.(BDA) Core (Plus) Wired Controller>/stick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""04f22425-23c4-463b-9905-6112eaf1fec8"",
                    ""path"": ""<HID::2Axes 11Keys Game  Pad>/stick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9644865e-222a-4790-a7ac-ddd99cd79b45"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bb395de2-d0f7-4066-a9e8-b61bd002b0f2"",
                    ""path"": ""<HID::Bensussen Deutsch & Associates,Inc.(BDA) Core (Plus) Wired Controller>/button3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b321658b-e322-4bbc-bf03-223571ceabe0"",
                    ""path"": ""<HID::2Axes 11Keys Game  Pad>/button2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e086216b-6610-456d-a187-b1145bb7740d"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""OpenRadialMenu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6a803e76-3b4d-4b12-b042-8090c36ea41e"",
                    ""path"": ""<HID::Bensussen Deutsch & Associates,Inc.(BDA) Core (Plus) Wired Controller>/button7"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""OpenRadialMenu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8072bab8-9370-4434-9784-5006ef6588da"",
                    ""path"": ""<HID::2Axes 11Keys Game  Pad>/button5"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""OpenRadialMenu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""541e85a7-5597-4a03-ba5a-812f40bf8e8b"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""CombatMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e811a239-7f63-486a-af8e-3cd1a0ed3169"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Affirm"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""05edb366-aef5-4c7a-bdb7-eec251f8e7c1"",
                    ""path"": ""<HID::Bensussen Deutsch & Associates,Inc.(BDA) Core (Plus) Wired Controller>/button2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Affirm"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""04ddf75e-3409-4db0-9f70-c196e1368ad1"",
                    ""path"": ""<HID::2Axes 11Keys Game  Pad>/button3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Affirm"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""68428efb-d15c-4365-8bc2-2b13f191bfb2"",
                    ""path"": ""<HID::SZMY-POWER CO.,LTD. BDA GP1>/stick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Joystick"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""4ff7795e-a742-4e50-b0b9-e6d55596c1ba"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""44d24522-8d65-4ff1-9920-edb1afe839d8"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""08d3bea8-93e1-4915-9222-a401086c7ec1"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""11f497e2-4b48-4def-8a1d-395618a06513"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""968a54f6-60d8-42e7-b0e3-5a99ec42b441"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""7db078e6-8529-4f96-b33a-b41c6a1dce2d"",
                    ""path"": ""<Joystick>/stick"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Joystick"",
                    ""action"": ""CombatMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c7213313-a819-4db9-98dd-2b41c3711cf1"",
                    ""path"": ""<HID::SZMY-POWER CO.,LTD. BDA GP1>/button2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Joystick"",
                    ""action"": ""Affirm"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d2ed53ee-d7c9-42a2-9e4e-5a6bff74b013"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Affirm"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9f9a847f-f4b3-47d1-b6cb-e21fc1d4efc8"",
                    ""path"": ""<HID::SZMY-POWER CO.,LTD. BDA GP1>/button3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Joystick"",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""be07ea67-5136-4250-9093-73124ae036c5"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5104fb12-7c1a-430c-b95b-85bada26e628"",
                    ""path"": ""<HID::SZMY-POWER CO.,LTD. BDA GP1>/button7"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Joystick"",
                    ""action"": ""OpenRadialMenu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e9ae9683-5f8f-4b05-8775-19589deffa36"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""OpenRadialMenu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""6ec7de8c-abc6-4a89-a813-0d2d38dcb74f"",
                    ""path"": ""2DVector"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CombatMove"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""dc76e022-80dc-4b72-b300-d606dfe7d2b2"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CombatMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""47151596-cc1b-4533-a9fa-8c986e9267d0"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CombatMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""c6c601a0-7dc2-4657-865a-66f478dcc171"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CombatMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""882642e1-4fbf-46cc-98f2-2d2b569335dd"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CombatMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        },
        {
            ""name"": ""RadialMenu"",
            ""id"": ""a35f5363-5809-4fd2-a0db-85161b842708"",
            ""actions"": [
                {
                    ""name"": ""MenuMove"",
                    ""type"": ""Button"",
                    ""id"": ""d687edc3-e564-4289-b36a-75d4d10e85ca"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MenuAffirm"",
                    ""type"": ""Button"",
                    ""id"": ""2c4ffbf0-35a5-4025-8885-7e48466429ef"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MenuCancel"",
                    ""type"": ""Button"",
                    ""id"": ""546661fd-9ef3-47a0-b8fe-ab61be582e77"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""ae2368ec-cb63-4acb-a3cb-40355beba0a8"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""MenuMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""be9e33d7-0811-4f3e-a736-494fa9641c99"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""MenuAffirm"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e226c2cf-48a9-47b7-a2ae-d27763485f4d"",
                    ""path"": ""<HID::Bensussen Deutsch & Associates,Inc.(BDA) Core (Plus) Wired Controller>/button2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""MenuAffirm"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1e7930de-57fb-4a0d-9935-c39c3e7d1b83"",
                    ""path"": ""<HID::2Axes 11Keys Game  Pad>/button3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""MenuAffirm"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e76e6bd3-7d39-4055-b396-3fbc48904b13"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""MenuCancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2e7acdc0-d9cb-4ab2-9649-6795f742b4dd"",
                    ""path"": ""<HID::Bensussen Deutsch & Associates,Inc.(BDA) Core (Plus) Wired Controller>/button3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""MenuCancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""377053c6-7f62-4ed6-8601-7288e63423a3"",
                    ""path"": ""<HID::2Axes 11Keys Game  Pad>/button2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""MenuCancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""07eda96a-4263-4308-b34e-6b34689aee3e"",
                    ""path"": ""<Joystick>/stick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Joystick"",
                    ""action"": ""MenuMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2beae4e0-40f8-462e-b407-ff214152d3f5"",
                    ""path"": ""<HID::SZMY-POWER CO.,LTD. BDA GP1>/button2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Joystick"",
                    ""action"": ""MenuAffirm"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""229c6651-eaf3-47cc-83c8-ca79440dee59"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MenuAffirm"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7b23b2d2-cd60-4f25-965c-0e6e901ed51f"",
                    ""path"": ""<HID::SZMY-POWER CO.,LTD. BDA GP1>/button3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Joystick"",
                    ""action"": ""MenuCancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""10a6e83e-2248-4628-af61-708d32b441ef"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MenuCancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""74dc512d-620e-41ca-a556-03dd9aa26a56"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MenuMove"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""f4a1992a-5b56-494d-a68f-afbfb07ce081"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MenuMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""db1cb406-47f1-4141-b287-04658fb256a0"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MenuMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""21c186cb-a05b-44be-bf1c-720a43988e14"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MenuMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""8c4912bc-8325-4870-9d03-c60d1ec9442a"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MenuMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        },
        {
            ""name"": ""Treasure"",
            ""id"": ""6681ecb8-af50-4165-b5fa-9247551783d5"",
            ""actions"": [
                {
                    ""name"": ""TreasureMove"",
                    ""type"": ""Button"",
                    ""id"": ""9de8703c-f58e-4349-87fa-8fe3d4536486"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TreasureAffirm"",
                    ""type"": ""Button"",
                    ""id"": ""9ee384ff-dcd0-49cb-a3be-06fd9edd13ae"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TreasureCancel"",
                    ""type"": ""Button"",
                    ""id"": ""f1c2cc72-4f4b-4649-99f2-ba67707dd67e"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""9b63c548-5c61-4e56-8c6f-172e4f8d59fa"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""TreasureMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""202f59f0-c8d7-4077-b9f2-3db2c51053db"",
                    ""path"": ""<Joystick>/stick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Joystick"",
                    ""action"": ""TreasureMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""ade7d838-645c-46c8-93b9-08c7872a5082"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TreasureMove"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""c284ba9a-f339-45fc-a639-783942e92c84"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TreasureMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""8494b14f-a067-4a8e-bc57-c837fc94242d"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TreasureMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""9028f366-ddeb-4cd2-ad7c-6ad8e74e031c"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TreasureMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""a7094735-b046-43c5-98dd-c8d91945bba4"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TreasureMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""6985b9f7-1be7-481c-afcc-e2bbc9847b96"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""TreasureAffirm"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8302c8b0-c05a-4ccc-92f6-6fb839750040"",
                    ""path"": ""<HID::Bensussen Deutsch & Associates,Inc.(BDA) Core (Plus) Wired Controller>/button2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""TreasureAffirm"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""642b3736-b009-434b-bdbf-f72e2b81e7ec"",
                    ""path"": ""<HID::2Axes 11Keys Game  Pad>/button3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""TreasureAffirm"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""65659843-bd53-4e56-a726-586239db976c"",
                    ""path"": ""<HID::SZMY-POWER CO.,LTD. BDA GP1>/button2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Joystick"",
                    ""action"": ""TreasureAffirm"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""39992c0d-7d55-46b3-80ea-006ad85105e7"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TreasureAffirm"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a54d51b0-f4c1-4dcc-ac2b-477aa45e4cc8"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""TreasureCancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6539c219-6de8-4053-8575-dd386c379fb7"",
                    ""path"": ""<HID::Bensussen Deutsch & Associates,Inc.(BDA) Core (Plus) Wired Controller>/button3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""TreasureCancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a9e1e5ae-af79-4d3f-b72c-691be073bfa0"",
                    ""path"": ""<HID::2Axes 11Keys Game  Pad>/button2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""TreasureCancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3197bd78-20e7-4ae6-a8b1-804d348cd980"",
                    ""path"": ""<HID::SZMY-POWER CO.,LTD. BDA GP1>/button3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Joystick"",
                    ""action"": ""TreasureCancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2521f586-9c38-4e92-93c2-79943da76ebb"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TreasureCancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Controller"",
            ""bindingGroup"": ""Controller"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": true,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Joystick"",
            ""bindingGroup"": ""Joystick"",
            ""devices"": [
                {
                    ""devicePath"": ""<Joystick>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Move = m_Player.FindAction("Move", throwIfNotFound: true);
        m_Player_CombatMove = m_Player.FindAction("CombatMove", throwIfNotFound: true);
        m_Player_Affirm = m_Player.FindAction("Affirm", throwIfNotFound: true);
        m_Player_Cancel = m_Player.FindAction("Cancel", throwIfNotFound: true);
        m_Player_OpenRadialMenu = m_Player.FindAction("OpenRadialMenu", throwIfNotFound: true);
        // RadialMenu
        m_RadialMenu = asset.FindActionMap("RadialMenu", throwIfNotFound: true);
        m_RadialMenu_MenuMove = m_RadialMenu.FindAction("MenuMove", throwIfNotFound: true);
        m_RadialMenu_MenuAffirm = m_RadialMenu.FindAction("MenuAffirm", throwIfNotFound: true);
        m_RadialMenu_MenuCancel = m_RadialMenu.FindAction("MenuCancel", throwIfNotFound: true);
        // Treasure
        m_Treasure = asset.FindActionMap("Treasure", throwIfNotFound: true);
        m_Treasure_TreasureMove = m_Treasure.FindAction("TreasureMove", throwIfNotFound: true);
        m_Treasure_TreasureAffirm = m_Treasure.FindAction("TreasureAffirm", throwIfNotFound: true);
        m_Treasure_TreasureCancel = m_Treasure.FindAction("TreasureCancel", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Move;
    private readonly InputAction m_Player_CombatMove;
    private readonly InputAction m_Player_Affirm;
    private readonly InputAction m_Player_Cancel;
    private readonly InputAction m_Player_OpenRadialMenu;
    public struct PlayerActions
    {
        private @InputMaster m_Wrapper;
        public PlayerActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Player_Move;
        public InputAction @CombatMove => m_Wrapper.m_Player_CombatMove;
        public InputAction @Affirm => m_Wrapper.m_Player_Affirm;
        public InputAction @Cancel => m_Wrapper.m_Player_Cancel;
        public InputAction @OpenRadialMenu => m_Wrapper.m_Player_OpenRadialMenu;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @CombatMove.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCombatMove;
                @CombatMove.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCombatMove;
                @CombatMove.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCombatMove;
                @Affirm.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAffirm;
                @Affirm.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAffirm;
                @Affirm.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAffirm;
                @Cancel.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCancel;
                @Cancel.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCancel;
                @Cancel.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCancel;
                @OpenRadialMenu.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnOpenRadialMenu;
                @OpenRadialMenu.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnOpenRadialMenu;
                @OpenRadialMenu.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnOpenRadialMenu;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @CombatMove.started += instance.OnCombatMove;
                @CombatMove.performed += instance.OnCombatMove;
                @CombatMove.canceled += instance.OnCombatMove;
                @Affirm.started += instance.OnAffirm;
                @Affirm.performed += instance.OnAffirm;
                @Affirm.canceled += instance.OnAffirm;
                @Cancel.started += instance.OnCancel;
                @Cancel.performed += instance.OnCancel;
                @Cancel.canceled += instance.OnCancel;
                @OpenRadialMenu.started += instance.OnOpenRadialMenu;
                @OpenRadialMenu.performed += instance.OnOpenRadialMenu;
                @OpenRadialMenu.canceled += instance.OnOpenRadialMenu;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);

    // RadialMenu
    private readonly InputActionMap m_RadialMenu;
    private IRadialMenuActions m_RadialMenuActionsCallbackInterface;
    private readonly InputAction m_RadialMenu_MenuMove;
    private readonly InputAction m_RadialMenu_MenuAffirm;
    private readonly InputAction m_RadialMenu_MenuCancel;
    public struct RadialMenuActions
    {
        private @InputMaster m_Wrapper;
        public RadialMenuActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @MenuMove => m_Wrapper.m_RadialMenu_MenuMove;
        public InputAction @MenuAffirm => m_Wrapper.m_RadialMenu_MenuAffirm;
        public InputAction @MenuCancel => m_Wrapper.m_RadialMenu_MenuCancel;
        public InputActionMap Get() { return m_Wrapper.m_RadialMenu; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(RadialMenuActions set) { return set.Get(); }
        public void SetCallbacks(IRadialMenuActions instance)
        {
            if (m_Wrapper.m_RadialMenuActionsCallbackInterface != null)
            {
                @MenuMove.started -= m_Wrapper.m_RadialMenuActionsCallbackInterface.OnMenuMove;
                @MenuMove.performed -= m_Wrapper.m_RadialMenuActionsCallbackInterface.OnMenuMove;
                @MenuMove.canceled -= m_Wrapper.m_RadialMenuActionsCallbackInterface.OnMenuMove;
                @MenuAffirm.started -= m_Wrapper.m_RadialMenuActionsCallbackInterface.OnMenuAffirm;
                @MenuAffirm.performed -= m_Wrapper.m_RadialMenuActionsCallbackInterface.OnMenuAffirm;
                @MenuAffirm.canceled -= m_Wrapper.m_RadialMenuActionsCallbackInterface.OnMenuAffirm;
                @MenuCancel.started -= m_Wrapper.m_RadialMenuActionsCallbackInterface.OnMenuCancel;
                @MenuCancel.performed -= m_Wrapper.m_RadialMenuActionsCallbackInterface.OnMenuCancel;
                @MenuCancel.canceled -= m_Wrapper.m_RadialMenuActionsCallbackInterface.OnMenuCancel;
            }
            m_Wrapper.m_RadialMenuActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MenuMove.started += instance.OnMenuMove;
                @MenuMove.performed += instance.OnMenuMove;
                @MenuMove.canceled += instance.OnMenuMove;
                @MenuAffirm.started += instance.OnMenuAffirm;
                @MenuAffirm.performed += instance.OnMenuAffirm;
                @MenuAffirm.canceled += instance.OnMenuAffirm;
                @MenuCancel.started += instance.OnMenuCancel;
                @MenuCancel.performed += instance.OnMenuCancel;
                @MenuCancel.canceled += instance.OnMenuCancel;
            }
        }
    }
    public RadialMenuActions @RadialMenu => new RadialMenuActions(this);

    // Treasure
    private readonly InputActionMap m_Treasure;
    private ITreasureActions m_TreasureActionsCallbackInterface;
    private readonly InputAction m_Treasure_TreasureMove;
    private readonly InputAction m_Treasure_TreasureAffirm;
    private readonly InputAction m_Treasure_TreasureCancel;
    public struct TreasureActions
    {
        private @InputMaster m_Wrapper;
        public TreasureActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @TreasureMove => m_Wrapper.m_Treasure_TreasureMove;
        public InputAction @TreasureAffirm => m_Wrapper.m_Treasure_TreasureAffirm;
        public InputAction @TreasureCancel => m_Wrapper.m_Treasure_TreasureCancel;
        public InputActionMap Get() { return m_Wrapper.m_Treasure; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(TreasureActions set) { return set.Get(); }
        public void SetCallbacks(ITreasureActions instance)
        {
            if (m_Wrapper.m_TreasureActionsCallbackInterface != null)
            {
                @TreasureMove.started -= m_Wrapper.m_TreasureActionsCallbackInterface.OnTreasureMove;
                @TreasureMove.performed -= m_Wrapper.m_TreasureActionsCallbackInterface.OnTreasureMove;
                @TreasureMove.canceled -= m_Wrapper.m_TreasureActionsCallbackInterface.OnTreasureMove;
                @TreasureAffirm.started -= m_Wrapper.m_TreasureActionsCallbackInterface.OnTreasureAffirm;
                @TreasureAffirm.performed -= m_Wrapper.m_TreasureActionsCallbackInterface.OnTreasureAffirm;
                @TreasureAffirm.canceled -= m_Wrapper.m_TreasureActionsCallbackInterface.OnTreasureAffirm;
                @TreasureCancel.started -= m_Wrapper.m_TreasureActionsCallbackInterface.OnTreasureCancel;
                @TreasureCancel.performed -= m_Wrapper.m_TreasureActionsCallbackInterface.OnTreasureCancel;
                @TreasureCancel.canceled -= m_Wrapper.m_TreasureActionsCallbackInterface.OnTreasureCancel;
            }
            m_Wrapper.m_TreasureActionsCallbackInterface = instance;
            if (instance != null)
            {
                @TreasureMove.started += instance.OnTreasureMove;
                @TreasureMove.performed += instance.OnTreasureMove;
                @TreasureMove.canceled += instance.OnTreasureMove;
                @TreasureAffirm.started += instance.OnTreasureAffirm;
                @TreasureAffirm.performed += instance.OnTreasureAffirm;
                @TreasureAffirm.canceled += instance.OnTreasureAffirm;
                @TreasureCancel.started += instance.OnTreasureCancel;
                @TreasureCancel.performed += instance.OnTreasureCancel;
                @TreasureCancel.canceled += instance.OnTreasureCancel;
            }
        }
    }
    public TreasureActions @Treasure => new TreasureActions(this);
    private int m_ControllerSchemeIndex = -1;
    public InputControlScheme ControllerScheme
    {
        get
        {
            if (m_ControllerSchemeIndex == -1) m_ControllerSchemeIndex = asset.FindControlSchemeIndex("Controller");
            return asset.controlSchemes[m_ControllerSchemeIndex];
        }
    }
    private int m_JoystickSchemeIndex = -1;
    public InputControlScheme JoystickScheme
    {
        get
        {
            if (m_JoystickSchemeIndex == -1) m_JoystickSchemeIndex = asset.FindControlSchemeIndex("Joystick");
            return asset.controlSchemes[m_JoystickSchemeIndex];
        }
    }
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnCombatMove(InputAction.CallbackContext context);
        void OnAffirm(InputAction.CallbackContext context);
        void OnCancel(InputAction.CallbackContext context);
        void OnOpenRadialMenu(InputAction.CallbackContext context);
    }
    public interface IRadialMenuActions
    {
        void OnMenuMove(InputAction.CallbackContext context);
        void OnMenuAffirm(InputAction.CallbackContext context);
        void OnMenuCancel(InputAction.CallbackContext context);
    }
    public interface ITreasureActions
    {
        void OnTreasureMove(InputAction.CallbackContext context);
        void OnTreasureAffirm(InputAction.CallbackContext context);
        void OnTreasureCancel(InputAction.CallbackContext context);
    }
}
