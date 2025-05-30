namespace Oxpecker.Solid.MotionOne

open JetBrains.Annotations
open Fable.Core
open Fable.Core.JsInterop
open Oxpecker.Solid
open Browser.Types

[<AllowNullLiteral>]
[<Interface>]
type DOMRectReadOnly =
    /// <summary>
    /// <a href="https://developer.mozilla.org/docs/Web/API/DOMRectReadOnly/bottom">MDN Reference</a>
    /// </summary>
    abstract member bottom: float with get
    /// <summary>
    /// <a href="https://developer.mozilla.org/docs/Web/API/DOMRectReadOnly/height">MDN Reference</a>
    /// </summary>
    abstract member height: float with get
    /// <summary>
    /// <a href="https://developer.mozilla.org/docs/Web/API/DOMRectReadOnly/left">MDN Reference</a>
    /// </summary>
    abstract member left: float with get
    /// <summary>
    /// <a href="https://developer.mozilla.org/docs/Web/API/DOMRectReadOnly/right">MDN Reference</a>
    /// </summary>
    abstract member right: float with get
    /// <summary>
    /// <a href="https://developer.mozilla.org/docs/Web/API/DOMRectReadOnly/top">MDN Reference</a>
    /// </summary>
    abstract member top: float with get
    /// <summary>
    /// <a href="https://developer.mozilla.org/docs/Web/API/DOMRectReadOnly/width">MDN Reference</a>
    /// </summary>
    abstract member width: float with get
    /// <summary>
    /// <a href="https://developer.mozilla.org/docs/Web/API/DOMRectReadOnly/x">MDN Reference</a>
    /// </summary>
    abstract member x: float with get
    /// <summary>
    /// <a href="https://developer.mozilla.org/docs/Web/API/DOMRectReadOnly/y">MDN Reference</a>
    /// </summary>
    abstract member y: float with get
    abstract member toJSON: unit -> obj

[<AllowNullLiteral; Interface>]
type Generator =
    abstract next: unit -> unit with get

[<Interface; AllowNullLiteral>]
type MotionEvent =
    inherit CustomEvent
    abstract target: obj with get

[<Interface; AllowNullLiteral>]
type CustomPointerEvent =
    inherit CustomEvent
    abstract originalEvent: PointerEvent with get

[<Interface; AllowNullLiteral>]
type ViewEvent =
    inherit CustomEvent
    abstract originalEntry: IntersectionObserverEntry with get

[<Erase>]
type OptionKeys = interface end

[<Erase>]
type AttrKey = interface end

[<Erase>]
type MotionEvents = interface end

[<AllowNullLiteral>]
[<Interface>]
type MotionStateContext =
    abstract member initial: string option with get, set
    abstract member animate: string option with get, set
    abstract member inView: string option with get, set
    abstract member hover: string option with get, set
    abstract member press: string option with get, set
    abstract member exit: string option with get, set

module MotionState =

    [<RequireQualifiedAccess>]
    [<StringEnum>]
    type Type =
        | Initial
        | Animate
        | InView
        | Hover
        | Press
        | Exit

    type SetActive = delegate of ``type``: Type * isActive: bool -> unit

[<AllowNullLiteral>]
[<Interface>]
type MotionState =
    abstract member update: (obj -> unit) with get, set
    abstract member getDepth: (unit -> float) with get, set
    abstract member getTarget: (unit -> obj) with get, set
    abstract member getOptions: (unit -> obj) with get, set
    abstract member getContext: (unit -> MotionStateContext) with get, set
    abstract member setActive: MotionState.SetActive with get, set
    abstract member mount: (Element -> unit -> unit) with get, set
    abstract member isMounted: (unit -> bool) with get, set
    abstract member animateUpdates: (unit -> Generator) with get, set


[<Import("Presence", "solid-motionone")>]
type Presence() =
    interface HtmlContainer

    [<Erase>]
    member _.exitBeforeEnter
        with set (_: bool) = ()

    [<Erase>]
    member _.initial
        with set (_: bool) = ()

/// <summary>
/// See the solid-motionone docs for usage.
/// </summary>
/// <remarks>You can implement native Motion elements by proxy: <code>
/// module Motion
/// [Import("Motion.div","solid-motionone")>]
/// type button() =
///     inherit Tags.button()
///     interface OptionKeys
///     interface MotionEvents
/// </code></remarks>
[<Import("Motion", "solid-motionone")>]
type Motion() =
    interface RegularNode
    interface OptionKeys
    interface MotionEvents
    interface AttrKey

[<AbstractClass>]
[<Erase; AutoOpen>]
type Exports =
    /// <summary>
    /// createMotion provides MotionOne as a compact Solid primitive.
    /// </summary>
    /// <param name="target">
    /// Target Element to animate.
    /// </param>
    /// <param name="options">
    /// Options to effect the animation.
    /// </param>
    /// <param name="presenceState">
    /// Optional PresenceContext override, defaults to current parent.
    /// </param>
    /// <returns>
    /// Object to access MotionState
    /// </returns>
    [<Import("createMotion", "solid-motionone")>]
    static member createMotion(target: HtmlElement, options: obj, ?presenceState: obj) : MotionState = nativeOnly

[<Erase; AutoOpen>]
module Extensions =
    type AttrKey with
        /// <summary>
        /// The native HTML tag that the motion component mirrors
        /// </summary>
        [<Erase>]
        [<LanguageInjection(InjectedLanguage.HTML, Prefix = "<", Suffix = ">")>]
        member _.tag

            with set (_: string) = ()

    type OptionKeys with
        [<Erase>]
        member _.initial'
            with set (_: obj) = ()

        [<Erase>]
        member _.animate'
            with set (_: obj) = ()

        [<Erase>]
        member _.inView'
            with set (_: obj) = ()

        [<Erase>]
        member _.inViewOptions'
            with set (_: obj) = ()

        [<Erase>]
        member _.hover'
            with set (_: obj) = ()

        [<Erase>]
        member _.press'
            with set (_: obj) = ()

        [<Erase>]
        member _.variants'
            with set (_: obj) = ()

        [<Erase>]
        member _.transition'
            with set (_: obj) = ()

        [<Erase>]
        member _.exit'
            with set (_: obj) = ()

        [<LanguageInjection(InjectedLanguage.JAVASCRIPT, Prefix = "<div style=", Suffix = " />")>]
        member inline this.initial
            with inline set (value: string) = this.initial' <- emitJsExpr () value

        [<LanguageInjection(InjectedLanguage.JAVASCRIPT, Prefix = "<div style=", Suffix = " />")>]
        member inline this.animate
            with inline set (value: string) = this.animate' <- emitJsExpr () value

        [<LanguageInjection(InjectedLanguage.JAVASCRIPT, Prefix = "<div style=", Suffix = " />")>]
        member inline this.inView
            with inline set (value: string) = this.inView' <- emitJsExpr () value

        [<LanguageInjection(InjectedLanguage.JAVASCRIPT, Prefix = "<div style=", Suffix = " />")>]
        member inline this.inViewOptions
            with inline set (value: string) = this.inViewOptions' <- emitJsExpr () value

        [<LanguageInjection(InjectedLanguage.JAVASCRIPT, Prefix = "<div style=", Suffix = " />")>]
        member inline this.hover
            with inline set (value: string) = this.hover' <- emitJsExpr () value

        [<LanguageInjection(InjectedLanguage.JAVASCRIPT, Prefix = "<div style=", Suffix = " />")>]
        member inline this.press
            with inline set (value: string) = this.press' <- emitJsExpr () value

        [<LanguageInjection(InjectedLanguage.JAVASCRIPT, Prefix = "<div style=", Suffix = " />")>]
        member inline this.variants
            with inline set (value: string) = this.variants' <- emitJsExpr () value

        [<LanguageInjection(InjectedLanguage.JAVASCRIPT, Prefix = "<div style=", Suffix = " />")>]
        member inline this.transition
            with inline set (value: string) = this.transition' <- emitJsExpr () value

        [<LanguageInjection(InjectedLanguage.JAVASCRIPT, Prefix = "<div style=", Suffix = " />")>]
        member inline this.exit
            with inline set (value: string) = this.exit' <- emitJsExpr () value

    type MotionEvents with
        member _.onMotionStart
            with set (handler: MotionEvent -> unit) = ()

        member _.onMotionComplete
            with set (handler: MotionEvent -> unit) = ()

        member _.onHoverStart
            with set (handler: CustomPointerEvent -> unit) = ()

        member _.onHoverEnd
            with set (handler: CustomPointerEvent -> unit) = ()

        member _.onPressStart
            with set (handler: CustomPointerEvent -> unit) = ()

        member _.onPressEnd
            with set (handler: CustomPointerEvent -> unit) = ()

        member _.onViewEnter
            with set (handler: ViewEvent -> unit) = ()

        member _.onViewLeave
            with set (handler: ViewEvent -> unit) = ()
