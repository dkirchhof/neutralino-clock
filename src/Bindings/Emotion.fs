module Emotion

open Fable.Core.JsInterop

let css string : string = importMember "@emotion/css"
let keyframes string : string = importMember "@emotion/css"
