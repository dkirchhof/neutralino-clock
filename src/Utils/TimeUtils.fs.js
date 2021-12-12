import { interpolate, toText } from "../fable_modules/fable-library.3.6.1/String.js";

export function formatTime(hours, minutes, seconds) {
    return toText(interpolate("%02i%P():%02i%P():%02i%P()", [hours, minutes, seconds]));
}

export function getSeconds(time) {
    return time % 60;
}

export function getMinutes(time) {
    return (~(~(time / 60))) % 60;
}

export function getHours(time) {
    return (~(~(time / 3600))) % 24;
}

export function splitTime(time) {
    return [getHours(time), getMinutes(time), getSeconds(time)];
}

