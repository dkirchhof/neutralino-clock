module Icons

open Feliz

let Cancel () =
    Svg.svg [
        svg.viewBox (0, 0, 40, 40)
        svg.children [
            Svg.line [
                svg.x1 14
                svg.y1 14
                svg.x2 26
                svg.y2 26
                svg.strokeWidth 4
                svg.stroke "currentColor"
            ]
            Svg.line [
                svg.x1 14
                svg.y1 26
                svg.x2 26
                svg.y2 14
                svg.strokeWidth 4
                svg.stroke "currentColor"
            ]
        ]
    ]

let Minus () =
    Svg.svg [
        svg.viewBox (0, 0, 40, 40)
        svg.children [
            Svg.rect [
                svg.x 13 
                svg.y 18 
                svg.width 14 
                svg.height 4 
                svg.fill "currentColor"
            ]
        ]
    ]

let Ok () =
    Svg.svg [
        svg.viewBox (0, 0, 40, 40)
        svg.children [
            Svg.path [
                svg.d " M 12.317 21.514 L 17.643 25.983 L 27.683 14.017" 
                svg.fill "none"
                svg.strokeWidth 4
                svg.stroke "currentColor"
            ]
        ]
    ]

let Pause () =
    Svg.svg [
        svg.viewBox (0, 0, 40, 40)
        svg.children [
            Svg.rect [
                svg.x 12
                svg.y 12
                svg.width 6 
                svg.height 16 
                svg.fill "currentColor"
            ]
            Svg.rect [
                svg.x 22 
                svg.y 12
                svg.width 6 
                svg.height 16 
                svg.fill "currentColor"
            ]
        ]
    ]

let Play () =
    Svg.svg [
        svg.viewBox (0, 0, 40, 40)
        svg.children [
            Svg.polygon [
                svg.points "28,20,15,28,15,12"
                svg.x 12
                svg.y 12
                svg.width 6 
                svg.height 16 
                svg.fill "currentColor"
            ]
        ]
    ]

let Plus () =
    Svg.svg [
        svg.viewBox (0, 0, 40, 40)
        svg.children [
            Svg.rect [
                svg.x 18 
                svg.y 12
                svg.width 4 
                svg.height 16 
                svg.fill "currentColor"
            ]
            Svg.rect [
                svg.x 12
                svg.y 18 
                svg.width 16 
                svg.height 4 
                svg.fill "currentColor"
            ]
        ]
    ]
