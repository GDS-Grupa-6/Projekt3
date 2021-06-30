// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/InputSystem/InputSystem.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputSystemControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputSystemControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputSystem"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""73f53c25-2a51-4e44-aea3-14c4beb9ab59"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""88f679d7-ef04-443d-a413-ed3e0d2ec9f4"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Button"",
                    ""id"": ""41c790eb-b528-4154-b8bb-742ace5ad5e7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Dash"",
                    ""type"": ""Button"",
                    ""id"": ""22960287-52c9-4ac5-a88a-fab2d7eaaebf"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SwithTppShootState"",
                    ""type"": ""Button"",
                    ""id"": ""4c4a35c0-838f-40a5-a0d4-1606a33d4e68"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Aim"",
                    ""type"": ""Button"",
                    ""id"": ""722b41d4-2ff7-4c2b-bb69-ca0fb841127d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Mele"",
                    ""type"": ""Button"",
                    ""id"": ""8b115b0f-7d3a-4890-8528-ac2d852ee327"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""60b5ed41-820b-4de3-85b2-05d02dc7958a"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""c4f9d2e8-87b9-4a21-9b2a-ba0cd8f62479"",
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
                    ""id"": ""19325b7d-6d76-4a0d-828f-9391ad061f46"",
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
                    ""id"": ""693d8542-bf73-4dad-aab3-b352f24b8cfa"",
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
                    ""id"": ""64952456-ede6-4fcc-af7c-98b59066886e"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Kayboard and mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""4cbf59d9-d76b-4ee8-85f8-e20cab09ef52"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Kayboard and mouse"",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""616b9544-00ba-49ea-bae6-73cefcd3ffa0"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": ""Tap(pressPoint=0.4)"",
                    ""processors"": """",
                    ""groups"": ""Kayboard and mouse"",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5b6ae687-a822-40b8-91be-7500f09f2831"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": ""Kayboard and mouse"",
                    ""action"": ""SwithTppShootState"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5c522e22-975e-457b-9c78-edad03883caf"",
                    ""path"": ""<Mouse>/middleButton"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""293da254-78c8-4fbc-8478-f3c5a734ee34"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Kayboard and mouse"",
                    ""action"": ""Mele"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Camera"",
            ""id"": ""46df42e0-5be7-4794-8957-5d2c9d734224"",
            ""actions"": [
                {
                    ""name"": ""MouseLook"",
                    ""type"": ""PassThrough"",
                    ""id"": ""946a6961-303b-48d7-baa7-a2cb3d7d8acf"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""1fe847e3-b4ef-4c0d-927d-6805e5516b1d"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Kayboard and mouse"",
                    ""action"": ""MouseLook"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Panels"",
            ""id"": ""9993797d-78e7-4873-909d-450da8eea61f"",
            ""actions"": [
                {
                    ""name"": ""OnOffPausePanel"",
                    ""type"": ""Button"",
                    ""id"": ""31b23e90-3449-4bf8-b75d-6502023fab36"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""7d8bf4ff-6d01-40d3-8f98-62dc94011b05"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Kayboard and mouse"",
                    ""action"": ""OnOffPausePanel"",
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
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
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
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Movement = m_Player.FindAction("Movement", throwIfNotFound: true);
        m_Player_Shoot = m_Player.FindAction("Shoot", throwIfNotFound: true);
        m_Player_Dash = m_Player.FindAction("Dash", throwIfNotFound: true);
        m_Player_SwithTppShootState = m_Player.FindAction("SwithTppShootState", throwIfNotFound: true);
        m_Player_Aim = m_Player.FindAction("Aim", throwIfNotFound: true);
        m_Player_Mele = m_Player.FindAction("Mele", throwIfNotFound: true);
        // Camera
        m_Camera = asset.FindActionMap("Camera", throwIfNotFound: true);
        m_Camera_MouseLook = m_Camera.FindAction("MouseLook", throwIfNotFound: true);
        // Panels
        m_Panels = asset.FindActionMap("Panels", throwIfNotFound: true);
        m_Panels_OnOffPausePanel = m_Panels.FindAction("OnOffPausePanel", throwIfNotFound: true);
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
    private readonly InputAction m_Player_Shoot;
    private readonly InputAction m_Player_Dash;
    private readonly InputAction m_Player_SwithTppShootState;
    private readonly InputAction m_Player_Aim;
    private readonly InputAction m_Player_Mele;
    public struct PlayerActions
    {
        private @InputSystemControls m_Wrapper;
        public PlayerActions(@InputSystemControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Player_Movement;
        public InputAction @Shoot => m_Wrapper.m_Player_Shoot;
        public InputAction @Dash => m_Wrapper.m_Player_Dash;
        public InputAction @SwithTppShootState => m_Wrapper.m_Player_SwithTppShootState;
        public InputAction @Aim => m_Wrapper.m_Player_Aim;
        public InputAction @Mele => m_Wrapper.m_Player_Mele;
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
                @Shoot.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShoot;
                @Shoot.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShoot;
                @Shoot.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShoot;
                @Dash.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDash;
                @Dash.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDash;
                @Dash.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDash;
                @SwithTppShootState.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSwithTppShootState;
                @SwithTppShootState.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSwithTppShootState;
                @SwithTppShootState.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSwithTppShootState;
                @Aim.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAim;
                @Aim.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAim;
                @Aim.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAim;
                @Mele.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMele;
                @Mele.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMele;
                @Mele.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMele;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Shoot.started += instance.OnShoot;
                @Shoot.performed += instance.OnShoot;
                @Shoot.canceled += instance.OnShoot;
                @Dash.started += instance.OnDash;
                @Dash.performed += instance.OnDash;
                @Dash.canceled += instance.OnDash;
                @SwithTppShootState.started += instance.OnSwithTppShootState;
                @SwithTppShootState.performed += instance.OnSwithTppShootState;
                @SwithTppShootState.canceled += instance.OnSwithTppShootState;
                @Aim.started += instance.OnAim;
                @Aim.performed += instance.OnAim;
                @Aim.canceled += instance.OnAim;
                @Mele.started += instance.OnMele;
                @Mele.performed += instance.OnMele;
                @Mele.canceled += instance.OnMele;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);

    // Camera
    private readonly InputActionMap m_Camera;
    private ICameraActions m_CameraActionsCallbackInterface;
    private readonly InputAction m_Camera_MouseLook;
    public struct CameraActions
    {
        private @InputSystemControls m_Wrapper;
        public CameraActions(@InputSystemControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @MouseLook => m_Wrapper.m_Camera_MouseLook;
        public InputActionMap Get() { return m_Wrapper.m_Camera; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CameraActions set) { return set.Get(); }
        public void SetCallbacks(ICameraActions instance)
        {
            if (m_Wrapper.m_CameraActionsCallbackInterface != null)
            {
                @MouseLook.started -= m_Wrapper.m_CameraActionsCallbackInterface.OnMouseLook;
                @MouseLook.performed -= m_Wrapper.m_CameraActionsCallbackInterface.OnMouseLook;
                @MouseLook.canceled -= m_Wrapper.m_CameraActionsCallbackInterface.OnMouseLook;
            }
            m_Wrapper.m_CameraActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MouseLook.started += instance.OnMouseLook;
                @MouseLook.performed += instance.OnMouseLook;
                @MouseLook.canceled += instance.OnMouseLook;
            }
        }
    }
    public CameraActions @Camera => new CameraActions(this);

    // Panels
    private readonly InputActionMap m_Panels;
    private IPanelsActions m_PanelsActionsCallbackInterface;
    private readonly InputAction m_Panels_OnOffPausePanel;
    public struct PanelsActions
    {
        private @InputSystemControls m_Wrapper;
        public PanelsActions(@InputSystemControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @OnOffPausePanel => m_Wrapper.m_Panels_OnOffPausePanel;
        public InputActionMap Get() { return m_Wrapper.m_Panels; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PanelsActions set) { return set.Get(); }
        public void SetCallbacks(IPanelsActions instance)
        {
            if (m_Wrapper.m_PanelsActionsCallbackInterface != null)
            {
                @OnOffPausePanel.started -= m_Wrapper.m_PanelsActionsCallbackInterface.OnOnOffPausePanel;
                @OnOffPausePanel.performed -= m_Wrapper.m_PanelsActionsCallbackInterface.OnOnOffPausePanel;
                @OnOffPausePanel.canceled -= m_Wrapper.m_PanelsActionsCallbackInterface.OnOnOffPausePanel;
            }
            m_Wrapper.m_PanelsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @OnOffPausePanel.started += instance.OnOnOffPausePanel;
                @OnOffPausePanel.performed += instance.OnOnOffPausePanel;
                @OnOffPausePanel.canceled += instance.OnOnOffPausePanel;
            }
        }
    }
    public PanelsActions @Panels => new PanelsActions(this);
    private int m_KayboardandmouseSchemeIndex = -1;
    public InputControlScheme KayboardandmouseScheme
    {
        get
        {
            if (m_KayboardandmouseSchemeIndex == -1) m_KayboardandmouseSchemeIndex = asset.FindControlSchemeIndex("Kayboard and mouse");
            return asset.controlSchemes[m_KayboardandmouseSchemeIndex];
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
    public interface IPlayerActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnShoot(InputAction.CallbackContext context);
        void OnDash(InputAction.CallbackContext context);
        void OnSwithTppShootState(InputAction.CallbackContext context);
        void OnAim(InputAction.CallbackContext context);
        void OnMele(InputAction.CallbackContext context);
    }
    public interface ICameraActions
    {
        void OnMouseLook(InputAction.CallbackContext context);
    }
    public interface IPanelsActions
    {
        void OnOnOffPausePanel(InputAction.CallbackContext context);
    }
}
