import { Record, Union } from "../fable_modules/fable-library.3.6.1/Types.js";
import { record_type, lambda_type, unit_type, int32_type, union_type, float64_type } from "../fable_modules/fable-library.3.6.1/Reflection.js";
import { useGlobalKeyDownEvent, useRefState } from "../Hooks/Hooks.fs.js";
import { equals, comparePrimitives, max } from "../fable_modules/fable-library.3.6.1/Util.js";
import { React_createDisposable_3A5B6456, useReact_useEffectOnce_Z5ECA432F } from "../fable_modules/Feliz.1.56.0/React.fs.js";
import { formatTime, splitTime } from "../Utils/TimeUtils.fs.js";
import { createElement } from "react";
import * as react from "react";
import { Styles_roundButton, Styles_buttonsBar, colors } from "../Theme/Theme.fs.js";
import { Interop_reactApi } from "../fable_modules/Feliz.1.56.0/Interop.fs.js";
import { Plus, Minus, Play, Pause, Cancel } from "../Theme/Icons.fs.js";
import { singleton, delay, toList } from "../fable_modules/fable-library.3.6.1/Seq.js";

export class TimerState extends Union {
    constructor(tag, ...fields) {
        super();
        this.tag = (tag | 0);
        this.fields = fields;
    }
    cases() {
        return ["IsRunning", "IsNotRunning"];
    }
}

export function TimerState$reflection() {
    return union_type("Stopwatch.TimerState", [], TimerState, () => [[["Item", float64_type]], []]);
}

export class Props extends Record {
    constructor(startTime, onCancel) {
        super();
        this.startTime = (startTime | 0);
        this.onCancel = onCancel;
    }
}

export function Props$reflection() {
    return record_type("Stopwatch.Props", [], Props, () => [["startTime", int32_type], ["onCancel", lambda_type(unit_type, unit_type)]]);
}

export function Stopwatch(props) {
    const patternInput = useRefState(props.startTime);
    const setElapsedTime = patternInput[1];
    const elapsedTimeRef = patternInput[2];
    const elapsedTime = patternInput[0] | 0;
    const patternInput_1 = useRefState(new TimerState(1));
    const timerStateRef = patternInput_1[2];
    const timerState = patternInput_1[0];
    const setTimerState = patternInput_1[1];
    const addOrSubtractTime = (delta) => {
        setElapsedTime(max((x, y) => comparePrimitives(x, y), 0, elapsedTimeRef.current + delta));
    };
    const startTimer = () => {
        const addOneSecond = () => {
            addOrSubtractTime(1);
        };
        const id = window.setInterval(addOneSecond, 1000);
        setTimerState(new TimerState(0, id));
    };
    const stopTimer = (id_1) => {
        window.clearInterval(id_1);
        setTimerState(new TimerState(1));
    };
    const toggleTimer = () => {
        const matchValue = timerStateRef.current;
        if (matchValue.tag === 1) {
            startTimer();
        }
        else {
            const id_2 = matchValue.fields[0];
            stopTimer(id_2);
        }
    };
    useReact_useEffectOnce_Z5ECA432F(() => {
        startTimer();
        return React_createDisposable_3A5B6456(() => {
            const matchValue_1 = timerStateRef.current;
            if (matchValue_1.tag === 0) {
                const id_3 = matchValue_1.fields[0];
                stopTimer(id_3);
            }
        });
    });
    useGlobalKeyDownEvent((e) => {
        const matchValue_2 = e.key;
        switch (matchValue_2) {
            case " ": {
                toggleTimer();
                e.preventDefault();
                break;
            }
            case "Escape": {
                props.onCancel();
                e.preventDefault();
                break;
            }
            case "-": {
                addOrSubtractTime(-60);
                e.preventDefault();
                break;
            }
            case "+": {
                addOrSubtractTime(60);
                e.preventDefault();
                break;
            }
            default: {
            }
        }
    });
    const patternInput_2 = splitTime(elapsedTime);
    const seconds = patternInput_2[2] | 0;
    const minutes = patternInput_2[1] | 0;
    const hours = patternInput_2[0] | 0;
    return react.createElement(react.Fragment, {}, createElement("div", {
        children: formatTime(hours, minutes, seconds),
        style: {
            color: equals(timerState, new TimerState(1)) ? colors.overlay : colors.subtle,
        },
    }), createElement("div", {
        className: Styles_buttonsBar,
        children: Interop_reactApi.Children.toArray([createElement("button", {
            className: Styles_roundButton,
            onClick: (_arg1) => {
                props.onCancel();
            },
            children: Interop_reactApi.Children.toArray([Cancel()]),
        }), createElement("button", {
            className: Styles_roundButton,
            onClick: (_arg2) => {
                toggleTimer();
            },
            children: Interop_reactApi.Children.toArray(Array.from(toList(delay(() => ((timerState.tag === 0) ? singleton(Pause()) : singleton(Play())))))),
        }), createElement("button", {
            className: Styles_roundButton,
            onClick: (_arg3) => {
                addOrSubtractTime(-60);
            },
            children: Interop_reactApi.Children.toArray([Minus()]),
        }), createElement("button", {
            className: Styles_roundButton,
            onClick: (_arg4) => {
                addOrSubtractTime(60);
            },
            children: Interop_reactApi.Children.toArray([Plus()]),
        })]),
    }));
}

