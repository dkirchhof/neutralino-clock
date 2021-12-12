module Timer

open Emotion
open Feliz

let getU r = 2.0 * System.Math.PI * float r

let getStrokeDashArray u p =
    let fill = p * u
    let gap = u - fill
    
    [| int fill; int gap |]

let rInner = 42 
let uInner = getU rInner

let getStrokeDashArrayInner = getStrokeDashArray uInner

let getSeconds time = time % 60
let getMinutes time = (time / 60) % 60
let getHours time = (time / 3600) % 24

module Styles =
    let svg = css"
        width: 300px;
        height: 300px;

        > g {
            transform: rotate(-90deg);
            transform-origin: center;

            > circle {
                stroke-width: 3;
                fill: none;
            }
        }
    "

    let inner = css"transition: stroke-dasharray 1s linear;"

let innerProps = [
    svg.cx 50
    svg.cy 50
    svg.r rInner
]

type TimerState =
    | IsRunning of float
    | IsNotRunning

[<ReactComponent>]
let Timer () =
    let (startTime, setStartTime) = React.useState 5
    let (remainingTime, setRemainingTime) = React.useStateWithUpdater 5
    let (timerState, setTimerState) = React.useState IsNotRunning

    let decTime () = setRemainingTime(fun time -> time - 1)
    let incTime () = setRemainingTime(fun time -> time + 1)

    let toggleTimer () = 
        match timerState with
        | IsRunning id ->
            Browser.Dom.window.clearInterval id
            setTimerState IsNotRunning
        | IsNotRunning ->
            let id = Browser.Dom.window.setInterval (decTime, 1000)
            setTimerState (IsRunning id)

    let seconds = getSeconds remainingTime
    let minutes = getMinutes remainingTime
    let hours = getHours remainingTime

    let percentage = float remainingTime / float startTime

    React.fragment [
        Svg.svg [
            svg.className Styles.svg
            svg.viewBox (0, 0, 100, 100) 
            svg.children [
                Svg.g [
                    Svg.circle [
                        yield! innerProps
                        svg.stroke Theme.Colors.RosePineMoon.overlay
                    ]
                    Svg.circle [
                        yield! innerProps
                        svg.className Styles.inner
                        svg.stroke Theme.Colors.RosePineMoon.pine
                        svg.strokeDasharray (percentage |> getStrokeDashArrayInner)
                    ]
                ]

                Svg.text [
                    svg.x 50
                    svg.y 50
                    svg.textAnchor.middle
                    svg.dominantBaseline.middle
                    svg.text (sprintf $"%02i{hours}:%02i{minutes}:%02i{seconds}")
                ] 
            ]
        ] 
        
        Html.div [
            Html.button [
                prop.text (if timerState = IsNotRunning then "Start" else "Pause")
                prop.onClick (fun _ -> toggleTimer ())
            ]
            Html.button [
                prop.text "-"
                prop.onClick (fun _ -> decTime ())
            ]
            Html.button [
                prop.text "+"
                prop.onClick (fun _ -> incTime ())
            ]
        ]
    ]
