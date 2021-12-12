import { Record } from "../fable_modules/fable-library.3.6.1/Types.js";
import { record_type, string_type } from "../fable_modules/fable-library.3.6.1/Reflection.js";
import { css } from "../Bindings/Emotion.fs.js";

export class Colors extends Record {
    constructor(bg, overlay, subtle, rose, pine) {
        super();
        this.bg = bg;
        this.overlay = overlay;
        this.subtle = subtle;
        this.rose = rose;
        this.pine = pine;
    }
}

export function Colors$reflection() {
    return record_type("Theme.Colors", [], Colors, () => [["bg", string_type], ["overlay", string_type], ["subtle", string_type], ["rose", string_type], ["pine", string_type]]);
}

export const rosePineMoon = new Colors("#232136", "#393552", "#6e6a86", "#ea9a97", "#3e8fb0");

export const rosePineDawn = new Colors("#faf4ed", "#f2e9de", "#9893a5", "#d7827e", "#286983");

export const colors = rosePineDawn;

export const Styles_buttonsBar = css("\n            display: grid;\n            grid-auto-flow: column;\n            grid-gap: 10px;\n            justify-content: center;\n\n            margin-top: 10px;\n        ");

export const Styles_roundButton = css(`
width: 42px;
height: 42px;
padding: 0;
color: ${colors.subtle};
background: none;
border: 1px solid ${colors.overlay};
border-radius: 50%;
outline: none;
cursor: pointer;
:hover {
background: ${colors.overlay};
}
`);

