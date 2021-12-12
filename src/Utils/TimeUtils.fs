module TimeUtils
    let formatTime hours minutes seconds = sprintf $"%02i{hours}:%02i{minutes}:%02i{seconds}"

    let getSeconds time = time % 60
    let getMinutes time = (time / 60) % 60
    let getHours time = (time / 3600) % 24

    let splitTime time = (getHours time, getMinutes time, getSeconds time)
