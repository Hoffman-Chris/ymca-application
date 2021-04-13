// Function to check for integer value.
function integerCheck(value) {
    return Number.isInteger(parseFloat(value));
}

// Function to check for float value.
function floatCheck(value) {
    if (!isNaN(value)) {
        if (value % 1 != 0) {
            return true;
        }
    }
}

// Function to check if string contains only letters.
function letterCheck(value) {
    return value.match(/[^\d,]/g, '');
}

// Function to truncate decimals without rounding.
function toFixed(num, fixed) {
    var re = new RegExp('^-?\\d+(?:\.\\d{0,' + (fixed || -1) + '})?');
    return num.toString().match(re)[0];
}

// Function to pad numbers with 0's.
function pad(number, width = 3, z = 0) {
    return (String(z).repeat(width) + String(number)).slice(String(number).length)
}

// Function to remove special characters.
function removeSpecials(string) {
    return string.replace(/[^a-zA-Z0-9,:\-\. ]/g, "");
};

// Function to convert fields to title case and remove special characters.
function titleCase(string) {
    return string.replace(
        /\w\S*/g,
        function (txt) {
            return txt.charAt(0).toUpperCase() + txt.substr(1).toLowerCase();
        }
    ).replace("&", "and")
        .replace(/[^a-zA-Z0-9 ]/g, "");
};

// Function to limit field length.
function limitLength(string, max) {
    if (string.length > max) {
        string = string.substr(0, max);
    };

    return string;
};

// Function to get today's date.
function getDate() {
    today = new Date().getMonth() + 1
        + "/" + new Date().getDate()
        + "/" + new Date().getFullYear()
        + " " + new Date().getHours()
        + ":" + new Date().getMinutes()
        + ":" + new Date().getSeconds()
    return today;
};