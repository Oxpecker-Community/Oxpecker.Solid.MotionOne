module App

open Oxpecker.Solid
open Oxpecker.Solid.MotionOne

[<SolidComponent>]
let FlipText (text: string) =
    div(style = "display: flex") {
        For(each = (text.ToCharArray() |> Array.map string)) {
            yield fun letter index ->
                Motion(
                    tag = "h1",
                    style = "transform-origin: bottom",
                    animate = "{ rotateX: [0, 90, 180, 270, 360, 360], opacity: 1 }",
                    transition = "{ duration: 2, offset: [0, 0.2, 0.3, 0.5, 0.8, 1], repeat: Infinity, easing: 'ease-in-out' }"
                    ) { if letter = " " then "\u00a0" else letter }
        }
    }

[<SolidComponent>]
let App() =
    h1() {
        FlipText "Hello world!"
    }
