using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; // Required when using event data.
#if UNITY_EDITOR
using UnityEditor;
#endif

#if UNITY_EDITOR
/// <summary>Inspector for the CanvasController component.</summary>
[CustomEditor(typeof(UIFade))]
[ExecuteInEditMode]
public class UIFadeEditor : Editor
{
    //Fetches or creates the CanvasGroup on enable
    private void OnEnable()
    {
        UIFade Controller = (UIFade)target;
        Controller.SetCanvasGroup();
    }
}
#endif

/// <summary>Component that provides fade in/out functionality for a canvas group.</summary>
[DisallowMultipleComponent]
[RequireComponent(typeof(CanvasGroup))]
public class UIFade : MonoBehaviour, IPointerDownHandler
{
    //Private Variables
    /// <summary>Default speed for fade in and fade out.</summary>
    [Header("Fade Setting")]
    [Tooltip("Default speed for fade in and fade out.")]
    public float DefaultSpeed = 0.4f;

    /// <summary>Automatically fade on activating the GameObject.</summary>
    [Tooltip("Automatically fade on activating the GameObject.")]
    public bool AutoFade = true;

    /// <summary>Blocks raycasts whilst fading.</summary>
    [Tooltip("Blocks raycasts whilst fading.")]
    public bool BlockRaycasts = true;

    //Private Variables
    /// <summary>The CanvasGroup to fade in and out.</summary>
    private CanvasGroup Elements;

    /// <summary>Whether or not the CanvasController is initialised.</summary>
    private bool Initialised = false;

    //Initialises
    private void Awake() { Initialise(); }

    /// <summary>Initialises the CanvasController.</summary>
    public void Initialise() { Initialise(false); }

    /// <summary>Sets the CanvasGroup of this CanvasController</summary>
    public void SetCanvasGroup()
    {
        if (Elements == null) { Elements = GetComponent<CanvasGroup>(); }
        if (Elements == null) { Elements = gameObject.AddComponent<CanvasGroup>(); }
    }

    /// <summary>Initialises the CanvasController.</summary>
    /// <param name="Override">Re-initialises even if already initialised.</param>
    public void Initialise(bool Override)
    {
        if (!Initialised || Override)
        {
            SetCanvasGroup();
            Initialised = true;
        }
    }

    RectTransform panelRectTransform;

    void Start()
    {
        panelRectTransform = GetComponent<RectTransform>();
        panelRectTransform.SetAsLastSibling();
    }

    public void InvokeAlwaysOnTop()
    {
        panelRectTransform.SetAsLastSibling();
    }

    //Invoked when the mouse pointer goes down on a UI element.
    public void OnPointerDown(PointerEventData data)
    {
        // Puts the panel to the front as it is now the last UI element to be drawn.
        //panelRectTransform.SetAsLastSibling();
    }

    public void Hide() { FadeOut(DefaultSpeed); }

    /// <summary>Fades out.</summary>
    public void FadeOut() { FadeOut(DefaultSpeed); }
    /// <summary>Fades out.</summary>
    /// <param name="Duration">Duration of the fade.</param>
    public void FadeOut(float Duration)
    {
        if (gameObject.activeInHierarchy)
        {
            //Stops any other fading
            StopAllCoroutines();

            //Begins Fade
            StartCoroutine(FadeOutComponents(Duration));
        }
    }
    /// <summary>Fades out.</summary>
    /// <param name="Duration">Duration of the fade.</param>
    IEnumerator FadeOutComponents(float Duration)
    {
        //Blocks raycasting
        bool WasRaycaster = false;
        if (BlockRaycasts)
        {
            WasRaycaster = Elements.blocksRaycasts;
            Elements.blocksRaycasts = false;
        }

        //Fades CanvasGroup
        Elements.alpha = 1f;
        while (Elements.alpha > 0)
        {
            if (Elements.gameObject.activeInHierarchy) { Elements.alpha -= Time.unscaledDeltaTime / Duration; }
            yield return null;
        }
        Elements.alpha = 1f;

        //Finishes fade
        if (BlockRaycasts && WasRaycaster) { Elements.blocksRaycasts = true; }
        gameObject.SetActive(false);
    }

    public void Show() { FadeIn(DefaultSpeed); }
    /// <summary>Fades in.</summary>
    public void FadeIn() { FadeIn(DefaultSpeed); }
    /// <summary>Fades in.</summary>
    /// <param name="Duration">Duration of the fade.</param>
    public void FadeIn(float Length)
    {
        if (gameObject.activeInHierarchy)
        {
            //Stops any other fading
            if (!Initialised) { Initialise(); }
            StopAllCoroutines();

            //Begins Fade
            StartCoroutine(FadeInComponents(Length));
        }
    }
    /// <summary>Fades in.</summary>
    /// <param name="Duration">Duration of the fade.</param>
    IEnumerator FadeInComponents(float Duration)
    {
        //Blocks raycasting
        bool WasRaycaster = false;
        if (BlockRaycasts)
        {
            WasRaycaster = Elements.blocksRaycasts;
            Elements.blocksRaycasts = false;
        }

        //Fades CanvasGroup
        Elements.alpha = 0f;
        while (Elements.alpha < 1f)
        {
            if (Elements.gameObject.activeInHierarchy) { Elements.alpha += Time.unscaledDeltaTime / Duration; }
            yield return null;
        }
        Elements.alpha = 1f;

        //Finishes fade
        if (BlockRaycasts && WasRaycaster) { Elements.blocksRaycasts = true; }
    }

    //Auto fade in
    void OnEnable() { if (AutoFade) { FadeIn(); } }
}