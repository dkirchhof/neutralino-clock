import { css } from "../Bindings/Emotion.fs.js";
import { createElement } from "react";
import * as react from "react";
import { substring } from "../fable_modules/fable-library.3.6.1/String.js";

export const Styles_first = css("text-decoration: underline");

export function FirstUnderlined(firstUnderlinedInputProps) {
    let value_4;
    const str = firstUnderlinedInputProps.str;
    return react.createElement(react.Fragment, {}, createElement("span", {
        className: Styles_first,
        children: substring(str, 0, 1),
    }), (value_4 = substring(str, 1), createElement("span", {
        children: [value_4],
    })));
}

