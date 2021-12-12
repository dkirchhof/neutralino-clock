module Hooks

open Feliz

let useRefState (initialValue: 'a) =
    let (state, setState) = React.useState initialValue

    let stateRef = React.useRef state

    React.useEffect(fun () ->
        stateRef.current <- state
    , [| box state |])
    
    (state, setState, stateRef)
    
let useGlobalKeyDownEvent callback =
    React.useEffectOnce(fun () ->
        Browser.Dom.window.onkeydown <- callback

        React.createDisposable(fun () -> Browser.Dom.window.onkeydown <- Fable.Core.JS.undefined)
    )
