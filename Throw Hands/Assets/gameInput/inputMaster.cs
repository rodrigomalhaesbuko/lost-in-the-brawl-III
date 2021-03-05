// GENERATED AUTOMATICALLY FROM 'Assets/gameInput/inputMaster.inputactions'

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
    ""name"": ""inputMaster"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""6957527f-3ed2-4e69-acc6-eddb02e81377"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""79338279-d9ee-43c2-9043-c9ee02c12279"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LeftArmShoot"",
                    ""type"": ""Button"",
                    ""id"": ""66423509-4994-48dd-a522-869ed0e2b90f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RightArmShoot"",
                    ""type"": ""Button"",
                    ""id"": ""5e2cb39e-456d-4ad2-a5d3-4daf436cf5e7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Parry"",
                    ""type"": ""Button"",
                    ""id"": ""c646a421-0b3c-4c77-ae04-371780fafc85"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LocalMoveP1"",
                    ""type"": ""Button"",
                    ""id"": ""015e6c9a-a6f8-4748-ad28-f95c9132a3af"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LocalMoveP2"",
                    ""type"": ""Button"",
                    ""id"": ""272f7407-691b-4802-af9e-7d3333e1378d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LocalRightShootP1"",
                    ""type"": ""Button"",
                    ""id"": ""3295bb49-2308-4807-ac9b-1a9aba349007"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LocalLeftShootP1"",
                    ""type"": ""Button"",
                    ""id"": ""998bdad4-6e52-4712-83a3-008bfe2483f9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LocalRightShootP2"",
                    ""type"": ""Button"",
                    ""id"": ""683b0f0b-e316-4c14-929b-f4b2d786b19a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LocalLeftShootP2"",
                    ""type"": ""Button"",
                    ""id"": ""fb3d8bbc-07df-4c0a-bac8-1c1862ff0ae7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LocalParryP1"",
                    ""type"": ""Button"",
                    ""id"": ""c20d73bb-1091-410f-a669-714d00e66964"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LocalParryP2"",
                    ""type"": ""Button"",
                    ""id"": ""935cf0e2-6fe5-4cee-930a-509152dfa98f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""cf329009-f72c-49ca-bcca-069cc994d3f7"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""68059b7a-234b-400a-9e38-c7fc0877f06b"",
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
                    ""id"": ""f247e63e-c8ed-44db-8d1c-c3309a5f8338"",
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
                    ""id"": ""347ad2a2-6464-4c17-ad89-8db7b90be344"",
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
                    ""id"": ""149a9683-ee66-4fd4-9059-2455de08d098"",
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
                    ""id"": ""3676eb1f-d0e7-4450-ab9a-258c798e27e1"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""keyarrows"",
                    ""id"": ""4bc4eb6b-1a04-4fb6-a619-6c720e705bb1"",
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
                    ""id"": ""e22a3679-f6b5-4ace-8723-d3893b985aa9"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""a1dbec5b-2336-4f5e-95db-b30c90db6cfe"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""c52015f0-7fa6-439e-a14f-41c63b0b3c5b"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""51e3bb21-0268-4cfd-b5a8-2b4161183baf"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""4af394f0-4f03-492d-9f2e-689ad9048c25"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftArmShoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1bdfea4d-39b1-4907-8692-9019d5900a0d"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftArmShoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fe97898d-ace2-4188-85db-6b7cbeab33aa"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RightArmShoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e7fead35-ed9e-4f32-a88c-6cee2176bff0"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RightArmShoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b1920847-4287-41e5-adc8-2f929304588b"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Parry"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d867e2f6-b267-4c8b-9b26-6661276f49d5"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Parry"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""80941685-5e5b-468a-bc5f-7d04fe97ce9e"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LocalMoveP1"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""495a93d0-dafc-4390-ae11-71c25d99203b"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Douglas"",
                    ""action"": ""LocalMoveP1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""3feb38a2-a11d-49ab-8768-05a836dad3d9"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Douglas"",
                    ""action"": ""LocalMoveP1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""4e6ccfb4-7eb6-4e90-9abc-d073aa16efd7"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Douglas"",
                    ""action"": ""LocalMoveP1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""1ac0fb2a-1290-4571-85b5-13ee2f114b3a"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Douglas"",
                    ""action"": ""LocalMoveP1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""9d430e72-f774-42aa-b0d9-dcbab5073adf"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Douglas"",
                    ""action"": ""LocalMoveP1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""keyarrows"",
                    ""id"": ""06e6ac10-08b3-4ee1-bc2b-1aa8cec2fe28"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LocalMoveP2"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""29c93859-bacc-4b0c-947c-4f55a2885a6c"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Carlos"",
                    ""action"": ""LocalMoveP2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""169f0507-0f25-4ac5-a275-f87fdef74033"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Carlos"",
                    ""action"": ""LocalMoveP2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""830404aa-53a7-4a93-b300-add22f40a075"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Carlos"",
                    ""action"": ""LocalMoveP2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""f97632ff-0a9b-41ca-9d80-b8054c541aac"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Carlos"",
                    ""action"": ""LocalMoveP2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""6bacca32-fe79-416d-9dff-79bdffae3df6"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Douglas"",
                    ""action"": ""LocalRightShootP1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c8d077cf-607b-426d-a0ab-1ae8214450b4"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Douglas"",
                    ""action"": ""LocalRightShootP1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a7072c42-ab22-4d76-a167-2936752ca000"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Douglas"",
                    ""action"": ""LocalLeftShootP1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2d92eef8-b9cd-49fb-8d13-2d3dd86dd5b0"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Douglas"",
                    ""action"": ""LocalLeftShootP1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""67dbe95e-20b9-4146-b578-db0ec9303220"",
                    ""path"": ""<Keyboard>/n"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Carlos"",
                    ""action"": ""LocalRightShootP2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6429a449-3a86-4f26-ac3b-23d5d0cff8be"",
                    ""path"": ""<Keyboard>/m"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Carlos"",
                    ""action"": ""LocalLeftShootP2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b08f38fe-f5b3-4c68-8876-eb0e64f105df"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Douglas"",
                    ""action"": ""LocalParryP1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8fd1bebe-4448-4c3b-8167-34ac57f0f6f5"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Douglas"",
                    ""action"": ""LocalParryP1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4f947c1c-d059-4eb1-b2cc-672314c302df"",
                    ""path"": ""<Keyboard>/comma"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Carlos"",
                    ""action"": ""LocalParryP2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5f3f39a8-c963-4dfc-8dc4-b1ece053e826"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Carlos"",
                    ""action"": ""LocalMoveP2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""84353a73-fdbd-441d-b5be-022952cedcc5"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Carlos"",
                    ""action"": ""LocalRightShootP2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""514f49f2-5f99-4628-b014-4d7101e33fd0"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Carlos"",
                    ""action"": ""LocalLeftShootP2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2c41d310-10b9-4094-9b46-d6e81cf074fc"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Carlos"",
                    ""action"": ""LocalParryP2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""MainMenu"",
            ""id"": ""231c7e4b-b02c-467d-abc0-f3db652748c6"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Button"",
                    ""id"": ""e518d00d-0729-4c1b-9e52-0c935a5cc96f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Select"",
                    ""type"": ""Button"",
                    ""id"": ""72781113-aa56-4c82-a2de-c63794c14f18"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""keyboard"",
                    ""id"": ""751cb9c8-619e-453a-be45-da65d428a740"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""410d14cd-d1f3-44b0-849d-565d7e29a7f4"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""c648711b-ec13-4b14-a564-17dc2ce073fe"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""gamepadseta"",
                    ""id"": ""57d9aafe-5b68-4ec2-ba81-5161b287a07d"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""3dc819f9-7f29-4ef4-95f7-78ba7229c559"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""e8b571ef-82f8-4adf-a898-38ded4cab8ed"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""2639c5e1-f2d0-4794-a0b6-9022bfdea9df"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6d2b0421-b6c9-4e65-af5d-987096e76737"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""StaticScene"",
            ""id"": ""15a026fc-69e5-4aad-8beb-2de07d0ff233"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Button"",
                    ""id"": ""e6bb6c10-ce67-4348-8eea-4c1708cbdaaa"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Select"",
                    ""type"": ""Button"",
                    ""id"": ""a82fcc27-c016-41ee-a4b8-e9317f858e78"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""636135b7-89cc-42e5-97e8-fb4d1e11bfc4"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""66c2ee19-19a1-4411-8355-4828ea66296b"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""keyarrows"",
                    ""id"": ""dc451f0e-2cab-484c-adcf-29b42f2d2a0d"",
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
                    ""id"": ""4dc6c2bb-199e-4e8e-b611-9f8c15bc0543"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""52a2c35e-4ca2-4261-a5ec-6070097ee43a"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""efa0a402-a5d1-4a89-b601-c7ed26565ceb"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""9990c106-e1df-4e1b-b9a7-7a2bec8fcc7b"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""gamepad"",
                    ""id"": ""42dcb094-9df8-4a56-bc2d-0b89ee2f61c4"",
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
                    ""id"": ""f9c71ef2-7304-4713-a43d-bcf6d6bb569f"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""9c6f256d-66cf-41ed-83a2-4108ed7b20fb"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""e329e975-58f0-4451-a4db-1ec571b74eee"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""752052e1-075b-45c1-948a-36a7d4e3443d"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": [
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
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Carlos"",
            ""bindingGroup"": ""Carlos"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Douglas"",
            ""bindingGroup"": ""Douglas"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Gameplay
        m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
        m_Gameplay_Move = m_Gameplay.FindAction("Move", throwIfNotFound: true);
        m_Gameplay_LeftArmShoot = m_Gameplay.FindAction("LeftArmShoot", throwIfNotFound: true);
        m_Gameplay_RightArmShoot = m_Gameplay.FindAction("RightArmShoot", throwIfNotFound: true);
        m_Gameplay_Parry = m_Gameplay.FindAction("Parry", throwIfNotFound: true);
        m_Gameplay_LocalMoveP1 = m_Gameplay.FindAction("LocalMoveP1", throwIfNotFound: true);
        m_Gameplay_LocalMoveP2 = m_Gameplay.FindAction("LocalMoveP2", throwIfNotFound: true);
        m_Gameplay_LocalRightShootP1 = m_Gameplay.FindAction("LocalRightShootP1", throwIfNotFound: true);
        m_Gameplay_LocalLeftShootP1 = m_Gameplay.FindAction("LocalLeftShootP1", throwIfNotFound: true);
        m_Gameplay_LocalRightShootP2 = m_Gameplay.FindAction("LocalRightShootP2", throwIfNotFound: true);
        m_Gameplay_LocalLeftShootP2 = m_Gameplay.FindAction("LocalLeftShootP2", throwIfNotFound: true);
        m_Gameplay_LocalParryP1 = m_Gameplay.FindAction("LocalParryP1", throwIfNotFound: true);
        m_Gameplay_LocalParryP2 = m_Gameplay.FindAction("LocalParryP2", throwIfNotFound: true);
        // MainMenu
        m_MainMenu = asset.FindActionMap("MainMenu", throwIfNotFound: true);
        m_MainMenu_Move = m_MainMenu.FindAction("Move", throwIfNotFound: true);
        m_MainMenu_Select = m_MainMenu.FindAction("Select", throwIfNotFound: true);
        // StaticScene
        m_StaticScene = asset.FindActionMap("StaticScene", throwIfNotFound: true);
        m_StaticScene_Move = m_StaticScene.FindAction("Move", throwIfNotFound: true);
        m_StaticScene_Select = m_StaticScene.FindAction("Select", throwIfNotFound: true);
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

    // Gameplay
    private readonly InputActionMap m_Gameplay;
    private IGameplayActions m_GameplayActionsCallbackInterface;
    private readonly InputAction m_Gameplay_Move;
    private readonly InputAction m_Gameplay_LeftArmShoot;
    private readonly InputAction m_Gameplay_RightArmShoot;
    private readonly InputAction m_Gameplay_Parry;
    private readonly InputAction m_Gameplay_LocalMoveP1;
    private readonly InputAction m_Gameplay_LocalMoveP2;
    private readonly InputAction m_Gameplay_LocalRightShootP1;
    private readonly InputAction m_Gameplay_LocalLeftShootP1;
    private readonly InputAction m_Gameplay_LocalRightShootP2;
    private readonly InputAction m_Gameplay_LocalLeftShootP2;
    private readonly InputAction m_Gameplay_LocalParryP1;
    private readonly InputAction m_Gameplay_LocalParryP2;
    public struct GameplayActions
    {
        private @InputMaster m_Wrapper;
        public GameplayActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Gameplay_Move;
        public InputAction @LeftArmShoot => m_Wrapper.m_Gameplay_LeftArmShoot;
        public InputAction @RightArmShoot => m_Wrapper.m_Gameplay_RightArmShoot;
        public InputAction @Parry => m_Wrapper.m_Gameplay_Parry;
        public InputAction @LocalMoveP1 => m_Wrapper.m_Gameplay_LocalMoveP1;
        public InputAction @LocalMoveP2 => m_Wrapper.m_Gameplay_LocalMoveP2;
        public InputAction @LocalRightShootP1 => m_Wrapper.m_Gameplay_LocalRightShootP1;
        public InputAction @LocalLeftShootP1 => m_Wrapper.m_Gameplay_LocalLeftShootP1;
        public InputAction @LocalRightShootP2 => m_Wrapper.m_Gameplay_LocalRightShootP2;
        public InputAction @LocalLeftShootP2 => m_Wrapper.m_Gameplay_LocalLeftShootP2;
        public InputAction @LocalParryP1 => m_Wrapper.m_Gameplay_LocalParryP1;
        public InputAction @LocalParryP2 => m_Wrapper.m_Gameplay_LocalParryP2;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                @LeftArmShoot.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLeftArmShoot;
                @LeftArmShoot.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLeftArmShoot;
                @LeftArmShoot.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLeftArmShoot;
                @RightArmShoot.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRightArmShoot;
                @RightArmShoot.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRightArmShoot;
                @RightArmShoot.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRightArmShoot;
                @Parry.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnParry;
                @Parry.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnParry;
                @Parry.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnParry;
                @LocalMoveP1.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLocalMoveP1;
                @LocalMoveP1.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLocalMoveP1;
                @LocalMoveP1.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLocalMoveP1;
                @LocalMoveP2.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLocalMoveP2;
                @LocalMoveP2.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLocalMoveP2;
                @LocalMoveP2.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLocalMoveP2;
                @LocalRightShootP1.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLocalRightShootP1;
                @LocalRightShootP1.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLocalRightShootP1;
                @LocalRightShootP1.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLocalRightShootP1;
                @LocalLeftShootP1.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLocalLeftShootP1;
                @LocalLeftShootP1.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLocalLeftShootP1;
                @LocalLeftShootP1.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLocalLeftShootP1;
                @LocalRightShootP2.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLocalRightShootP2;
                @LocalRightShootP2.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLocalRightShootP2;
                @LocalRightShootP2.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLocalRightShootP2;
                @LocalLeftShootP2.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLocalLeftShootP2;
                @LocalLeftShootP2.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLocalLeftShootP2;
                @LocalLeftShootP2.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLocalLeftShootP2;
                @LocalParryP1.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLocalParryP1;
                @LocalParryP1.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLocalParryP1;
                @LocalParryP1.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLocalParryP1;
                @LocalParryP2.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLocalParryP2;
                @LocalParryP2.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLocalParryP2;
                @LocalParryP2.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLocalParryP2;
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @LeftArmShoot.started += instance.OnLeftArmShoot;
                @LeftArmShoot.performed += instance.OnLeftArmShoot;
                @LeftArmShoot.canceled += instance.OnLeftArmShoot;
                @RightArmShoot.started += instance.OnRightArmShoot;
                @RightArmShoot.performed += instance.OnRightArmShoot;
                @RightArmShoot.canceled += instance.OnRightArmShoot;
                @Parry.started += instance.OnParry;
                @Parry.performed += instance.OnParry;
                @Parry.canceled += instance.OnParry;
                @LocalMoveP1.started += instance.OnLocalMoveP1;
                @LocalMoveP1.performed += instance.OnLocalMoveP1;
                @LocalMoveP1.canceled += instance.OnLocalMoveP1;
                @LocalMoveP2.started += instance.OnLocalMoveP2;
                @LocalMoveP2.performed += instance.OnLocalMoveP2;
                @LocalMoveP2.canceled += instance.OnLocalMoveP2;
                @LocalRightShootP1.started += instance.OnLocalRightShootP1;
                @LocalRightShootP1.performed += instance.OnLocalRightShootP1;
                @LocalRightShootP1.canceled += instance.OnLocalRightShootP1;
                @LocalLeftShootP1.started += instance.OnLocalLeftShootP1;
                @LocalLeftShootP1.performed += instance.OnLocalLeftShootP1;
                @LocalLeftShootP1.canceled += instance.OnLocalLeftShootP1;
                @LocalRightShootP2.started += instance.OnLocalRightShootP2;
                @LocalRightShootP2.performed += instance.OnLocalRightShootP2;
                @LocalRightShootP2.canceled += instance.OnLocalRightShootP2;
                @LocalLeftShootP2.started += instance.OnLocalLeftShootP2;
                @LocalLeftShootP2.performed += instance.OnLocalLeftShootP2;
                @LocalLeftShootP2.canceled += instance.OnLocalLeftShootP2;
                @LocalParryP1.started += instance.OnLocalParryP1;
                @LocalParryP1.performed += instance.OnLocalParryP1;
                @LocalParryP1.canceled += instance.OnLocalParryP1;
                @LocalParryP2.started += instance.OnLocalParryP2;
                @LocalParryP2.performed += instance.OnLocalParryP2;
                @LocalParryP2.canceled += instance.OnLocalParryP2;
            }
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);

    // MainMenu
    private readonly InputActionMap m_MainMenu;
    private IMainMenuActions m_MainMenuActionsCallbackInterface;
    private readonly InputAction m_MainMenu_Move;
    private readonly InputAction m_MainMenu_Select;
    public struct MainMenuActions
    {
        private @InputMaster m_Wrapper;
        public MainMenuActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_MainMenu_Move;
        public InputAction @Select => m_Wrapper.m_MainMenu_Select;
        public InputActionMap Get() { return m_Wrapper.m_MainMenu; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MainMenuActions set) { return set.Get(); }
        public void SetCallbacks(IMainMenuActions instance)
        {
            if (m_Wrapper.m_MainMenuActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_MainMenuActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_MainMenuActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_MainMenuActionsCallbackInterface.OnMove;
                @Select.started -= m_Wrapper.m_MainMenuActionsCallbackInterface.OnSelect;
                @Select.performed -= m_Wrapper.m_MainMenuActionsCallbackInterface.OnSelect;
                @Select.canceled -= m_Wrapper.m_MainMenuActionsCallbackInterface.OnSelect;
            }
            m_Wrapper.m_MainMenuActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Select.started += instance.OnSelect;
                @Select.performed += instance.OnSelect;
                @Select.canceled += instance.OnSelect;
            }
        }
    }
    public MainMenuActions @MainMenu => new MainMenuActions(this);

    // StaticScene
    private readonly InputActionMap m_StaticScene;
    private IStaticSceneActions m_StaticSceneActionsCallbackInterface;
    private readonly InputAction m_StaticScene_Move;
    private readonly InputAction m_StaticScene_Select;
    public struct StaticSceneActions
    {
        private @InputMaster m_Wrapper;
        public StaticSceneActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_StaticScene_Move;
        public InputAction @Select => m_Wrapper.m_StaticScene_Select;
        public InputActionMap Get() { return m_Wrapper.m_StaticScene; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(StaticSceneActions set) { return set.Get(); }
        public void SetCallbacks(IStaticSceneActions instance)
        {
            if (m_Wrapper.m_StaticSceneActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_StaticSceneActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_StaticSceneActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_StaticSceneActionsCallbackInterface.OnMove;
                @Select.started -= m_Wrapper.m_StaticSceneActionsCallbackInterface.OnSelect;
                @Select.performed -= m_Wrapper.m_StaticSceneActionsCallbackInterface.OnSelect;
                @Select.canceled -= m_Wrapper.m_StaticSceneActionsCallbackInterface.OnSelect;
            }
            m_Wrapper.m_StaticSceneActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Select.started += instance.OnSelect;
                @Select.performed += instance.OnSelect;
                @Select.canceled += instance.OnSelect;
            }
        }
    }
    public StaticSceneActions @StaticScene => new StaticSceneActions(this);
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    private int m_CarlosSchemeIndex = -1;
    public InputControlScheme CarlosScheme
    {
        get
        {
            if (m_CarlosSchemeIndex == -1) m_CarlosSchemeIndex = asset.FindControlSchemeIndex("Carlos");
            return asset.controlSchemes[m_CarlosSchemeIndex];
        }
    }
    private int m_DouglasSchemeIndex = -1;
    public InputControlScheme DouglasScheme
    {
        get
        {
            if (m_DouglasSchemeIndex == -1) m_DouglasSchemeIndex = asset.FindControlSchemeIndex("Douglas");
            return asset.controlSchemes[m_DouglasSchemeIndex];
        }
    }
    public interface IGameplayActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnLeftArmShoot(InputAction.CallbackContext context);
        void OnRightArmShoot(InputAction.CallbackContext context);
        void OnParry(InputAction.CallbackContext context);
        void OnLocalMoveP1(InputAction.CallbackContext context);
        void OnLocalMoveP2(InputAction.CallbackContext context);
        void OnLocalRightShootP1(InputAction.CallbackContext context);
        void OnLocalLeftShootP1(InputAction.CallbackContext context);
        void OnLocalRightShootP2(InputAction.CallbackContext context);
        void OnLocalLeftShootP2(InputAction.CallbackContext context);
        void OnLocalParryP1(InputAction.CallbackContext context);
        void OnLocalParryP2(InputAction.CallbackContext context);
    }
    public interface IMainMenuActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnSelect(InputAction.CallbackContext context);
    }
    public interface IStaticSceneActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnSelect(InputAction.CallbackContext context);
    }
}
