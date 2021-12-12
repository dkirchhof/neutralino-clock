module App

open Emotion
open Feliz

module Colors =
    module RosePineMoon =
        let bg = "#232136"
        let overlay = "#393552"
        let rose = "#EA9A97"
        let pine = "#3E8FB0"

let getU r = 2.0 * System.Math.PI * float r

let getStrokeDashArray u a p =
    if a % 2 = 0 then 
        let fill = p * u
        let gap = u - fill
        
        [| int fill; int gap |]
    else
        let fill = p * u
        let gap = u - fill
        
        [| 0; int fill; int gap; 0 |]

let rInner = 42 
let rOuter = 48

let uInner = getU rInner
let uOuter = getU rOuter 

let getStrokeDashArrayInner = getStrokeDashArray uInner
let getStrokeDashArrayOuter = getStrokeDashArray uOuter

let getPercentageOfMinute seconds = float seconds / 60.0
let getPercentageOfHour minutes = float minutes / 60.0

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
                stroke-linecap: round;
                fill: none;
            }
        }
    "

    let outer = css"transition: stroke-dasharray 1s linear;"
    let inner = css"transition: stroke-dasharray 60s linear;"

let innerProps = [
    svg.cx 50
    svg.cy 50
    svg.r rInner
]

let outerProps = [
    svg.cx 50
    svg.cy 50
    svg.r rOuter
]

[<ReactComponent>]
let GlobalStyles () = 
    React.useEffectOnce (fun () -> 
        let style = Browser.Dom.document.createElement "style"

        style.innerHTML <- sprintf "
            body { 
                background-color: %s; 

                font-family: system-ui;
            }
        " Colors.RosePineMoon.bg

        Browser.Dom.document.head.appendChild style |> ignore
    )

    Html.none

type TimerState =
    | IsRunning of float
    | IsNotRunning

[<ReactComponent>]
let App () =
    let (time, setTime) = React.useStateWithUpdater 3500
    let (timerState, setTimerState) = React.useState IsNotRunning


    (* React.useEffectOnce(fun () -> *)
    (*     let subscriptionId = Browser.Dom.window.setInterval ((fun () -> setTime (fun time -> time + 1)), 100) *)

    (*     React.createDisposable(fun () -> Browser.Dom.window.clearTimeout subscriptionId) *)
    (* ) *)
    
    let decTime () = setTime(fun time -> time - 1)
    let incTime () = setTime(fun time -> time + 1)

    let toggleTimer () = 
        match timerState with
        | IsRunning id ->
            Browser.Dom.window.clearInterval id
            setTimerState IsNotRunning
        | IsNotRunning ->
            let id = Browser.Dom.window.setInterval (incTime, 1000)
            setTimerState (IsRunning id)

    let seconds = getSeconds time
    let minutes = getMinutes time
    let hours = getHours time

    React.fragment [
        GlobalStyles ()

        Svg.svg [
            svg.className Styles.svg
            svg.viewBox (0, 0, 100, 100) 
            svg.children [
                Svg.g [
                    Svg.circle [
                        yield! outerProps
                        svg.stroke Colors.RosePineMoon.overlay
                    ]
                    Svg.circle [
                        yield! outerProps
                        svg.className Styles.outer
                        svg.stroke Colors.RosePineMoon.rose
                        svg.strokeDasharray (seconds |> getPercentageOfMinute |> (getStrokeDashArrayOuter minutes))
                    ]

                    Svg.circle [
                        yield! innerProps
                        svg.stroke Colors.RosePineMoon.overlay
                    ]
                    Svg.circle [
                        yield! innerProps
                        svg.className Styles.inner
                        svg.stroke Colors.RosePineMoon.pine
                        svg.strokeDasharray (minutes |> getPercentageOfHour |> (getStrokeDashArrayInner hours))
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

Feliz.ReactDOM.render (App(), Browser.Dom.document.getElementById ("app"))
