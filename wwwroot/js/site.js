var dir;
var log;
var logIndex;
var inputBox;
var outputWindow;

document.addEventListener("DOMContentLoaded", function (event) {
    SetDir("C:\ ");
    logIndex = log.length - 1;
    inputBox = document.getElementById("input");
    outputWindow = document.getElementById("output");
});

function Run(event) {
    var key = event.which || event.keyCode;
    switch (key) {
        case 13:
            window.scrollTo(0, document.body.scrollHeight);
            var input = inputBox.value;
            var output = document.createElement("SPAN");
            log.push(input);
            logIndex = log.length - 1;
            inputBox.value = "";
            if (input.substring(0, 3) == "cd " ||
                input.substring(0, 3) == "CD ") {
                var xhr = new XMLHttpRequest();
                xhr.onreadystatechange = function () {
                    var response = this.responseText
                    if (this.readyState == 4 && this.status == 200) {
                        if (response == "Дериктория не найдена") {
                            output.innerHTML = response;
                            output.style = "color: orangered";
                            outputWindow.appendChild(output);
                        } else {
                            SetDir(input.substring(3));
                        }
                    }
                };
                xhr.open("GET", "Home/Command?cmd=&dir=" + input.substring(3), true);
                xhr.send();
            } else {
                var separator = document.createElement("SPAN");
                separator.innerHTML = "<br>== == == == == == == == == == == == == == == == == ==<br>";
                separator.style = "color: aqua";
                outputWindow.appendChild(separator);
                var executedCMD = document.createElement("SPAN");
                executedCMD.innerHTML = dir + "> " + input + "<br>";
                outputWindow.appendChild(executedCMD);
                var xhr = new XMLHttpRequest();
                xhr.onreadystatechange = function () {
                    var response = this.responseText
                    if (this.readyState == 4 && this.status == 200) {
                        if (response != "") {
                            output.innerHTML = response
                            output.style = "color: lawngreen";
                        } else {
                            output.innerHTML = "Не удалось выполнить указанную команду";
                            output.style = "color: orangered";
                        }
                        outputWindow.appendChild(output);
                    }
                };
                xhr.open("GET", "Home/Command?cmd=" + input + "&dir=" + dir, true);
                xhr.send();
            }
            break;
        case 38:
            inputBox.value = log[logIndex];
            if (logIndex > 0) {
                logIndex--;
            }
            break;
        case 40:
            inputBox.value = log[logIndex];
            if (logIndex < log.length - 1) {
                logIndex++;
            }
            break;
    }
}

function SetDir(newDir){
    dir = newDir;
    document.getElementById("dir").innerHTML = dir + "> ";
}
