import { matches as matches_1 } from "../fable_modules/fable-library.3.6.1/RegExp.js";
import { map, delay, toList } from "../fable_modules/fable-library.3.6.1/Seq.js";
import { parse } from "../fable_modules/fable-library.3.6.1/Int32.js";
import { map as map_1, sum } from "../fable_modules/fable-library.3.6.1/List.js";
import { css } from "../Bindings/Emotion.fs.js";
import { Styles_roundButton, Styles_buttonsBar, colors } from "../Theme/Theme.fs.js";
import { Record } from "../fable_modules/fable-library.3.6.1/Types.js";
import { record_type, unit_type, lambda_type, bool_type, int32_type } from "../fable_modules/fable-library.3.6.1/Reflection.js";
import { useReact_useRef_1505 } from "../fable_modules/Feliz.1.56.0/React.fs.js";
import { value as value_19 } from "../fable_modules/fable-library.3.6.1/Option.js";
import { useGlobalKeyDownEvent } from "../Hooks/Hooks.fs.js";
import { createElement } from "react";
import * as react from "react";
import { Interop_reactApi } from "../fable_modules/Feliz.1.56.0/Interop.fs.js";
import { Ok, Cancel } from "../Theme/Icons.fs.js";

export function Helpers_getParts(str) {
    const matches = matches_1(/((\d+)([hms]))/g, str);
    return toList(delay(() => map((m) => [parse(m[2] || "", 511, false, 32), m[3] || ""], matches)));
}

export function Helpers_partToSeconds(part_0, part_1) {
    const part = [part_0, part_1];
    if (part[1] === "h") {
        const value = part[0] | 0;
        return (value * 3600) | 0;
    }
    else if (part[1] === "m") {
        const value_1 = part[0] | 0;
        return (value_1 * 60) | 0;
    }
    else if (part[1] === "s") {
        const value_2 = part[0] | 0;
        return value_2 | 0;
    }
    else {
        throw (new Error("Wrong Unit"));
    }
}

export function Helpers_parseTimeInput(str) {
    return sum(map_1((tupledArg) => Helpers_partToSeconds(tupledArg[0], tupledArg[1]), Helpers_getParts(str)), {
        GetZero: () => 0,
        Add: (x, y) => (x + y),
    });
}

export const Styles_input = css(`
width: 100%;
box-sizing: border-box;
color: ${colors.subtle};
background: none;
border: none;
outline: none;
font: inherit;
text-align: center;
::placeholder {
color: ${colors.overlay};
}
`);

export class Props extends Record {
    constructor(validator, onCancel, onSubmit) {
        super();
        this.validator = validator;
        this.onCancel = onCancel;
        this.onSubmit = onSubmit;
    }
}

export function Props$reflection() {
    return record_type("TimeInput.Props", [], Props, () => [["validator", lambda_type(int32_type, bool_type)], ["onCancel", lambda_type(unit_type, unit_type)], ["onSubmit", lambda_type(int32_type, unit_type)]]);
}

export function TimeInput(props) {
    const inputRef = useReact_useRef_1505(void 0);
    const submit = () => {
        const value = value_19(inputRef.current).value;
        const seconds = Helpers_parseTimeInput(value) | 0;
        const isValid = props.validator(seconds);
        if (isValid) {
            props.onSubmit(seconds);
        }
    };
    useGlobalKeyDownEvent((e) => {
        const matchValue = e.key;
        switch (matchValue) {
            case "Enter": {
                submit();
                e.preventDefault();
                break;
            }
            case "Escape": {
                props.onCancel();
                e.preventDefault();
                break;
            }
            default: {
            }
        }
    });
    return react.createElement(react.Fragment, {}, createElement("input", {
        ref: inputRef,
        className: Styles_input,
        autoFocus: true,
        placeholder: "10h 20m 40s",
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
                submit();
            },
            children: Interop_reactApi.Children.toArray([Ok()]),
        })]),
    }));
}

