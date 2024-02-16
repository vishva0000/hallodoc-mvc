// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var t = document.getElementById("theme-btn"); //Moon icon
var m = document.getElementById("moon");
var sun = document.getElementById("sun");
var x = document.getElementById("password"); // Input
var xc = document.getElementById("confirmPassword"); // Input
var s = document.getElementById("show"); // Show pass
var h = document.getElementById("hide"); // Hide pass
var sc = document.getElementById("showc"); // Show pass
var hc = document.getElementById("hidec"); // Hide pass
var ft = 0;

window.onload = function (event) {
    sun.style.display = "none";
    h.style.display = "none";

}

function darkMode() {
    let element = document.body;
    element.classList.toggle("darkMode");

    if (ft == 1) {
        sun.style.display = "none";
        m.style.display = "inline";


    }
    else {
        m.style.display = "none";
        sun.style.display = "inline";
        ft = 1;
    }

}
var myModal = new bootstrap.Modal(document.getElementById('myModal'), {})
myModal.show()
let hidemodal = document.getElementById('hidemodal');
hidemodal.addEventListener('click', () => {
    myModal.hide();
})

function back() {
    window.history.back()
}


function togglePass() {
    if (x.type === "password") {
        x.type = "text";
        s.style.display = "none";
        h.style.display = "inline";
    } else {
        x.type = "password";
        s.style.display = "inline";
        h.style.display = "none";
    }
}

function togglePassForConfirm() {
    if (xc.type === "password") {
        xc.type = "text";
        sc.style.display = "none";
        hc.style.display = "inline";
    } else {
        xc.type = "password";
        sc.style.display = "inline";
        hc.style.display = "none";
    }
}
