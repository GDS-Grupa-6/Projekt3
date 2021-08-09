// GENERATED AUTOMATICALLY FROM 'Assets/_Scripts/Core/Input/Controls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Controls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Controls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controls"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""ec5dc0ba-4a5f-40d9-997b-44d97255d5a1"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""847856a7-0b7d-43ca-b434-dc8471529d93"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""CameraLook"",
                    ""type"": ""Value"",
                    ""id"": ""742bdcbc-b132-4abb-8aea-846b22591556"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Dash"",
                    ""type"": ""Button"",
                    ""id"": ""732aef4f-cfe6-4f36-bec0-79214532aa56"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Aim"",
                    ""type"": ""Button"",
                    ""id"": ""05770401-63c2-4731-8303-9f5a52d7fd6d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold""
                },
                {
                    ""name"": ""State1"",
                    ""type"": ""Button"",
                    ""id"": ""33c6f681-377a-4319-96af-0586ea2332ae"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""State2"",
                    ""type"": ""Button"",
                    ""id"": ""d9520450-9f54-482c-bfb4-a8421771226e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""State3"",
                    ""type"": ""Button"",
                    ""id"": ""bf232b82-63f9-49dc-b4bc-c4f8f6610f76"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""State4"",
                    ""type"": ""Button"",
                    ""id"": ""138eb9b0-328e-47a5-948e-a99369451264"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""DashHold"",
                    ""type"": ""Button"",
                    ""id"": ""4a421831-ad06-4002-bd21-c7c7ed4befa6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold""
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""517eb296-23ef-4d71-9471-4d0bb6987062"",
                    ""path"": ""2DVector(mode=1)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""6e4ead6f-d5b4-4fff-a8e9-c68671aab6dc"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Kayboard and mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""d3605c7b-f78c-4512-99b0-c17a588ff7ac"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Kayboard and mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""89ecaffb-c35c-4fd8-bc06-6885bb9e0f58"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Kayboard and mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""44c0342b-8dc7-45af-b7ed-4913f4206435"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Kayboard and mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""LeftStick"",
                    ""id"": ""752d39da-0890-48c5-a546-5ef8e3902208"",
                    ""path"": ""2DVector(mode=2)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""62bd5413-a2a4-4736-9431-de071ef50f9d"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Pad"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""40de3615-d231-41c5-bf07-d8663cb13012"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Pad"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""fc8e0de4-93a7-4373-8ec5-d0a0a109b601"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Pad"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""488999c8-b4ca-4073-a672-273bd083d3e0"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Pad"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""51830651-ce71-45fc-9473-80f4d14f65f4"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Kayboard and mouse"",
                    ""action"": ""CameraLook"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""RightStick"",
                    ""id"": ""c44a53b4-14c4-4aed-b496-f0eaa245b8c2"",
                    ""path"": ""2DVector(mode=2)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraLook"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""a971fe46-8c1d-430b-bf2d-9e8ad693d8a4"",
                    ""path"": ""<Gamepad>/rightStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Pad"",
                    ""action"": ""CameraLook"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""aa8bf2e7-f1bd-4e84-87ed-e76649d05944"",
                    ""path"": ""<Gamepad>/rightStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Pad"",
                    ""action"": ""CameraLook"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""9f708b97-46ae-420f-9826-3a9bd64c9018"",
                    ""path"": ""<Gamepad>/rightStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Pad"",
                    ""action"": ""CameraLook"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""bd652324-6a58-4ea0-a669-6725fc6cac47"",
                    ""path"": ""<Gamepad>/rightStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Pad"",
                    ""action"": ""CameraLook"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""13728719-8870-4880-b155-63f18e193483"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Kayboard and mouse"",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e031ca5c-4a1c-44f7-89ed-426074af1159"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Pad"",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cc3077f9-fd76-4414-818a-f91038461696"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Kayboard and mouse"",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7e05368d-d589-4e2e-b68b-1a04ee216745"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Pad"",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8a57084c-9571-4edb-87ea-48373f2c160c"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Kayboard and mouse"",
                    ""action"": ""State1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8f2a2c67-1b08-4dc7-888f-8b2c71f561f5"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Kayboard and mouse"",
                    ""action"": ""State2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""63cd34cc-8362-446a-b085-90392cf0617b"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Kayboard and mouse"",
                    ""action"": ""State3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""471ba992-01be-4ffe-818b-6fe2fd41bfe4"",
                    ""path"": ""<Keyboard>/4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Kayboard and mouse"",
                    ""action"": ""State4"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""436de17b-1e43-497a-bbfb-7f2a13f7dd60"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Kayboard and mouse"",
                    ""action"": ""DashHold"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f20b1978-3d89-4466-a1e1-0d0c08073e24"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Pad"",
                    ""action"": ""DashHold"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Kayboard and mouse"",
            ""bindingGroup"": ""Kayboard and mouse"",
            ""devices"": []
        },
        {
            ""name"": ""Pad"",
            ""bindingGroup"": ""Pad"",
            ""devices"": []
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Movement = m_Player.FindAction("Movement", throwIfNotFound: true);
        m_Player_CameraLook = m_Player.FindAction("CameraLook", throwIfNotFound: true);
        m_Player_Dash = m_Player.FindAction("Dash", throwIfNotFound: true);
        m_Player_Aim = m_Player.FindAction("Aim", throwIfNotFound: true);
        m_Player_State1 = m_Player.FindAction("State1", throwIfNotFound: true);
        m_Player_State2 = m_Player.FindAction("State2", throwIfNotFound: true);
        m_Player_State3 = m_Player.FindAction("State3", throwIfNotFound: true);
        m_Player_State4 = m_Player.FindAction("State4", throwIfNotFound: true);
        m_Player_DashHold = m_Player.FindAction("DashHold", throwIfNotFound: true);
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
    private readonly InputAction m_Player_Movement;
    private readonly InputAction m_Player_CameraLook;
    private readonly InputAction m_Player_Dash;
    private readonly InputAction m_Player_Aim;
    private readonly InputAction m_Player_State1;
    private readonly InputAction m_Player_State2;
    private readonly InputAction m_Player_State3;
    private readonly InputAction m_Player_State4;
    private readonly InputAction m_Player_DashHold;
    public struct PlayerActions
    {
        private @Controls m_Wrapper;
        public PlayerActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Player_Movement;
        public InputAction @CameraLook => m_Wrapper.m_Player_CameraLook;
        public InputAction @Dash => m_Wrapper.m_Player_Dash;
        public InputAction @Aim => m_Wrapper.m_Player_Aim;
        public InputAction @State1 => m_Wrapper.m_Player_State1;
        public InputAction @State2 => m_Wrapper.m_Player_State2;
        public InputAction @State3 => m_Wrapper.m_Player_State3;
        public InputAction @State4 => m_Wrapper.m_Player_State4;
        public InputAction @DashHold => m_Wrapper.m_Player_DashHold;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @CameraLook.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCameraLook;
                @CameraLook.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCameraLook;
                @CameraLook.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCameraLook;
                @Dash.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDash;
                @Dash.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDash;
                @Dash.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDash;
                @Aim.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAim;
                @Aim.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAim;
                @Aim.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAim;
                @State1.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnState1;
                @State1.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnState1;
                @State1.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnState1;
                @State2.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnState2;
                @State2.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnState2;
                @State2.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnState2;
                @State3.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnState3;
                @State3.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnState3;
                @State3.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnState3;
                @State4.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnState4;
                @State4.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnState4;
                @State4.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnState4;
                @DashHold.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDashHold;
                @DashHold.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDashHold;
                @DashHold.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDashHold;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @CameraLook.started += instance.OnCameraLook;
                @CameraLook.performed += instance.OnCameraLook;
                @CameraLook.canceled += instance.OnCameraLook;
                @Dash.started += instance.OnDash;
                @Dash.performed += instance.OnDash;
                @Dash.canceled += instance.OnDash;
                @Aim.started += instance.OnAim;
                @Aim.performed += instance.OnAim;
                @Aim.canceled += instance.OnAim;
                @State1.started += instance.OnState1;
                @State1.performed += instance.OnState1;
                @State1.canceled += instance.OnState1;
                @State2.started += instance.OnState2;
                @State2.performed += instance.OnState2;
                @State2.canceled += instance.OnState2;
                @State3.started += instance.OnState3;
                @State3.performed += instance.OnState3;
                @State3.canceled += instance.OnState3;
                @State4.started += instance.OnState4;
                @State4.performed += instance.OnState4;
                @State4.canceled += instance.OnState4;
                @DashHold.started += instance.OnDashHold;
                @DashHold.performed += instance.OnDashHold;
                @DashHold.canceled += instance.OnDashHold;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    private int m_KayboardandmouseSchemeIndex = -1;
    public InputControlScheme KayboardandmouseScheme
    {
        get
        {
            if (m_KayboardandmouseSchemeIndex == -1) m_KayboardandmouseSchemeIndex = asset.FindControlSchemeIndex("Kayboard and mouse");
            return asset.controlSchemes[m_KayboardandmouseSchemeIndex];
        }
    }
    private int m_PadSchemeIndex = -1;
    public InputControlScheme PadScheme
    {
        get
        {
            if (m_PadSchemeIndex == -1) m_PadSchemeIndex = asset.FindControlSchemeIndex("Pad");
            return asset.controlSchemes[m_PadSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnCameraLook(InputAction.CallbackContext context);
        void OnDash(InputAction.CallbackContext context);
        void OnAim(InputAction.CallbackContext context);
        void OnState1(InputAction.CallbackContext context);
        void OnState2(InputAction.CallbackContext context);
        void OnState3(InputAction.CallbackContext context);
        void OnState4(InputAction.CallbackContext context);
        void OnDashHold(InputAction.CallbackContext context);
    }
}
