module TimeInput

open Emotion
open Fable.Core.JsInterop
open Feliz

module Helpers =
    open System.Text.RegularExpressions

    let getParts str = 
        let matches = Regex.Matches(str, "((\d+)([hms]))")

        [ for m in matches -> (int m.Groups[2].Value, m.Groups[3].Value) ]

    let partToSeconds part =
        match part with
        | value, "h" -> value * 3600
        | value, "m" -> value * 60
        | value, "s" -> value
        | _ -> raise (System.FormatException "Wrong Unit")

    let parseTimeInput str = str |> getParts |> List.map partToSeconds |> List.sum

module Styles =
    let input = css $"
        width: 100%%;
        box-sizing: border-box;

        color: {Theme.colors.subtle};
        background: none;
        border: none;
        outline: none;

        font: inherit;
        text-align: center;

        ::placeholder {{
            color: {Theme.colors.overlay};
        }}
    " 

type Props = {
    validator: int -> bool
    onCancel: unit -> unit
    onSubmit: int -> unit
}

[<ReactComponent>]
let TimeInput props =
    let inputRef = React.useRef<Browser.Types.HTMLInputElement option> None

    let submit () =
        let value = inputRef.current.Value.value
        let seconds = Helpers.parseTimeInput value
        let isValid = props.validator seconds

        if isValid then props.onSubmit seconds
    
    Hooks.useGlobalKeyDownEvent(fun e ->
        match e.key with
        | "Enter" -> 
            submit ()
            e.preventDefault()
        | "Escape" -> 
            props.onCancel ()
            e.preventDefault()
        | _ -> ignore ()
    )

    React.fragment [
        Html.input [
            prop.ref inputRef
            prop.className Styles.input
            prop.autoFocus true
            prop.placeholder "10h 20m 40s"
        ]
        Html.div [
            prop.className Theme.Styles.buttonsBar
            prop.children [
                Html.button [
                    prop.className Theme.Styles.roundButton
                    prop.onClick (fun _ -> props.onCancel ())
                    prop.children [Icons.Cancel ()]
                ]
                Html.button [
                    prop.className Theme.Styles.roundButton
                    prop.onClick (fun _ -> submit ())
                    prop.children [Icons.Ok ()]
                ]
            ]
        ]
    ]
