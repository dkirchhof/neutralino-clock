module Theme
    type Colors = {
        bg: string
        overlay: string
        subtle: string
        rose: string
        pine: string
    }

    let rosePineMoon = {
        bg = "#232136"
        overlay = "#393552"
        subtle = "#6e6a86"
        rose = "#ea9a97"
        pine = "#3e8fb0"
    }

    let rosePineDawn = {
        bg = "#faf4ed"
        overlay = "#f2e9de"
        subtle = "#9893a5"
        rose = "#d7827e"
        pine = "#286983"
    }

    let colors = rosePineDawn

    module Styles = 
        let buttonsBar = Emotion.css "
            display: grid;
            grid-auto-flow: column;
            grid-gap: 10px;
            justify-content: center;

            margin-top: 10px;
        "

        let roundButton = Emotion.css $"
            width: 42px;
            height: 42px;
            padding: 0;

            color: {colors.subtle};
            background: none;
            border: 1px solid {colors.overlay};
            border-radius: 50%%;
            outline: none;

            cursor: pointer;

            :hover {{
                background: {colors.overlay};
            }}
        " 
