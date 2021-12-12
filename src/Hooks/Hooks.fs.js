import { React_createDisposable_3A5B6456, useReact_useEffectOnce_Z5ECA432F, useReact_useEffect_Z101E1A95, useReact_useRef_1505, useFeliz_React__React_useState_Static_1505 } from "../fable_modules/Feliz.1.56.0/React.fs.js";

export function useRefState(initialValue) {
    const patternInput = useFeliz_React__React_useState_Static_1505(initialValue);
    const state = patternInput[0];
    const setState = patternInput[1];
    const stateRef = useReact_useRef_1505(state);
    useReact_useEffect_Z101E1A95(() => {
        stateRef.current = state;
    }, [state]);
    return [state, setState, stateRef];
}

export function useGlobalKeyDownEvent(callback) {
    useReact_useEffectOnce_Z5ECA432F(() => {
        window.onkeydown = callback;
        return React_createDisposable_3A5B6456(() => {
            window.onkeydown = (void 0);
        });
    });
}

