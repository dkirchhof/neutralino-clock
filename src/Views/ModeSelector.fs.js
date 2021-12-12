import { css } from "../Bindings/Emotion.fs.js";
import { colors } from "../Theme/Theme.fs.js";
import { Record, Union } from "../fable_modules/fable-library.3.6.1/Types.js";
import { record_type, lambda_type, unit_type, union_type } from "../fable_modules/fable-library.3.6.1/Reflection.js";
import { useGlobalKeyDownEvent } from "../Hooks/Hooks.fs.js";
import { createElement } from "react";
import * as react from "react";
import { FirstUnderlined } from "../Components/FirstUnderlined.fs.js";

export const Styles_button = css(`
color: inherit;
background: none;
border: none;
outline: none;
font: inherit;
text-transform: uppercase;
text-align: center;
cursor: pointer;
:hover, :focus {
background: ${colors.overlay};
}
`);

export class Mode extends Union {
    constructor(tag, ...fields) {
        super();
        this.tag = (tag | 0);
        this.fields = fields;
    }
    cases() {
        return ["Stopwatch", "Timer"];
    }
}

export function Mode$reflection() {
    return union_type("ModeSelector.Mode", [], Mode, () => [[], []]);
}

export class Props extends Record {
    constructor(onSelectMode) {
        super();
        this.onSelectMode = onSelectMode;
    }
}

export function Props$reflection() {
    return record_type("ModeSelector.Props", [], Props, () => [["onSelectMode", lambda_type(Mode$reflection(), unit_type)]]);
}

export function ModeSelector(props) {
    useGlobalKeyDownEvent((e) => {
        const matchValue = e.key;
        switch (matchValue) {
            case "c": {
                props.onSelectMode(new Mode(1));
                e.preventDefault();
                break;
            }
            case "s": {
                props.onSelectMode(new Mode(0));
                e.preventDefault();
                break;
            }
            default: {
            }
        }
    });
    return react.createElement(react.Fragment, {}, createElement("button", {
        className: Styles_button,
        onClick: (_arg1) => {
            props.onSelectMode(new Mode(0));
        },
        children: createElement(FirstUnderlined, {
            str: "Stopwatch",
        }),
    }), createElement("button", {
        className: Styles_button,
        onClick: (_arg2) => {
            props.onSelectMode(new Mode(1));
        },
        children: createElement(FirstUnderlined, {
            str: "Countdown",
        }),
    }));
}

