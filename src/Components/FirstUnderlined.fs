module FirstUnderlined

open Emotion
open Feliz

module Styles =
    let first = css "text-decoration: underline"

[<ReactComponent>]
let FirstUnderlined (str: string) =
    React.fragment [
        Html.span [
            prop.className Styles.first
            prop.text (str.Substring(0, 1))
        ]
        Html.span (str.Substring(1))
    ]    
