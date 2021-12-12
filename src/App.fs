module App

open Feliz

[<ReactComponent>]
let GlobalStyles () = 
    React.useEffectOnce (fun () -> 
        let style = Browser.Dom.document.createElement "style"

        style.innerHTML <- $"
            body {{ 
                color: {Theme.colors.subtle};
                background-color: {Theme.colors.bg};

                font-family: system-ui;
                font-size: 26px;
                font-weight: bold;

                text-align: center;
                text-transform: uppercase;
            }}

            #app {{
                position: fixed;
                inset: 0;

                display: flex;
                flex-direction: column;
                align-items: center;
                justify-content: center;
            }}
        " 

        Browser.Dom.document.head.appendChild style |> ignore
    )

    Html.none

type View =
    | ChooseMode
    | ChooseTimeForStopwatch
    | ChooseTimeForTimer
    | Stopwatch of int
    | Timer of int

[<ReactComponent>]
let App () =
    let (view, setView) = React.useState ChooseMode

    let onSelectMode mode = 
        match mode with
        | ModeSelector.Stopwatch -> setView ChooseTimeForStopwatch 
        | ModeSelector.Timer -> setView ChooseTimeForTimer 

    let onCancelChooseTime () = setView ChooseMode
    let onSubmitChooseTimeForStopwatch time = setView (Stopwatch time)
    let onSubmitChooseTimeForTimer time = setView (Timer time)
    let onCancelStopwatch () = setView ChooseTimeForStopwatch
    let onCancelTimer () = setView ChooseTimeForTimer

    React.fragment [
        GlobalStyles ()
        
        match view with
        | ChooseMode -> ModeSelector.ModeSelector { onSelectMode = onSelectMode }
        | ChooseTimeForStopwatch -> TimeInput.TimeInput { validator = (fun _ -> true); onCancel = onCancelChooseTime; onSubmit = onSubmitChooseTimeForStopwatch } 
        | ChooseTimeForTimer -> TimeInput.TimeInput { validator = (fun seconds -> seconds > 0); onCancel = onCancelChooseTime; onSubmit = onSubmitChooseTimeForTimer } 
        | Stopwatch(startTime) -> Stopwatch.Stopwatch { startTime = startTime; onCancel = onCancelStopwatch }
        | Timer(startTime) -> Timer.Timer { startTime = startTime; onCancel = onCancelTimer }
    ]

Feliz.ReactDOM.render (App(), Browser.Dom.document.getElementById ("app"))
