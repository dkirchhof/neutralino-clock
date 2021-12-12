module TimeInput

open Emotion
open Feliz

// todo: remove when updateAt is available in fable
let updateAt index value array =
    let before = Array.take index array
    let after = Array.skip (index + 1) array

    Array.concat [before; [|value|]; after]

let keyToDigit (key: string) =
    let r = System.Int16.TryParse key

    match r with
    | (true, value) -> Some (int value)
    | _ -> None

module Styles =
    let container = css "
        display: grid
    " 

type FieldProps = { active: bool; value: int; }

[<ReactComponent>]
let Field props =
    Html.span [
        prop.text props.value
        prop.style [
            style.textDecoration (if props.active then textDecorationLine.underline else textDecorationLine.none)
        ]
    ]

[<ReactComponent>]
let TimeInput () =
    let (time, setTime, timeRef) = Hooks.useRefState (Array.create 6 0)
    let (index, setIndex, indexRef) = Hooks.useRefState 0

    React.useEffectOnce(fun () ->
        let onKeyDown (e: Browser.Types.KeyboardEvent) =
            if e.key = "Backspace" then
                e.preventDefault ()

                if indexRef.current > 0 then
                    let newTime = updateAt (indexRef.current - 1) 0 timeRef.current

                    setTime newTime
                    setIndex (indexRef.current - 1)
            else
                let createNewTime digit = 
                    updateAt indexRef.current digit timeRef.current

                let updateState newTime =
                    setTime newTime
                    setIndex (indexRef.current + 1)

                keyToDigit e.key
                |> Option.map createNewTime
                |> Option.iter updateState 

        Browser.Dom.window.onkeydown <- onKeyDown
    )
    
    React.fragment [
        Html.div [
            prop.style [
                style.color "white"
            ]

            prop.children [
                Field { active = index = 0; value = time[0] }
                Field { active = index = 1; value = time[1] }
                Html.span ":"
                Field { active = index = 2; value = time[2] }
                Field { active = index = 3; value = time[3] }
                Html.span ":"
                Field { active = index = 4; value = time[4] }
                Field { active = index = 5; value = time[5] }
            ]

            (* prop.children ( *)
            (*     time |> Array.mapi (fun i value -> Field { active = i = index; value = value }) *)
            (* ) *)
        ]
    ]
