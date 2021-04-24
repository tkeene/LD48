// GENERATED AUTOMATICALLY FROM 'Assets/Controls.inputactions'

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
            ""id"": ""9710f1a0-8466-44f1-803c-a9acfd09e089"",
            ""actions"": [
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""b1a865e6-978d-4229-a912-434372cadf02"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Dodge"",
                    ""type"": ""Button"",
                    ""id"": ""73036166-2af9-4294-8da9-21bdf6ddeaac"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Crashdown"",
                    ""type"": ""Button"",
                    ""id"": ""7238751e-c1d1-459a-b904-69e15b597d41"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""243e3c92-57d6-4996-a9f2-4327e6ffba42"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""b21714c0-0696-4b1a-b0fb-582249713160"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""f2b75dae-8c7e-474f-bcb0-c97215d5b076"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c9077890-b91d-4e2e-9932-feb3d00948c9"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d5166624-ad34-4fcd-a2af-e7661cd1a731"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dodge"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6aa9ee65-c17a-401c-b9a5-6e54695c13ea"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dodge"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7a2e0f5f-ffea-4283-b0f7-77350485f292"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Crashdown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d7b29a72-82a1-47d4-9a03-80ac4b6acd1b"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Crashdown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3c82a316-e7a8-426e-b785-3ec06bce4ef0"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""be122094-c1e2-4a8f-99b3-717176226b56"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""17447cf7-750c-468e-98cd-0f0641975580"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": ""StickDeadzone"",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""89aa05be-a465-4547-86ff-27f45ec7f317"",
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
                    ""id"": ""eff07c46-bfee-49eb-92d5-9d6d5cf2ade6"",
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
                    ""id"": ""fb422214-1cb2-46a5-aaa9-028b9f47bbce"",
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
                    ""id"": ""b70eda2e-80f9-4af7-a724-34474cb76f7b"",
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
                    ""id"": ""c3245aa4-6ff0-472e-a3ef-3570a054a530"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        },
        {
            ""name"": ""Test"",
            ""id"": ""8ec1929d-5046-494e-a3a8-e4fecde99a56"",
            ""actions"": [
                {
                    ""name"": ""Add Max Health"",
                    ""type"": ""Button"",
                    ""id"": ""7c632310-2b07-4043-b9f1-f28591b6e2bf"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Damage Player"",
                    ""type"": ""Button"",
                    ""id"": ""a3411f17-f1b2-4a80-8a64-2f474096da78"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""50174659-b7fc-4771-8955-7041439ab0b9"",
                    ""path"": ""<Keyboard>/m"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Add Max Health"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""514d7cc0-3308-4d0f-98a5-dcdae188dcc1"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Damage Player"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Attack = m_Player.FindAction("Attack", throwIfNotFound: true);
        m_Player_Dodge = m_Player.FindAction("Dodge", throwIfNotFound: true);
        m_Player_Crashdown = m_Player.FindAction("Crashdown", throwIfNotFound: true);
        m_Player_Interact = m_Player.FindAction("Interact", throwIfNotFound: true);
        m_Player_Move = m_Player.FindAction("Move", throwIfNotFound: true);
        // Test
        m_Test = asset.FindActionMap("Test", throwIfNotFound: true);
        m_Test_AddMaxHealth = m_Test.FindAction("Add Max Health", throwIfNotFound: true);
        m_Test_DamagePlayer = m_Test.FindAction("Damage Player", throwIfNotFound: true);
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
    private readonly InputAction m_Player_Attack;
    private readonly InputAction m_Player_Dodge;
    private readonly InputAction m_Player_Crashdown;
    private readonly InputAction m_Player_Interact;
    private readonly InputAction m_Player_Move;
    public struct PlayerActions
    {
        private @Controls m_Wrapper;
        public PlayerActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Attack => m_Wrapper.m_Player_Attack;
        public InputAction @Dodge => m_Wrapper.m_Player_Dodge;
        public InputAction @Crashdown => m_Wrapper.m_Player_Crashdown;
        public InputAction @Interact => m_Wrapper.m_Player_Interact;
        public InputAction @Move => m_Wrapper.m_Player_Move;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Attack.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttack;
                @Attack.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttack;
                @Attack.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttack;
                @Dodge.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDodge;
                @Dodge.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDodge;
                @Dodge.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDodge;
                @Crashdown.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCrashdown;
                @Crashdown.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCrashdown;
                @Crashdown.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCrashdown;
                @Interact.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @Move.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Attack.started += instance.OnAttack;
                @Attack.performed += instance.OnAttack;
                @Attack.canceled += instance.OnAttack;
                @Dodge.started += instance.OnDodge;
                @Dodge.performed += instance.OnDodge;
                @Dodge.canceled += instance.OnDodge;
                @Crashdown.started += instance.OnCrashdown;
                @Crashdown.performed += instance.OnCrashdown;
                @Crashdown.canceled += instance.OnCrashdown;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);

    // Test
    private readonly InputActionMap m_Test;
    private ITestActions m_TestActionsCallbackInterface;
    private readonly InputAction m_Test_AddMaxHealth;
    private readonly InputAction m_Test_DamagePlayer;
    public struct TestActions
    {
        private @Controls m_Wrapper;
        public TestActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @AddMaxHealth => m_Wrapper.m_Test_AddMaxHealth;
        public InputAction @DamagePlayer => m_Wrapper.m_Test_DamagePlayer;
        public InputActionMap Get() { return m_Wrapper.m_Test; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(TestActions set) { return set.Get(); }
        public void SetCallbacks(ITestActions instance)
        {
            if (m_Wrapper.m_TestActionsCallbackInterface != null)
            {
                @AddMaxHealth.started -= m_Wrapper.m_TestActionsCallbackInterface.OnAddMaxHealth;
                @AddMaxHealth.performed -= m_Wrapper.m_TestActionsCallbackInterface.OnAddMaxHealth;
                @AddMaxHealth.canceled -= m_Wrapper.m_TestActionsCallbackInterface.OnAddMaxHealth;
                @DamagePlayer.started -= m_Wrapper.m_TestActionsCallbackInterface.OnDamagePlayer;
                @DamagePlayer.performed -= m_Wrapper.m_TestActionsCallbackInterface.OnDamagePlayer;
                @DamagePlayer.canceled -= m_Wrapper.m_TestActionsCallbackInterface.OnDamagePlayer;
            }
            m_Wrapper.m_TestActionsCallbackInterface = instance;
            if (instance != null)
            {
                @AddMaxHealth.started += instance.OnAddMaxHealth;
                @AddMaxHealth.performed += instance.OnAddMaxHealth;
                @AddMaxHealth.canceled += instance.OnAddMaxHealth;
                @DamagePlayer.started += instance.OnDamagePlayer;
                @DamagePlayer.performed += instance.OnDamagePlayer;
                @DamagePlayer.canceled += instance.OnDamagePlayer;
            }
        }
    }
    public TestActions @Test => new TestActions(this);
    public interface IPlayerActions
    {
        void OnAttack(InputAction.CallbackContext context);
        void OnDodge(InputAction.CallbackContext context);
        void OnCrashdown(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
    }
    public interface ITestActions
    {
        void OnAddMaxHealth(InputAction.CallbackContext context);
        void OnDamagePlayer(InputAction.CallbackContext context);
    }
}
