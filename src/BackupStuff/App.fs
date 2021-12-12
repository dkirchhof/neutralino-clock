module App

open Feliz

module Colors =
    module RosePineMoon =
        let bg = "#232136"
        let overlay = "#393552"
        let rose = "#EA9A97"
        let pine = "#3E8FB0"

let css = [
    rule "body" [
        Css.backgroundColor Colors.RosePineMoon.bg
    ]
    rule "svg" [
        Css.transform (transform.rotate -90)
    ]
    rule "circle" [
        Css.custom ("stroke-width", "3")
        Css.custom ("stroke-linecap", "round")
        Css.fill "none"
        Css.transition "stroke-dasharray .2s"
    ]
]

let getU r = 2.0 * System.Math.PI * float r

let getStrokeDashArray u p =
    let fill = p * u
    let gap = u - fill

    sprintf $"{fill} {gap}"

let rInner = 42 
let rOuter = 48

let uInner = getU rInner
let uOuter = getU rOuter 

let getStrokeDashArrayInner = getStrokeDashArray uInner
let getStrokeDashArrayOuter = getStrokeDashArray uOuter

let testInner = getStrokeDashArrayInner 0.25
let testOuter = getStrokeDashArrayOuter 0.5

type Model = { time: int }

type Message = 
    | IncTime

let init () = { time = 0 }

let update msg model = 
    match msg with
    | IncTime -> { model with time = model.time + 1 }

let view () =
    let model, dispatch = () |> Store.makeElmishSimple init update ignore
    
    DOM.fragment [
        Svg.svg [
            Attr.custom ("viewBox", "0 0 100 100")
            Attr.style "width: 300px; height: 300px"

            Svg.circle [
                Attr.cx 50
                Attr.cy 50
                Attr.r rOuter
                Attr.stroke Colors.RosePineMoon.overlay
            ]
            Svg.circle [
                Attr.cx 50
                Attr.cy 50
                Attr.r rOuter
                Attr.stroke Colors.RosePineMoon.rose
                Attr.custom ("stroke-dasharray", testOuter)
            ]

            Svg.circle [
                Attr.cx 50
                Attr.cy 50
                Attr.r rInner
                Attr.stroke Colors.RosePineMoon.overlay
            ]
            Svg.circle [
                Attr.cx 50
                Attr.cy 50
                Attr.r rInner 
                Attr.stroke Colors.RosePineMoon.pine
                Attr.custom ("stroke-dasharray", testInner)
            ]

        ] 

        Html.button [
            Html.text "start"
            onClick (fun _ -> dispatch IncTime) []
        ]
    ]|> withStyle css 

view () |> Program.mountElement "app"
