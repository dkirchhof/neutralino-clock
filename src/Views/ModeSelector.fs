module ModeSelector

open Emotion
open Feliz

module Styles =
    let button = css $"
        color: inherit;
        background: none;
        border: none;
        outline: none;

        font: inherit;
        text-transform: uppercase;
        text-align: center;

        cursor: pointer;

        :hover, :focus {{
            background: {Theme.colors.overlay};
        }}
    " 

type Mode = Stopwatch | Timer

type Props = { onSelectMode: Mode -> unit }

[<ReactComponent>]
let ModeSelector props =
    Hooks.useGlobalKeyDownEvent(fun e ->
        match e.key with
        | "c" -> 
            props.onSelectMode Timer
            e.preventDefault()
        | "s" -> 
            props.onSelectMode Stopwatch
            e.preventDefault()
        | _ -> ignore ()
    )

    React.fragment [
        Html.button [
            prop.className Styles.button
            prop.onClick (fun _ -> props.onSelectMode Stopwatch )
            prop.children (FirstUnderlined.FirstUnderlined "Stopwatch")
        ]
        Html.button [
            prop.className Styles.button
            prop.onClick (fun _ -> props.onSelectMode Timer )
            prop.children (FirstUnderlined.FirstUnderlined "Countdown")
        ]
    ]
