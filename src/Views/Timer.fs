module Timer

open Feliz

type TimerState =
    | IsRunning of float
    | IsNotRunning

type Props = { 
    startTime: int 

    onCancel: unit -> unit
}

[<ReactComponent>]
let Timer props =
    let audioPlayer = React.useRef<Browser.Types.HTMLAudioElement option> None

    let (remainingTime, setRemainingTime, remainingTimeRef) = Hooks.useRefState props.startTime
    let (timerState, setTimerState, timerStateRef) = Hooks.useRefState IsNotRunning

    let addOrSubtractTime delta = setRemainingTime(System.Math.Max(0, remainingTimeRef.current + delta))

    let startTimer () =
        if remainingTimeRef.current > 0 then
            let subtractOneSecond () = addOrSubtractTime -1
            let id = Browser.Dom.window.setInterval (subtractOneSecond, 1000)
            setTimerState (IsRunning id)

    let stopTimer id =
        Browser.Dom.window.clearInterval id
        setTimerState IsNotRunning

    let toggleTimer () = 
        match timerStateRef.current with
        | IsRunning id -> stopTimer id
        | IsNotRunning -> startTimer ()

    React.useEffectOnce (fun () ->
        startTimer ()

        React.createDisposable(fun () -> 
            match timerStateRef.current with
            | IsRunning id -> stopTimer id
            | _ -> ignore ()
        )
    )

    React.useEffect(fun () ->
        match timerStateRef.current, remainingTime with
        | IsRunning id, 0 -> 
            audioPlayer.current.Value.play()
            stopTimer id
        | _ -> ignore ()
    , [| box remainingTime |])

    Hooks.useGlobalKeyDownEvent(fun e ->
        match e.key with
        | " " -> 
            toggleTimer ()
            e.preventDefault()
        | "Escape" -> 
            props.onCancel ()
            e.preventDefault()
        | "-" -> 
            addOrSubtractTime -60
            e.preventDefault()
        | "+" -> 
            addOrSubtractTime 60
            e.preventDefault()
        | _ -> ignore ()
    )

    let (hours, minutes, seconds) = TimeUtils.splitTime remainingTime

    React.fragment [
        Html.audio [
            prop.ref audioPlayer
            prop.src "./audio/mixkit-bell-notification-933.wav"
        ]

        Html.div [
            prop.text (TimeUtils.formatTime hours minutes seconds)
            prop.style [
                style.color (if timerState = IsNotRunning then Theme.colors.overlay else Theme.colors.subtle)
            ]
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
                    prop.onClick (fun _ -> toggleTimer ())
                    prop.children [
                        match timerState with
                        | IsNotRunning -> Icons.Play ()
                        | IsRunning _ -> Icons.Pause ()
                    ]
                ]
                Html.button [
                    prop.className Theme.Styles.roundButton
                    prop.onClick (fun _ -> addOrSubtractTime -60)
                    prop.children [Icons.Minus ()]
                ]
                Html.button [
                    prop.className Theme.Styles.roundButton
                    prop.onClick (fun _ -> addOrSubtractTime 60)
                    prop.children [Icons.Plus ()]
                ]
            ]
        ]
    ]
